using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;

namespace Today.Web.Services.OrderService
{
    public class HSRService : IHSRService
    {
        private readonly IGenericRepository _repo;
        public HSRService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public List<HSRVM> GetHSRpages()
        {
            //var result = _repo.GetAll<City>;
            var hsrpageone = (from hp in _repo.GetAll<Product>()
                            select hp).Select(h => new HSRVM { ProductName = h.ProductName }).ToList();
            

        }


        //private readonly IGenericRepository _repo;
        //public CityService(IGenericRepository repo)
        //{
        //    _repo = repo;
        //}
        //public List<CityVM> GetCityPages()
        //{
        //    //var result = _repo.GetAll<City>;
        //    var citypages = (from cp in _repo.GetAll<City>()
        //                     where cp.CityId == 12
        //                     select cp).Select(c => new CityVM { Id = c.CityId, CityName = c.CityName, CityImage = c.CityImage, CityIntrod = c.CityIntrod }).ToList();
        //    return citypages;
        //}
    }
}
