using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;

namespace Today.Web.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository _repo;
        public OrderService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public List<OrderVM> GetOrderpages()
        {
            
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
