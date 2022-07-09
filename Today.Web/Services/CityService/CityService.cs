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
        public List<CityVM> GetCityPages()
        {
            
            var citypages = (from cp in _repo.GetAll<City>()
                             where cp.CityId == 24
                             select cp).Select(c => new CityVM { Id = c.CityId, CityName = c.CityName, CityImage = c.CityImage, CityIntrod = c.CityIntrod }).ToList();
            return citypages;
        }
    }
}
