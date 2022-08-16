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
using static Today.Web.ViewModels.ClassifyCardInfo;

namespace Today.Web.Services.ClassifyService
{
    public class ClassifyService : IClassifyService
    {
        private readonly IGenericRepository _repo;
        public ClassifyService(IGenericRepository repo)
        {
            _repo = repo;
        }
        //public ClassifyDTO GetClassifyPages(ClassifyRequestDTO input)
        //{
        //    var result = new ClassifyDTO()
        //    {
        //        CategoryList = new List<ClassifyDTO.CategoryDestinations>(),
        //    };

        //    var categoryList = _repo.GetAll<Category>();
        //    var productCategoryList = _repo.GetAll<ProductCategory>();
        //    var productList = _repo.GetAll<Product>();

        //    if (input.CategoryId != 0)
        //    {
        //        productList = productList.Where(p =>
        //            _repo.GetAll<ProductCategory>()
        //                .Any(pc =>
        //                    pc.ProductId == p.ProductId &&
        //                    pc.CategoryId == input.CategoryId)
        //        );
        //    };

        //    result.CardCount = productList.Count();

        //    var memberId = input.MemberId;
        //    result.ClassifyCardList = GetClassifyCards(productList
        //        .Skip(10 * (input.Page - 1))
        //        .Take(10)
        //        .ToList()
        //    , memberId);


        //    var category = _repo.GetAll<Category>().ToList();
        //    //var city1 = _repo.GetAll<City>();
        //    var maincategory = category.Where(category => category.ParentCategoryId == null);
        //    var categoryGroup = category.Where(category => category.ParentCategoryId != null).GroupBy(category => category.ParentCategoryId);
        //    foreach (var cy in maincategory)
        //    {
        //        var mainTemp = new ClassifyDTO.CategoryDestinations()
        //        {
        //            ProductCategoryId = cy.CategoryId,
        //            CategoryName = cy.CategoryName,
        //            ChildCategory = new List<ClassifyDTO.CategoryDestinations>()
        //        };
        //        foreach (var group in categoryGroup)
        //        {
        //            if (mainTemp.ProductCategoryId == group.Key)
        //            {
        //                foreach (var item in group)
        //                {
        //                    var x = new ClassifyDTO.CategoryDestinations()
        //                    {
        //                        ProductCategoryId = item.CategoryId,
        //                        CategoryName = item.CategoryName,
        //                    };
        //                    mainTemp.ChildCategory.Add(x);
        //                }
        //            }
        //        }
        //        result.CategoryList.Add(mainTemp);
        //    }
        //    return result;
        //}


        public GetAllFiltersOutputDTO GetClassifyFilters()  // 路由可能會預先勾選一些
        {
            var result = new GetAllFiltersOutputDTO()
            {
                CategoryFilterList = new List<FilterVM.CategoryFilter>(),
            };

            //貞德有產品的城市
            result.CityFilterList = _repo.GetAll<City>()
                .Where(c => _repo.GetAll<Product>().Any(p => p.CityId == c.CityId))
                .Select(c => new FilterVM.CityFilter
            {
                CityId = c.CityId,
                CityName = c.CityName,
                //Checked = false,
            }).ToList();




            var categoryList = _repo.GetAll<Category>();//.ToList();
            var maincategory = categoryList.Where(category => category.ParentCategoryId == null).ToList();
            foreach (var mainC in maincategory)
            {
                var mainTemp = new FilterVM.CategoryFilter()
                {
                    ProductCategoryId = mainC.CategoryId,
                    CategoryName = mainC.CategoryName,
                    ChildCategory = new List<FilterVM.CategoryFilter>(),
                    //Checked = false,
                };

                mainTemp.ChildCategory = categoryList
                    .Where(c => c.ParentCategoryId == mainC.CategoryId)
                    .Select( c => new FilterVM.CategoryFilter()
                    {
                        ProductCategoryId = c.CategoryId,
                        CategoryName = c.CategoryName,
                        //ChildCategory = null,
                        //Checked = false,
                    }).ToList();


                result.CategoryFilterList.Add(mainTemp);
            }



            //擴充其他條件

            return result;

        }


        public ClassifyDTO GetClassifyMatchedCards(FilterDTO input)
        {
            var result = new ClassifyDTO() { };

            var matchedProductList = _repo.GetAll<Product>();
            if (input.CityFilterList.Any())
            {
                matchedProductList = matchedProductList.Where(p => input.CityFilterList.Contains(p.CityId));
            }

            if (input.CategoryFilterList.Any())
            {
                matchedProductList = matchedProductList.Where(p =>
                _repo.GetAll<ProductCategory>()
                    .Where(pc => pc.ProductId == p.ProductId)
                    .Select(pc => pc.CategoryId)
                    .Any(cId => input.CategoryFilterList.Contains(cId))
                );
            }

            //擴充各種篩選條件...


            //篩定後
            result.CardCount = matchedProductList.Count();

            //排序
            var orderedMatchedProductList = matchedProductList;

            //只取分頁
            result.ClassifyCardList = GetClassifyCards(
                orderedMatchedProductList
                    .Skip(10 * (input.Page - 1))
                    .Take(10)
                    .ToList()
            , input.MemberId);

            return result;
        }

        private List<ClassifyCardInfo> GetClassifyCards(List<Product> product, int memberId)//context 可以 拿到登入者資訊
        {

            var result = new List<ClassifyCardInfo>();

            //var productPhoto1 = _repo.GetAll<ProductPhoto>().Where(pp => product.Select(p => p.ProductId).Contains(pp.ProductId));
            //var city = _repo.GetAll<City>().Where(c => product.Select(p => p.CityId).Contains(c.CityId));

            //var productTag = _repo.GetAll<ProductTag>();
            //var Tag = _repo.GetAll<Tag>();
            var comment = _repo.GetAll<Model.Models.Comment>();
            var programList = _repo.GetAll<Model.Models.Program>();
            var orderDetailList = _repo.GetAll<OrderDetail>();
            var specificationList = _repo.GetAll<ProgramSpecification>();

            //memberId = HtttpContex.User.xxx....
            var favoriteList = _repo.GetAll<Collect>().Where(c => c.MemberId == memberId ).Select(c => c.ProductId); //&& product.Any(p=> p.ProductId == c.ProductId) 

            product.ForEach(p => result.Add(
                new ClassifyCardInfo
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CityId = p.CityId,
                }));


            result.ForEach(p =>
            {
                p.Path = _repo.GetAll<ProductPhoto>().FirstOrDefault(ph => ph.ProductId == p.ProductId)?.Path.ToString();
                p.CityName = _repo.GetAll<City>().First(c => c.CityId == p.CityId).CityName;

                p.Favorite = favoriteList.Contains(p.ProductId);

                p.TagText = _repo.GetAll<Tag>()
                    .Where(t => _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == p.ProductId)
                    .Select(pt => pt.TagId)
                    .Contains(t.TagId))
                    .Select(t => t.TagText).ToList();
                p.RatingStar = (comment.Where(c => c.ProductId == p.ProductId)
                                       .Count() != 0) ? Math.Round((double)comment.Where(c => c.ProductId == p.ProductId)
                                       .Sum(c => c.RatingStar) / comment.Where(c => c.ProductId == p.ProductId).Count(), 1) : 0;
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



