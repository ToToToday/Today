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
            //var class13 = from c in _repo.GetAll<Category>()
            //              where c.CategoryId == 13
            //              join pc in _repo.GetAll<ProductCategory>() on c.CategoryId equals pc.CategoryId
            //              join p in _repo.GetAll<Product>() on pc.ProductId equals p.ProductId
            //              join pp in _repo.GetAll<ProductPhoto>() on p.ProductId equals pp.ProductId
            //              where pp.Sort == 1
            //              select new HSRVM { ProductName = p.ProductName, Path = pp.Path };
            var class13 = (from p in _repo.GetAll<ProductPhoto>()
                           where p.PhotoId <= 4 && p.Sort == 1
                           select p).Select(h => new HSRVM { Path = h.Path }).ToList();
            //var class14 = (from p in _repo.GetAll<Product>()
            //               where p.ProductId <= 4
            //               select p).Select(h => new HSRVM { ProductName = h.ProductName }).ToList();

            return class13;
            //return class13.ToList();

        }
    }
}
