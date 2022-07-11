using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;

namespace Today.Web.Services.CityService
{

    public class CityService:ICityService
    {
      
        private readonly IGenericRepository _repo;
        public CityService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public List<CityVM> GetCityPages(int id)
        {
            
            var citypages = (from cp in _repo.GetAll<City>()
                             where cp.CityId == id
                             select cp).Select(c => new CityVM { Id = c.CityId, CityName = c.CityName, CityImage = c.CityImage, CityIntrod = c.CityIntrod }).ToList();
            return citypages;

        }
        public List<RaiderVM> GetRaiders(int id)
        {
            var RaiderPages = (from rp in _repo.GetAll<CityRaider>()
                               where rp.RaidersId == id
                               select rp).Select(r => new RaiderVM { Id = r.RaidersId, CityId = r.CityId, Title = r.Title, Subtitle = r.Subtitle, Text = r.Text, Video = r.Video }).ToList();
            return RaiderPages;
        }
    }
}
