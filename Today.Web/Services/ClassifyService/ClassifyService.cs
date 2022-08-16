using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;
using Today.Web.DTOModels.ClassifyDTO;
using System;
using System.Web;
using Today.Web.Services.ProductService;
using Comment = Today.Model.Models.Comment;
using static Today.Web.DTOModels.ClassifyDTO.ClassifyDTO;
using static Today.Web.ViewModels.ClassifyVM;
using static Today.Web.ViewModels.ClassifyVM.ClassifyCardInfo;

namespace Today.Web.Services.ClassifyService
{
    public class ClassifyService : IClassifyService
    {
        private readonly IGenericRepository _repo;
        public ClassifyService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public ClassifyDTO GetClassifyPages(ClassifyRequestDTO input)
        {
            var memberId = input.MemberId;
            var result = new ClassifyDTO()
            {
                CategoryList = new List<ClassifyDTO.CategoryDestinations>(),

            };

            var categoryList = _repo.GetAll<Category>();
            var productCategoryList = _repo.GetAll<ProductCategory>();
            var productList = _repo.GetAll<Product>();

            if (input.CategoryId != 0)
            {
                productList = productList.Where(p =>
                    _repo.GetAll<ProductCategory>()
                        .Any(pc =>
                            pc.ProductId == p.ProductId &&
                            pc.CategoryId == input.CategoryId)
                );
            };
            var category = _repo.GetAll<Category>().ToList();
            if (input.isOffIsland == true)
            {
                var city = _repo.GetAll<City>();
                productList = productList.Where(p => city.Any(c => c.CityId == p.CityId && c.IsIsland==false));
                //var productCategory = _repo.GetAll<ProductCategory>();
                //productList = productList.Where(p => productCategory.Any(pr => pr.CategoryId == 29)).Distinct();
            }
            //result.DateCantbeUseList = this.NewMethod(input.DateRange);

            result.CardCount = productList.Count();
            result.ClassifyCardList = AddClassifyCardToResult(productList
                .Skip(10 * (input.Page - 1))
                .Take(10)
                .ToList()
            , memberId);



            var city1 = _repo.GetAll<City>();

            var maincategory = category.Where(category => category.ParentCategoryId == null);
            var categoryGroup = category.Where(category => category.ParentCategoryId != null).GroupBy(category => category.ParentCategoryId);
            foreach (var cy in maincategory)
            {
                var mainTemp = new ClassifyDTO.CategoryDestinations()
                {
                    ProductCategoryId = cy.CategoryId,
                    CategoryName = cy.CategoryName,
                    ChildCategory = new List<ClassifyDTO.CategoryDestinations>()
                };
                foreach (var group in categoryGroup)
                {
                    if (mainTemp.ProductCategoryId == group.Key)
                    {
                        foreach (var item in group)
                        {
                            var x = new ClassifyDTO.CategoryDestinations()
                            {
                                ProductCategoryId = item.CategoryId,
                                CategoryName = item.CategoryName,
                            };
                            mainTemp.ChildCategory.Add(x);
                        }
                    }
                }
                result.CategoryList.Add(mainTemp);
            }
            return result;
        }


