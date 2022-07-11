using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.CityDTO;
using static Today.Web.DTOModels.RaiderDTO;
using City = Today.Model.Models.City;

namespace Today.Web.Services.CityService
{

    public class CityService:ICityService
    {
      
        private readonly IGenericRepository _repo;
        public CityService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public CityResponseDTO GetCity(CityRequestDTO request)
        {
            var cinfo = _repo.GetAll<City>();
            var result = new CityResponseDTO { CityInfo = new CityDTO.City() };

           var getCity = cinfo.Where(x => x.CityId == request.CityId).Select(c => new CityDTO.City
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
        public RaiderResponseDTO GetRaiders(RaiderRequestDTO request)
        {
            var raiderdata = _repo.GetAll<CityRaider>();
            var RaiderPages = new RaiderResponseDTO { RaiderInfo = new RaiderDTO.Raider()};
            var getRaider = raiderdata.Where(r => r.RaidersId == request.RaiderId).Select(rp => new RaiderDTO.Raider
            {
                Id = rp.RaidersId,
                CityId = rp.CityId,
                Title = rp.Title,
                Subtitle = rp.Subtitle,
                Video = rp.Video,
                Text = rp.Text
            });
            if(getRaider.Any())
            {
                RaiderPages.RaiderInfo = getRaider.First();
            }


            //var RaiderPages = (from rp in _repo.GetAll<CityRaider>()
            //                   where rp.RaidersId == id
            //                   select rp).Select(r => new RaiderVM { Id = r.RaidersId, CityId = r.CityId, Title = r.Title, Subtitle = r.Subtitle, Text = r.Text, Video = r.Video }).ToList();
            return RaiderPages;
        }

    }
}
