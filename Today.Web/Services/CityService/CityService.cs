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
        private readonly IProductService _productService;
        public CityService(IGenericRepository repo, IProductService productService)
        {
            _repo = repo;
            _productService = productService;
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
                photo = r.Image,
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
                              select new { c.CityId, m.MemberName, cm.RatingStar, cm.CommentDate, od.DepartureDate, cm.PartnerType, p.ProductName, cm.CommentText, cm.CommentTitle,p.ProductId };

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
                        Text = cm.CommentText,
                        ProductId = cm.ProductId
                        
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
        public CityDTO GetAllCard(CityRequestDTO request)
        {
            var dataSource = _productService.AllProduct().QueryProduct;
            var favoriteList = _repo.GetAll<Collect>().Where(c => c.MemberId == request.MemberId).Select(c => c.ProductId);

            var result = new CityDTO()
            {
                TopProductList = dataSource.Where(p => p.CityId == request.CityId).OrderByDescending(p => p.TotalOrder).Take(10).Select(x => new ProductInfo
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
                NewProductList = dataSource.Where(p => p.CityId == request.CityId).OrderByDescending(p => p.Id).Take(10).Select(x => new ProductInfo
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
                AboutProductList = dataSource.Where(p => p.CityId == request.CityId).OrderBy(x => Guid.NewGuid()).Take(10).Select(x => new ProductInfo
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
            return result;
        }

    }
}