        public ClassifyDTO GetClassifyMatchedProducts(FilterDTO input)
        {
            var result = new ClassifyDTO() { };

            var productList = _repo.GetAll<Product>();
            if (input.CityFilterList.Any())
            {
                productList = productList.Where(p => input.CityFilterList.Contains(p.CityId));
            }

            if (input.CategoryFilterList.Any())
            {
                productList = productList.Where(p =>
                _repo.GetAll<ProductCategory>()
                    .Where(pc => pc.ProductId == p.ProductId)
                    .Select(pc => pc.CategoryId)
                    .Any(cId => input.CategoryFilterList.Contains(cId))
                );
            }
            if (input.isOffIsland == true)
            {
                var city = _repo.GetAll<City>().Where(c => c.IsIsland == false); ;
                productList = productList.Where(p => city.Select(c => c.CityId).Contains(p.CityId));
                //var productCategory = _repo.GetAll<ProductCategory>();
                //productList = productList.Where(p => productCategory.Any(pr => pr.CategoryId == 29)).Distinct();
            }
            IEnumerable<Product> IEproduct = productList;
            //v 格停 日期篩
            if (input.DateRange !=null) //string.IsNullOrEmpty()
            {
                var dateS = DateTime.Parse(input.DateRange[0]);
                var dateE = DateTime.Parse(input.DateRange[1]).AddDays(1);


                //產品任何一個方案 在篩選日期範圍內 就保留
                IEproduct = productList.ToList().Where(p => this.CheckProductCanUse(dateS , dateE , p) );
            }

            //^ 格停 日期篩

            result.CardCount = IEproduct.Count();
            result.ClassifyCardList = AddClassifyCardToResult(IEproduct
                .Skip(10 * (input.Page - 1))
                .Take(10)
                .ToList()
            , input.MemberId);

            return result;
        }


        //每個方案 => 完全不可用? 
        // 方案有禁用日期
        //不可用的定義 = 範圍內完全都是禁的

        private bool CheckProductCanUse(DateTime dateStart, DateTime dateEnd, Product p)
        {
            var ProgramList = _repo.GetAll<Today.Model.Models.Program>()
                .Where(pg => pg.ProductId == p.ProductId)
                .ToList();

            var AllNotbeuse = _repo.GetAll<ProgramCantUseDate>().ToList();
            //只考慮範圍內、和這些方案有關的 禁用日期
            var ProgramCantUseDate = AllNotbeuse
                .Where(x =>
                    x.Date >= DateTime.UtcNow && x.Date >= dateStart && x.Date < dateEnd
                    && ProgramList.Any(pg => pg.ProgramId == x.ProgramId)
                )
                .ToList();


            foreach( var pg in ProgramList)
            {
                var programnot = ProgramCantUseDate.Where(X => X.ProgramId == pg.ProgramId).ToList();
                if(  programnot.Count ==0 ) return true;

                //只要有個方案 有一天 沒被禁 就是可以
                bool CanorNot = programnot
                    .Any(x =>
                    {
                        for (var dt = dateStart; dt < dateEnd; dt = dt.AddDays(1))
                        {
                            if (dt != x.Date)
                                return true;
                        };
                        return false;
                    });
            
                if(CanorNot) return true;
            };

            return false;
        }

        //private List<ProgramCantUseDate> NewMethod(List<string> realDate)
        //{
        //    var Program = _repo.GetAll<Today.Model.Models.Program>();
        //    var ProgramCantUseDate = _repo.GetAll<ProgramCantUseDate>().ToList();
        //    if (realDate.Any())
        //    {
        //        var TimeList = new List<DateTime>();

        //        string ds = realDate.Aggregate((x, y) => x + "," + y); //用linq Aggregate()將List<string>轉成string
        //        var dateS = ds.Substring(6, 4) + '-' + ds.Substring(0, 2) + '-' + ds.Substring(3, 2);
        //        var dateE = ds.Substring(17, 4) + '-' + ds.Substring(11, 2) + '-' + ds.Substring(14, 2);
        //        TimeList.Add(DateTime.Parse(dateS));
        //        TimeList.Add(DateTime.Parse(dateE));

        //        if (TimeList.Any()) //如果Date有值得話
        //        {
        //            ProgramCantUseDate = _repo.GetAll<ProgramCantUseDate>().Where(x => x.Date <= TimeList[0] || x.Date >= TimeList[1]).ToList(); //篩選出一個product的program , 它(不能用的日期)不能再選取內
        //        }
        //    }

