using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;

namespace Today.Web.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository _repo;
        public ProductService (IGenericRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ProductCardVM> GetProductCard()
        {
            var productViews = _repo.GetAll<Product>().Select(x => new ProductCardVM
            {
                ProductPhoto =  _repo.GetAll<ProductPhoto>().Where(pp => pp.ProductId == x.ProductId).Select(x => x.Path).FirstOrDefault(),
                ProductName = x.ProductName,
                CityName = string.Join("", _repo.GetAll<City>().Where(c => c.CityId == x.CityId).Select(c => c.CityName)),
                Tags = _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == x.ProductId).Join(_repo.GetAll<Tag>(), pt => pt.TagId, t => t.TagId, (pt, t) => new { pt.ProductId, t.TagText }).Select(x => x.TagText).ToList(),
                OriginalPrice = _repo.GetAll<Today.Model.Models.Program>().Where(pm => pm.ProductId == x.ProductId).Join(_repo.GetAll<ProgramSpecification>(), pm => pm.ProgramId, ps => ps.ProgramId, (pm, ps) => new { pm.ProductId, ps.OriginalUnitPrice, ps.UnitPrice }).Select(ps => ps.OriginalUnitPrice).Min(),
                Price = _repo.GetAll<Today.Model.Models.Program>().Where(pm => pm.ProductId == x.ProductId).Join(_repo.GetAll<ProgramSpecification>(), pm => pm.ProgramId, ps => ps.ProgramId, (pm, ps) => new { pm.ProductId, ps.OriginalUnitPrice, ps.UnitPrice }).Select(ps => ps.UnitPrice).Min()

            }) ;

            return productViews;
        }
    }
}
