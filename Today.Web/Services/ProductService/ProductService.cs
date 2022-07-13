using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTO;
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

        //public List<FilterVM> Filter()
        //{
        //    List<FilterVM> filterList = new List<FilterVM>()
        //    {
        //        new FilterVM{ Name = "最近瀏覽", Method = this.ViewedLatest()},
        //        new FilterVM{ Name = "為你精選", Method = this.GetProductCard()}
        //    };
        //    return filterList;
        //}

        //public List<ProductDTO> Card()
        //{
        //    List<ProductDTO> cards = new List<ProductDTO>()
        //    {
        //        new ProductDTO
        //        {
        //            Id = 
        //        }
        //    }
        //}
        public List<ProductDTO> GetProduct()
        {
            var productViews =  _repo.GetAll<Product>().Select(x => new ProductDTO
            {
                productList = new List<ProductDTO.ProductInfo>()
                {
                    new ProductDTO.ProductInfo()
                    {
                        Id = x.ProductId,
                        ProductName = x.ProductName,
                        ProductPhoto = string.Join("", _repo.GetAll<ProductPhoto>().Where(pp => pp.ProductId == x.ProductId).Take(1).Select(x => x.Path)),
                        CityName = string.Join("", _repo.GetAll<City>().Where(c => c.CityId == x.CityId).Select(c => c.CityName)),
                        Tags = _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == x.ProductId).Join(_repo.GetAll<Tag>(), pt => pt.TagId, t => t.TagId, (pt, t) => new { pt.ProductId, t.TagText }).Select(x => x.TagText).ToList(),
                        OriginalPrice = _repo.GetAll<Today.Model.Models.Program>().Where(pm => pm.ProductId == x.ProductId).Join(_repo.GetAll<ProgramSpecification>(), pm => pm.ProgramId, ps => ps.ProgramId, (pm, ps) => new { pm.ProductId, ps.OriginalUnitPrice, ps.UnitPrice }).Select(ps => ps.OriginalUnitPrice).Min(),
                        Price = _repo.GetAll<Today.Model.Models.Program>().Where(pm => pm.ProductId == x.ProductId).Join(_repo.GetAll<ProgramSpecification>(), pm => pm.ProgramId, ps => ps.ProgramId, (pm, ps) => new { pm.ProductId, ps.OriginalUnitPrice, ps.UnitPrice }).Select(ps => ps.UnitPrice).Min()
                    }
                }
            }).ToList();

            return productViews;
        }
    }
}