        //    return ProgramCantUseDate;
        //}
        private List<ClassifyVM.ClassifyCardInfo> AddClassifyCardToResult(List<Product> product, int memberId)
        {
            var result = new List<ClassifyVM.ClassifyCardInfo>();

            var productPhoto1 = _repo.GetAll<ProductPhoto>().Where(pp => product.Select(p => p.ProductId).Contains(pp.ProductId));
            var city = _repo.GetAll<City>().Where(c => product.Select(p => p.CityId).Contains(c.CityId));
            var productTag = _repo.GetAll<ProductTag>();
            var Tag = _repo.GetAll<Tag>();
            var comment = _repo.GetAll<Model.Models.Comment>();
            var programList = _repo.GetAll<Model.Models.Program>();
            var orderDetailList = _repo.GetAll<OrderDetail>();
            var specificationList = _repo.GetAll<ProgramSpecification>();
            var UnitPriceList = new List<ClassifyVM.ClassifyCardInfo>();
            var Program = _repo.GetAll<Today.Model.Models.Program>();
            var ProgramCantUseDate = _repo.GetAll<ProgramCantUseDate>();
            //product.ForEach(p =>
            //{
            //    programList.Where(pg => pg.ProductId == p.ProductId)
            //    .ToList()
            //    .ForEach(pg =>
            //    {
            //        UnitPriceList.Add(new ClassifyDTO.ClassifyCardInfo
            //        {
            //            ProductId = pg.ProductId,
            //            UnitPrice = specificationList.Where(sp => pg.ProgramId == sp.ProgramId)
            //                                        .OrderBy(sp => sp.UnitPrice)
            //                                        .FirstOrDefault().UnitPrice,
            //            OriginalPrice = specificationList.Where(sp => pg.ProgramId == sp.ProgramId)
            //                                        .OrderBy(sp => sp.UnitPrice)
            //                                        .FirstOrDefault().OriginalUnitPrice
            //        });
            //    });

            var favoriteList = _repo.GetAll<Collect>().Where(c => c.MemberId == memberId).Select(c => c.ProductId);

            //});

            product.ForEach(p => result.Add(
                new ClassifyCardInfo
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CityId = p.CityId,
                }));

            result.ForEach(p =>
            {
                p.Path = _repo.GetAll<ProductPhoto>().First(ph => ph.ProductId == p.ProductId).Path.ToString();
                p.CityName = _repo.GetAll<City>().First(c => c.CityId == p.CityId).CityName.ToString();
                p.Favorite = favoriteList.Contains(p.ProductId);
                p.TagText = _repo.GetAll<Tag>()
                    .Where(t => _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == p.ProductId)
                    .Select(pt => pt.TagId)
                    .Contains(t.TagId))
                    .Select(t => t.TagText).ToList();
                //p.UnitPrice = UnitPriceList.Where(u => u.ProductId == p.ProductId).Select(u => u.UnitPrice).First();
                //p.OriginalPrice = UnitPriceList.Where(u => u.ProductId == p.ProductId).Select(u => u.OriginalPrice).First();
                p.RatingStar = (comment.Where(c => c.ProductId == p.ProductId)
                                       .Count() != 0) ? (float)comment.Where(c => c.ProductId == p.ProductId)
                                       .Sum(c => c.RatingStar) / comment.Where(c => c.ProductId == p.ProductId).Count() : 0;
                p.TotalComment = comment.Where(c => c.ProductId == p.ProductId).Count();
                p.TotalOrder = programList.Where(program => program.ProductId == p.ProductId)
                                    .Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new { program.ProgramId, specification.SpecificationId })
                                    .Join(orderDetailList, specification => specification.SpecificationId, orderDetail => orderDetail.SpecificationId, (specification, orderDetail) => new { orderDetail.Quantity })
                                    .Sum(n => n.Quantity);
                p.Prices = programList.Where(program => program.ProductId == p.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new PriceInfo { OriginalPrice = specification.OriginalUnitPrice, Price = specification.UnitPrice }).OrderBy(specification => specification.Price).FirstOrDefault();
                
                
            });
            return result;
         }
    }
}



