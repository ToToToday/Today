using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels.ProductDTO;
using Today.Web.Repository;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ProductDTO.ProductDTO;

namespace Today.Web.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository _repo;
        private readonly IMemoryCacheRepository _iMemoryCacheRepository;
        public ProductService (IGenericRepository repo, IMemoryCacheRepository iMemoryCacheRepository)
        {
            _repo = repo;
            _iMemoryCacheRepository = iMemoryCacheRepository;
        }
        public ProductResponseDTO ConvertPages(ProductRequestDTO search)
        {
            var cityList = _repo.GetAll<City>();
            var categoryList = _repo.GetAll<Category>();
            //var productList = _repo.GetAll<Product>();

            var selectCity = cityList.Where(c => c.CityName.Contains(search.SearchWord)).Select(c => c.CityId);
            var selectCategory = categoryList.Where(c => c.CategoryName.Contains(search.SearchWord)).Select(c => c.CategoryId);

            var result = new ProductResponseDTO();
            int? cityId = (selectCity.Count() != 0) ? selectCity.First() : null;
            int? categoryId = (selectCategory.Count() != 0) ? selectCategory.First() : null;
            //var productId = productList.Where(p => p.ProductName.Contains(search.SearchWord)).Select(p => p.ProductId).ToList();

            if (cityId != null && categoryId == null)
            {
                result.HasCityId = true;
                result.Id = (int)cityId;
            }
            else if (categoryId != null && cityId == null)
            {
                result.HasCityId = false;
                result.Id = (int)categoryId;
            }
            else
            {
                result.HasCityId = false;
                result.Id = 0;
            }

            return result;
        }
        
        public ProductDTO AllProduct()
        {
            var result = new ProductDTO {  };
            var productList = _repo.GetAll<Product>();
            var productPhotoList = _repo.GetAll<ProductPhoto>();
            var productCategoryList = _repo.GetAll<ProductCategory>();
            var categoryList = _repo.GetAll<Category>();
            var cityList = _repo.GetAll<City>();
            var productTagList = _repo.GetAll<ProductTag>();
            var tagList = _repo.GetAll<Tag>();
            var programList = _repo.GetAll<Model.Models.Program>();
            var specificationList = _repo.GetAll<ProgramSpecification>();
            var commentList = _repo.GetAll<Model.Models.Comment>();
            var orderDetailList = _repo.GetAll<OrderDetail>();
            var mainCategoryList = categoryList.Where(category => category.ParentCategoryId == null);
            var categoryListGroup = categoryList.Where(category => category.ParentCategoryId != null).ToList().GroupBy(category => category.ParentCategoryId);

            #region categoryList
            List<CategoryInfo> allCategoryTemp = new List<CategoryInfo>();
            foreach (var category in mainCategoryList)
            {
                var mainTemp = new CategoryInfo
                {
                    Id = category.CategoryId,
                    Name = category.CategoryName,
                    ChildCategoryList = new List<CategoryInfo>()
                };

                foreach (var group in categoryListGroup)
                {
                    if (mainTemp.Id == group.Key)
                    {
                        foreach (var item in group)
                        {
                            var temp = new CategoryInfo
                            {
                                Id = item.CategoryId,
                                Name = item.CategoryName
                            };
                            mainTemp.ChildCategoryList.Add(temp);
                        }
                    }
                }
                allCategoryTemp.Add(mainTemp);
            }
            #endregion

            #region productList
            result = new ProductDTO()
            {
                QueryProduct = productList.Select(x => new ProductDTO.ProductInfo
                {
                    Id = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPhoto = productPhotoList.Where(pp => pp.ProductId == x.ProductId).Select(x => x.Path).First(),
                    ChildCategoryName = productCategoryList.Where(pc => pc.ProductId == x.ProductId).Join(categoryList, pc => pc.CategoryId, c => c.CategoryId, (pc, c) => new { pc.ProductId, c.CategoryName }).Select(c => c.CategoryName).First(),
                    CityId = x.CityId,
                    CityName = cityList.Where(c => c.CityId == x.CityId).Select(c => c.CityName).First(),
                    Tags = productTagList.Where(pt => pt.ProductId == x.ProductId).Join(tagList, pt => pt.TagId, t => t.TagId, (pt, t) => new { pt.ProductId, t.TagText }).Select(x => x.TagText).ToList(),
                    Rating = new RatingInfo() { RatingStar = (commentList.Where(comment => comment.ProductId == x.ProductId).Count() != 0) ? (float)commentList.Where(comment => comment.ProductId == x.ProductId).Sum(comment => comment.RatingStar) / commentList.Where(comment => comment.ProductId == x.ProductId).Count() : 0, TotalGiveComment = commentList.Where(comment => comment.ProductId == x.ProductId).Count() },
                    TotalOrder = programList.Where(program => program.ProductId == x.ProductId)
                                            .Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new { program.ProgramId, specification.SpecificationId })
                                            .Join(orderDetailList, specification => specification.SpecificationId, orderDetail => orderDetail.SpecificationId, (specification, orderDetail) => new { orderDetail.Quantity })
                                            .Sum(n => n.Quantity),
                    Prices = programList.Where(program => program.ProductId == x.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new PriceInfo { OriginalPrice = specification.OriginalUnitPrice, Price = specification.UnitPrice }).OrderBy(specification => specification.Price).FirstOrDefault()
                }),
                CategoryList = allCategoryTemp
            };
            #endregion

            return result;
        }

        public ProductDTO GetAllProductCard(int user)
        {
            var favoriteList = _repo.GetAll<Collect>().Where(c => c.MemberId == user).Select(c => c.ProductId);
            var result = _iMemoryCacheRepository.Get<ProductDTO>("Product.GetAllProductCard");
            if (result != null)
            {
                result = new ProductDTO {
                    RecentlyViewed = result.RecentlyViewed.Select(x => new RecentlyInfo
                    {
                        Id = x.Id,
                        ProductName = x.ProductName,
                        ProductPhoto = x.ProductPhoto,
                        Favorite = favoriteList.Contains(x.Id),
                        Price = x.Price
                    }).ToList(),
                    TopProduct = result.TopProduct.Select(x => new ProductInfo
                    {
                        Id = x.Id,
                        ProductPhoto = x.ProductPhoto,
                        ProductName = x.ProductName,
                        CityName = x.CityName,
                        Favorite = favoriteList.Contains(x.Id),
                        Tags = x.Tags,
                        Rating = x.Rating,
                        TotalOrder = x.TotalOrder,
                        Prices = x.Prices
                    }).ToList(),
                    Featured = result.Featured.Select(x => new ProductInfo
                    {
                        Id = x.Id,
                        ProductPhoto = x.ProductPhoto,
                        ProductName = x.ProductName,
                        CityName = x.CityName,
                        Favorite = favoriteList.Contains(x.Id),
                        Tags = x.Tags,
                        Rating = x.Rating,
                        TotalOrder = x.TotalOrder,
                        Prices = x.Prices
                    }).ToList(),
                    Paradise = result.Paradise.Select(x => new ProductInfo
                    {
                        Id = x.Id,
                        ProductPhoto = x.ProductPhoto,
                        ProductName = x.ProductName,
                        CityName = x.CityName,
                        Favorite = favoriteList.Contains(x.Id),
                        Tags = x.Tags,
                        Rating = x.Rating,
                        TotalOrder = x.TotalOrder,
                        Prices = x.Prices
                    }).ToList(),
                    AttractionTickets = result.AttractionTickets.Select(x => new ProductInfo
                    {
                        Id = x.Id,
                        ProductPhoto = x.ProductPhoto,
                        ProductName = x.ProductName,
                        CityName = x.CityName,
                        Favorite = favoriteList.Contains(x.Id),
                        Tags = x.Tags,
                        Rating = x.Rating,
                        TotalOrder = x.TotalOrder,
                        Prices = x.Prices
                    }).ToList(),
                    Exhibition = result.Exhibition.Select(x => new ProductInfo
                    {
                        Id = x.Id,
                        ProductPhoto = x.ProductPhoto,
                        ProductName = x.ProductName,
                        CityName = x.CityName,
                        Favorite = favoriteList.Contains(x.Id),
                        Tags = x.Tags,
                        Rating = x.Rating,
                        TotalOrder = x.TotalOrder,
                        Prices = x.Prices
                    }).ToList(),
                    Hotel = result.Hotel.Select(x => new ProductInfo
                    {
                        Id = x.Id,
                        ProductPhoto = x.ProductPhoto,
                        ProductName = x.ProductName,
                        CityName = x.CityName,
                        Favorite = favoriteList.Contains(x.Id),
                        Tags = x.Tags,
                        Rating = x.Rating,
                        TotalOrder = x.TotalOrder,
                        Prices = x.Prices
                    }).ToList(),
                    Taoyuan = result.Taoyuan.Select(x => new ProductInfo
                    {
                        Id = x.Id,
                        ProductPhoto = x.ProductPhoto,
                        ProductName = x.ProductName,
                        CityName = x.CityName,
                        Favorite = favoriteList.Contains(x.Id),
                        Tags = x.Tags,
                        Rating = x.Rating,
                        TotalOrder = x.TotalOrder,
                        Prices = x.Prices
                    }).ToList(),
                    Evaluation = result.Evaluation.Select(x => new ProductInfo
                    {
                        Id = x.Id,
                        ProductPhoto = x.ProductPhoto,
                        ProductName = x.ProductName,
                        CityName = x.CityName,
                        Favorite = favoriteList.Contains(x.Id),
                        Tags = x.Tags,
                        Rating = x.Rating,
                        TotalOrder = x.TotalOrder,
                        Prices = x.Prices
                    }).ToList(),
                };
                return result;
            }

            var dataSource = AllProduct();
            var productSource = dataSource.QueryProduct;
            var categorySource = dataSource.CategoryList;

            result = new ProductDTO
            {
                RecentlyViewed = productSource.Take(3).Select(x => new ProductDTO.RecentlyInfo
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    ProductPhoto = x.ProductPhoto,
                    Favorite = favoriteList.Contains(x.Id),
                    Price = (x.Prices == null) ? null : x.Prices.Price
                }).ToList(),
                TopProduct = productSource.OrderByDescending(x => x.TotalOrder).Take(10).Select(x => new ProductInfo { 
                    Id = x.Id,
                    ProductPhoto = x.ProductPhoto,
                    ProductName = x.ProductName,
                    CityName = x.CityName,
                    Favorite = favoriteList.Contains(x.Id),
                    Tags = x.Tags,
                    Rating = x.Rating,
                    TotalOrder = x.TotalOrder,
                    Prices = x.Prices
                }).ToList(),
                Featured = productSource.Where(x => x.CityName.Contains("台北") || x.CityName.Contains("台南")).Take(8).Select(x => new ProductInfo
                {
                    Id = x.Id,
                    ProductPhoto = x.ProductPhoto,
                    ProductName = x.ProductName,
                    CityName = x.CityName,
                    Favorite = favoriteList.Contains(x.Id),
                    Tags = x.Tags,
                    Rating = x.Rating,
                    TotalOrder = x.TotalOrder,
                    Prices = x.Prices
                }).ToList(),
                Paradise = productSource.Where(x => x.ChildCategoryName.Contains("樂園")).Take(8).Select(x => new ProductInfo
                {
                    Id = x.Id,
                    ProductPhoto = x.ProductPhoto,
                    ProductName = x.ProductName,
                    CityName = x.CityName,
                    Favorite = favoriteList.Contains(x.Id),
                    Tags = x.Tags,
                    Rating = x.Rating,
                    TotalOrder = x.TotalOrder,
                    Prices = x.Prices
                }).ToList(),
                AttractionTickets = productSource.Where(x => MaybeCategoryList(categorySource, "景點").Contains(x.ChildCategoryName)).Take(8).Select(x => new ProductInfo
                {
                    Id = x.Id,
                    ProductPhoto = x.ProductPhoto,
                    ProductName = x.ProductName,
                    CityName = x.CityName,
                    Favorite = favoriteList.Contains(x.Id),
                    Tags = x.Tags,
                    Rating = x.Rating,
                    TotalOrder = x.TotalOrder,
                    Prices = x.Prices
                }).ToList(),
                Exhibition = productSource.Where(x => MaybeCategoryList(categorySource, "展覽").Contains(x.ChildCategoryName)).Take(8).Select(x => new ProductInfo
                {
                    Id = x.Id,
                    ProductPhoto = x.ProductPhoto,
                    ProductName = x.ProductName,
                    CityName = x.CityName,
                    Favorite = favoriteList.Contains(x.Id),
                    Tags = x.Tags,
                    Rating = x.Rating,
                    TotalOrder = x.TotalOrder,
                    Prices = x.Prices
                }).ToList(),
                Hotel = productSource.Where(x => MaybeCategoryList(categorySource, "住宿").Contains(x.ChildCategoryName)).Take(8).Select(x => new ProductInfo
                {
                    Id = x.Id,
                    ProductPhoto = x.ProductPhoto,
                    ProductName = x.ProductName,
                    CityName = x.CityName,
                    Favorite = favoriteList.Contains(x.Id),
                    Tags = x.Tags,
                    Rating = x.Rating,
                    TotalOrder = x.TotalOrder,
                    Prices = x.Prices
                }).ToList(),
                Taoyuan = productSource.Where(x => x.CityName.Contains("桃園")).Take(8).Select(x => new ProductInfo
                {
                    Id = x.Id,
                    ProductPhoto = x.ProductPhoto,
                    ProductName = x.ProductName,
                    CityName = x.CityName,
                    Favorite = favoriteList.Contains(x.Id),
                    Tags = x.Tags,
                    Rating = x.Rating,
                    TotalOrder = x.TotalOrder,
                    Prices = x.Prices
                }).ToList(),
                //TimeLimit =
                Evaluation = productSource.OrderByDescending(x => x.Rating.RatingStar).ThenByDescending(x => x.Rating.TotalGiveComment).Take(8).Select(x => new ProductInfo
                {
                    Id = x.Id,
                    ProductPhoto = x.ProductPhoto,
                    ProductName = x.ProductName,
                    CityName = x.CityName,
                    Favorite = favoriteList.Contains(x.Id),
                    Tags = x.Tags,
                    Rating = x.Rating,
                    TotalOrder = x.TotalOrder,
                    Prices = x.Prices
                }).ToList()
            };

            _iMemoryCacheRepository.Set<ProductDTO>("Product.GetAllProductCard", result);

            return result;
        }

        public ProductDTO PopularCityCard()
        {
            var cityList = _repo.GetAll<City>();

            var result = new ProductDTO { CityList = new List<CityInfo>() };
            result.CityList = cityList.OrderBy(c => Guid.NewGuid()).Take(5).Select(c => new CityInfo
            {
                Id = c.CityId,
                CityName = c.CityName,
                CityImage = c.CityImage
            }).ToList();

            return result;
        }

        public List<string> MaybeCategoryList(List<CategoryInfo> source, string target)
        {
            var temp = source.Where(s => s.Name.Contains(target)).Select(s => s.ChildCategoryList.Select(cc => cc.Name).ToList());
            List<string> result = new List<string>();
            foreach (var item in temp)
            {
                foreach (var i in item)
                {
                    result.Add(i);
                }
            }
            return result;
        }
    }
}
