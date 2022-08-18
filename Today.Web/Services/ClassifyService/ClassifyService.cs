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
using static Today.Web.DTOModels.ClassifyDTO.FilterDTO;
using static Today.Web.DTOModels.ClassifyDTO.ClassifyDTO;

namespace Today.Web.Services.ClassifyService
{
    public class ClassifyService : IClassifyService
    {
        private readonly IGenericRepository _repo;
        public ClassifyService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public DTOModels.ClassifyDTO.GetAllFiltersOutputDTO GetClassifyFilters()  // 路由可能會預先勾選一些
        {
            var result = new DTOModels.ClassifyDTO.GetAllFiltersOutputDTO()
            {
                CategoryFilterList = new List<FilterVM.CategoryFilter>(),
                CityFilterList = new List<FilterVM.CityFilter>(),
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
                    .Select(c => new FilterVM.CategoryFilter()
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

            var MatchedProducts = _repo.GetAll<Product>();
            if (input.CityFilterList.Any())
            {
                MatchedProducts = MatchedProducts.Where(p => input.CityFilterList.Contains(p.CityId));
            }

            if (input.CategoryFilterList.Any())
            {
                MatchedProducts = MatchedProducts.Where(p =>
                _repo.GetAll<ProductCategory>()
                    .Where(pc => pc.ProductId == p.ProductId)
                    .Select(pc => pc.CategoryId)
                    .Any(cId => input.CategoryFilterList.Contains(cId))
                );
            }

            if (input.IsOffIsland == true)
            {
                var productCategory = _repo.GetAll<ProductCategory>();
                // matchedProductList = matchedProductList.Where(p => productCategory.Any(pr => pr.CategoryId == 29 && pr.ProductId ==p.ProductId));
                MatchedProducts = MatchedProducts.Where(p => _repo.GetAll<City>().Any(c => c.IsIsland == false && p.CityId == c.CityId));
            }
            if (input.IsRent == true)
            {
                var productCategory = _repo.GetAll<ProductCategory>();
                MatchedProducts = MatchedProducts.Where(p => productCategory.Any(pr => pr.CategoryId == 42 && pr.ProductId == p.ProductId));
            }
            if (input.IsHSR == true)
            {
                var productCategory = _repo.GetAll<ProductCategory>();
                MatchedProducts = MatchedProducts.Where(p => productCategory.Any(pr => pr.CategoryId == 13 && pr.ProductId == p.ProductId));
            }
            if (input.IsDIY == true)
            {
                var productCategory = _repo.GetAll<ProductCategory>();
                MatchedProducts = MatchedProducts.Where(p => productCategory.Any(pr => pr.CategoryId == 53 && pr.ProductId == p.ProductId));
            }
            if(input.city_choosed !=null)
            {
                var city = _repo.GetAll<City>();
                MatchedProducts = MatchedProducts.Where(p => city.Any(c => c.CityName == input.city_choosed && c.CityId == p.CityId));
            }
            if (input.IsParent == true)
            {
                var productCategory = _repo.GetAll<ProductCategory>();
                MatchedProducts = MatchedProducts.Where(p => productCategory.Any(pr => pr.CategoryId == 22 && pr.ProductId == p.ProductId));
            }
            if(input.OffCityName != null)
            {
                var city = _repo.GetAll<City>();
                MatchedProducts = MatchedProducts.Where(p => city.Any(c => c.CityName == input.OffCityName && c.CityId == p.CityId));
            }
            if (input.typeBanner != null)
            {
                MatchedProducts = MatchedProducts.Where(p=>p.ProductName.Contains(input.typeBanner));
            }
            IEnumerable<Product> IEproduct = MatchedProducts;
            //v 閣廷 日期篩
            if (input.DateRange != null) //string.IsNullOrEmpty()
            {
                var dateS = DateTime.Parse(input.DateRange[0]);
                var dateE = DateTime.Parse(input.DateRange[1]).AddDays(1);

                //擴充各種篩選條件...

                //產品任何一個方案 在篩選日期範圍內 就保留
                IEproduct = MatchedProducts.ToList().Where(p => this.CheckProductCanUse(dateS, dateE, p));
            }
            //擴充各種篩選條件...


            //篩定後
            var MatchedProductList = MatchedProducts.ToList();
            result.CardCount = MatchedProductList.Count;


            //1. 先算有關排序的條件 再排序、篩分頁 再變成卡 || 
            ///2. 先變成卡 全吐|| 再排序、篩分頁 => api 取消分頁條件 分頁不打api

            var MatchedCardList = ToInitialCards(MatchedProductList);
            //排序
            var orderedMatchedProductList = new List<ClassifyCardInfo>();
            //算排序條件值 > 排序 >　切頁＞　補算另外兩項
            if ( input.SortBy == SortCondition.熱門程度)
            {
                orderedMatchedProductList = 
                    withPrice(
                        GetCards_withRatingStar(
                            GetCards_withTotalOrder(MatchedCardList)
                                .OrderByDescending(card => card.TotalOrder)//排序
                                .Skip(10 * (input.Page - 1)).Take(10).ToList()//分頁
                        )
                    );
            }
            if (input.SortBy == SortCondition.評價星星數)
            {
                orderedMatchedProductList =
                    withPrice(
                        GetCards_withTotalOrder(
                            GetCards_withRatingStar(MatchedCardList)
                                .OrderByDescending(card => card.RatingStar)
                                .Skip(10 * (input.Page - 1)).Take(10).ToList()
                        )
                    );

            }
            if (input.SortBy == SortCondition.價錢低到高)
            {
                orderedMatchedProductList =
                    GetCards_withRatingStar(
                        GetCards_withTotalOrder(
                            withPrice(MatchedCardList)
                                .OrderBy(card => card.Prices.Price)
                                .Skip(10 * (input.Page - 1)).Take(10).ToList()
                        )
                    );
            }

            //剩下的資訊
            result.ClassifyCardList = GetClassifyCards(
                orderedMatchedProductList
                    //.Skip(10 * (input.Page - 1))
                    //.Take(10)
                    //.ToList()
            , input.MemberId);
            return result;
        }
        private List<ClassifyCardInfo> ToInitialCards(List<Product> product)
        {
            var result = new List<ClassifyCardInfo>();

            product.ForEach(p => result.Add(
                new ClassifyCardInfo
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CityId = p.CityId,
                }));

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

            foreach (var pg in ProgramList)
            {
                var programnot = ProgramCantUseDate.Where(X => X.ProgramId == pg.ProgramId).ToList();
                if (programnot.Count == 0) return true;

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

                if (CanorNot) return true;
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

        //private List<ClassifyCardInfo> GetClassifyCards(List<Product> product, int memberId)//context 可以 拿到登入者資訊
            //另種寫法
            //product.Select(p=> new ClassifyCardInfo
            //{
            //    ProductId = p.ProductId,
            //    ProductName = p.ProductName,
            //    CityId = p.CityId,
            //})
            //.ToList();
        //}
        
        private List<ClassifyCardInfo> GetCards_withTotalOrder( List<ClassifyCardInfo> cards)
        {
            var programList = _repo.GetAll<Model.Models.Program>();
            var orderDetailList = _repo.GetAll<OrderDetail>();
            var specificationList = _repo.GetAll<ProgramSpecification>();

            cards.ForEach(card =>
            {
                card.TotalOrder = programList.Where(program => program.ProductId == card.ProductId)
                            .Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new { program.ProgramId, specification.SpecificationId })
                            .Join(orderDetailList, specification => specification.SpecificationId, orderDetail => orderDetail.SpecificationId, (specification, orderDetail) => new { orderDetail.Quantity })
                            .Sum(n => n.Quantity);
            });
            return cards;
        }

        private List<ClassifyCardInfo> GetCards_withRatingStar(List<ClassifyCardInfo> cards)
        {
            cards.ForEach(card =>
            {
                var comment = _repo.GetAll<Model.Models.Comment>().Where(c => c.ProductId == card.ProductId);
                card.TotalComment = comment.Count();
                card.RatingStar = (card.TotalComment != 0)
                        ? Math.Round((double)comment.Sum(c => c.RatingStar) / card.TotalComment, 1)
                        : 0;
            });
            return cards;
        }

        private List<ClassifyCardInfo> withPrice(List<ClassifyCardInfo> cards)
        {
            var programList = _repo.GetAll<Model.Models.Program>();
            var orderDetailList = _repo.GetAll<OrderDetail>();
            var specificationList = _repo.GetAll<ProgramSpecification>();

            cards.ForEach(card =>
            {
                card.Prices = programList
                    .Where(program => program.ProductId == card.ProductId)
                    .Join(specificationList, 
                    program => program.ProgramId, 
                    specification => specification.ProgramId, 
                    (program, specification) => new PriceInfo { OriginalPrice = specification.OriginalUnitPrice, Price = specification.UnitPrice  })
                    .OrderBy(specification => specification.Price)
                    .FirstOrDefault();
                if(card.Prices == null)
                {
                    card.Prices = new PriceInfo
                    {
                        OriginalPrice = 10826,
                        Price = 10826
                    };
                }
            });
            return cards;
        }
        private List<ClassifyCardInfo> GetClassifyCards(List<ClassifyCardInfo> product, int memberId)//context 可以 拿到登入者資訊
        {
            var favoriteList = _repo.GetAll<Collect>().Where(c => c.MemberId == memberId).Select(c => c.ProductId); //&& product.Any(p=> p.ProductId == c.ProductId) 

            product.ForEach(p => {            
                p.Path = _repo.GetAll<ProductPhoto>().FirstOrDefault(ph => ph.ProductId == p.ProductId)?.Path.ToString();
                p.CityName = _repo.GetAll<City>().First(c => c.CityId == p.CityId).CityName;

                p.Favorite = favoriteList.Contains(p.ProductId);

                p.TagText = _repo.GetAll<Tag>()
                    .Where(t => _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == p.ProductId)
                    .Select(pt => pt.TagId)
                    .Contains(t.TagId))
                    .Select(t => t.TagText).ToList();
            });
            return product;
        }

    }
}



