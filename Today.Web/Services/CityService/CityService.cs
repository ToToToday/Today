using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels;
using Today.Web.Services.CommonEnum;
using Today.Web.Helper;
using static Today.Web.DTOModels.CityDTO;
using static Today.Web.DTOModels.RaiderDTO;
using City = Today.Model.Models.City;

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
        public List<CityDTO.City> GetAllCity()
        {
            var cityResule = _repo.GetAll<City>().Select(c=>new CityDTO.City
            {
                CityImage = c.CityImage,
                Id = c.CityId,
                CityIntrod = c.CityIntrod,
                CityName = c.CityName
            }).ToList();
           
            return cityResule;
        }
        public List<RaiderCard> GetRaiderCard()
        {
            var raiderResult = _repo.GetAll<CityRaider>().Select(r =>new CityDTO.RaiderCard
            {
                CityId = r.CityId,
                Title = r.Title,
                SubTitle = r.Subtitle
            }).ToList();
            

            return raiderResult;
        }
        public List<CommentCard> GetAllComment()
        {
            Type type = typeof(CityEnum);

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

            
           var result = new List<CommentCard>();
            if(CommentData != null)
            {
                foreach (var cm in CommentData)
                {
                    var typeDesc = EnumHelper.GetTypeDescription(cm.PartnerType);
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

    }
}
