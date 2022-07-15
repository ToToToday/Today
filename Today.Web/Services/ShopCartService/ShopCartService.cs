using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.Services.CityService;
using Today.Web.ViewModels;

namespace Today.Web.Services.ShopCartService
{

    public class ShopCartService:IShopCartService
    {
      
        private readonly IGenericRepository _repo;
        public ShopCartService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public List<ShopCartVM> GetShopCartCard()
        {
            //var result = _repo.GetAll<City>;

            var card = (from p in _repo.GetAll<Product>()           
                        where p.ProductId <= 2
                             select p).Select(p => new ShopCartVM {Name = p.ProductName }).ToList();
            return card;
        }
    }
}
