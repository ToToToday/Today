using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.Services.ProductService;
using Today.Web.Helper;
using static Today.Web.DTOModels.CityDTO.CityDTO;
using static Today.Web.DTOModels.CityDTO.RaiderDTO;
using City = Today.Model.Models.City;
using Today.Web.CommonEnum;
using Today.Web.DTOModels.CityDTO;
using Today.Web.DTOModels.ProductDTO;
using static Today.Web.DTOModels.ProductDTO.ProductDTO;

namespace Today.Web.Services.CityService
{

    public class CityService : ICityService
    {

        private readonly IGenericRepository _repo;
        public CityService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public CityResponseDTO GetCity(CityRequestDTO request)
        {
            var citysource = _repo.GetAll<City>();
            var result = new CityResponseDTO { CityInfo = new CityDTO.City() };

            var getCity = citysource.Where(x => x.CityId == request.CityId).Select(c => new CityDTO.City
            {
                Id = c.CityId,
                CityName = c.CityName,
                CityImage = c.CityImage,
                CityIntrod = c.CityIntrod

            });
            if (getCity.Any())
            {
                result.CityInfo = getCity.First();

            }


            return result;

        }
        public List<CityDTO.City> GetAllCity(CityRequestDTO request)
        {
            var cityResule = _repo.GetAll<City>().Where(x => x.CityId > request.CityId).Take(10).Select(c=>new CityDTO.City
            {
                CityImage = c.CityImage,
                Id = c.CityId,
                CityIntrod = c.CityIntrod,
                CityName = c.CityName
            }).ToList();
           
            return cityResule;
        }
        public List<RaiderCard> GetRaiderCard(CityRequestDTO request)
        {
            var raiderResult = _repo.GetAll<CityRaider>().Where(x => x.CityId == request.CityId).Select(r =>new CityDTO.RaiderCard
            {
                RaiderId = r.RaidersId,
                CityId = r.CityId,
                Title = r.Title,
                SubTitle = r.Subtitle
            }).ToList();
            

            return raiderResult;
        }
        public List<CommentCard> GetAllComment(CityRequestDTO request)
        {
            Type type = typeof(CommonEnum.AllEnum);

            var CommentData = from cm in _repo.GetAll<Comment>()
                              join od in _repo.GetAll<OrderDetail>() on
                              cm.OrderDetailsId equals od.OrderDetailsId
                              join p in _repo.GetAll<Product>() on
                              cm.ProductId equals p.ProductId
                              join m in _repo.GetAll<Member>() on
                              cm.MemberId equals m.MemberId
                              join c in _repo.GetAll<City>() on
                              p.CityId equals c.CityId
                              select new { c.CityId, m.MemberName, cm.RatingStar, cm.CommentDate, od.DepartureDate, cm.PartnerType, p.ProductName, cm.CommentText, cm.CommentTitle };

            var Comment = CommentData.Where(c => c.CityId == request.CityId).Take(4).ToList();
            var result = new List<CommentCard>();
            if(Comment != null)
            {
                foreach (var cm in Comment)
                {
                    var typeDesc = cm.PartnerType.ToDescription<AllEnum.PartnerType>();
                    var temp = new CityDTO.CommentCard
                    {
                        CityId = cm.CityId,
                        Name = cm.MemberName,
                        RatingStar = cm.RatingStar,
                        CommentDate = cm.CommentDate,
                        UseDate = cm.DepartureDate,
                        PartnerType = typeDesc,
                        ProductName = cm.ProductName,
                        Title = cm.CommentTitle,
                        Text = cm.CommentText
                    };
                    result.Add(temp);

                }
            }
            return result;


        }

        public RaiderResponseDTO GetRaiders(RaiderRequestDTO request)
        {
            var raiderdata = _repo.GetAll<CityRaider>();
            var RaiderPages = new RaiderResponseDTO { RaiderInfo = new RaiderDTO.Raider() };
            var getRaider = raiderdata.Where(r => r.RaidersId == request.RaiderId).Select(rp => new RaiderDTO.Raider
            {
                Id = rp.RaidersId,
                CityId = rp.CityId,
                Title = rp.Title,
                Subtitle = rp.Subtitle,
                Video = rp.Video,
                Text = rp.Text
            });
            if (getRaider.Any())
            {
                RaiderPages.RaiderInfo = getRaider.First();
            }
            return RaiderPages;
        }
        public ProductDTO GetProduct()
        {
            var result = new ProductDTO { ProductList = new List<ProductDTO.ProductInfo> { }, CategoryList = new List<ProductDTO.CategoryInfo> { } };
            var productList = _repo.GetAll<Product>().ToList();
            var productPhotoList = _repo.GetAll<ProductPhoto>().ToList();
            var productCategoryList = _repo.GetAll<ProductCategory>().ToList();
            var categoryList = _repo.GetAll<Category>().ToList();
            var cityList = _repo.GetAll<City>().ToList();
            var productTagList = _repo.GetAll<ProductTag>().ToList();
            var tagList = _repo.GetAll<Tag>().ToList();
            var programList = _repo.GetAll<Model.Models.Program>().ToList();
            var specificationList = _repo.GetAll<ProgramSpecification>().ToList();
            var commentList = _repo.GetAll<Comment>().ToList();
            var orderDetailList = _repo.GetAll<OrderDetail>().ToList();
            var mainCategoryList = categoryList.Where(category => category.ParentCategoryId == null);
            var categoryListGroup = categoryList.Where(category => category.ParentCategoryId != null).GroupBy(category => category.ParentCategoryId);


            #region categoryList
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
                result.CategoryList.Add(mainTemp);
            }
            #endregion

            #region productList
            foreach (var product in productList)
            {
                var tempCategoryList = productCategoryList.Where(productCategory => productCategory.ProductId == product.ProductId);
                var tempProductTagList = productTagList.Where(productTag => productTag.ProductId == product.ProductId);
                var totalComment = commentList.Where(comment => comment.ProductId == product.ProductId).Count();
                var sumRating = commentList.Where(comment => comment.ProductId == product.ProductId).Sum(comment => comment.RatingStar);

                var productTemp = new ProductDTO.ProductInfo
                {
                    Id = product.ProductId,
                    ProductPhoto = productPhotoList.Where(photo => photo.ProductId == product.ProductId).Select(x => x.Path).First(),
                    ProductName = product.ProductName,
                    ChildCategoryName = tempCategoryList.Select(productCategory => categoryList.Where(category => category.CategoryId == productCategory.CategoryId).Select(category => category.CategoryName).First()).First(),
                    CityId = product.CityId,
                    CityName = string.Join("", cityList.Where(city => city.CityId == product.CityId).Select(city => city.CityName)),
                    Tags = tempProductTagList.Join(tagList, productTag => productTag.TagId, tag => tag.TagId, (productTag, tag) => new { tag.TagText }).Select(tag => tag.TagText).ToList(),
                    Rating = new RatingInfo() { RatingStar = (totalComment != 0) ? (float)sumRating / totalComment : 0, TotalGiveComment = totalComment },
                    TotalOrder = programList.Where(program => program.ProductId == product.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new { program.ProgramId, specification.SpecificationId }).Join(orderDetailList, specification => specification.SpecificationId, orderDetail => orderDetail.SpecificationId, (specification, orderDetail) => new { orderDetail.Quantity }).Sum(n => n.Quantity),
                    Prices = programList.Where(program => program.ProductId == product.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new PriceInfo { OriginalPrice = specification.OriginalUnitPrice, Price = specification.UnitPrice }).OrderBy(specification => specification.Price).FirstOrDefault()
                };

                result.ProductList.Add(productTemp);
                #endregion
            }
            return result;
        }
        public List<ProductCard> GetTopTen(CityRequestDTO request)
        {
            var result = GetProduct().ProductList.OrderByDescending(p => p.TotalOrder).Where(c => c.CityId == request.CityId).Take(10).Select(newA => new CityDTO.ProductCard
            {
                Id = newA.Id,
                ProductPhoto = newA.ProductPhoto,
                ProductName = newA.ProductName,
                CityId = newA.CityId,
                CityName = newA.CityName,
                Tags = newA.Tags,
                Rating = newA.Rating.RatingStar,
                TotalComment = newA.Rating.TotalGiveComment,
                Quantity = newA.TotalOrder,
                OriginalPrice = (newA.Prices == null || newA.Prices.OriginalPrice == newA.Prices.Price) ? null : newA.Prices.OriginalPrice,
                Price = (newA.Prices == null) ? null : newA.Prices.Price


            }).ToList();

            return result;
        }
    

        public List<ProductCard> GetNewActiviy(CityRequestDTO request)
        {
            var result = GetProduct().ProductList.OrderByDescending(p => p.Id).Where(c => c.CityId == request.CityId).Take(10).Select(newA => new CityDTO.ProductCard
            {
                Id = newA.Id,
                ProductPhoto = newA.ProductPhoto,
                ProductName = newA.ProductName,
                CityId = newA.CityId,
                CityName = newA.CityName,
                Tags = newA.Tags,
                Rating = newA.Rating.RatingStar,
                TotalComment = newA.Rating.TotalGiveComment,
                Quantity = newA.TotalOrder,
                OriginalPrice = (newA.Prices == null || newA.Prices.OriginalPrice == newA.Prices.Price) ? null : newA.Prices.OriginalPrice,
                Price = (newA.Prices == null) ? null : newA.Prices.Price


            }).ToList();

            return result;
        }
        public List<ProductCard> GetAboutProduct(CityRequestDTO request)
        {
            var Aboutresult = GetProduct().ProductList.OrderBy(x => Guid.NewGuid()).Where(c => c.CityId ==request.CityId).Take(10).Select(Aboutp => new CityDTO.ProductCard
            {
                Id = Aboutp.Id,
                ProductPhoto = Aboutp.ProductPhoto,
                ProductName = Aboutp.ProductName,
                CityId = Aboutp.CityId,
                CityName = Aboutp.CityName,
                Tags = Aboutp.Tags,
                Rating = Aboutp.Rating.RatingStar,
                TotalComment = Aboutp.Rating.TotalGiveComment,
                Quantity = Aboutp.TotalOrder,
                OriginalPrice = (Aboutp.Prices == null || Aboutp.Prices.OriginalPrice == Aboutp.Prices.Price) ? null : Aboutp.Prices.OriginalPrice,
                Price = (Aboutp.Prices == null) ? null : Aboutp.Prices.Price


            }).ToList();

            return Aboutresult;
        }

    }
}
