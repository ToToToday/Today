using Microsoft.EntityFrameworkCore;
using System;
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
        public ProductDTO GetProduct()
        {
            var result = new ProductDTO()
            {
                productList = _repo.GetAll<Product>().Select(x => new ProductDTO.ProductInfo
                {
                    Id = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPhoto = string.Join("", _repo.GetAll<ProductPhoto>().Where(pp => pp.ProductId == x.ProductId).Take(1).Select(x => x.Path)),
                    ChildCategoryName = string.Join("", _repo.GetAll<ProductCategory>().Where(pc => pc.ProductId == x.ProductId).Join(_repo.GetAll<Category>(), pc => pc.CategoryId, c => c.CategoryId, (pc, c) => new {pc.ProductId, c.CategoryName}).Select(c => c.CategoryName)),
                    ParentCategoryName = string.Join("", _repo.GetAll<ProductCategory>().Where(pc => pc.ProductId == x.ProductId).Join(_repo.GetAll<Category>(), pc => pc.CategoryId, c => c.CategoryId, (pc, c) => new { pc.ProductId, c.ParentCategoryId }).Join(_repo.GetAll<Category>(), c => c.ParentCategoryId, cc => cc.CategoryId, (c, cc) => new {cc.CategoryName}).Select(cc => cc.CategoryName)),
                    CityName = string.Join("", _repo.GetAll<City>().Where(c => c.CityId == x.CityId).Select(c => c.CityName)),
                    Tags = _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == x.ProductId).Join(_repo.GetAll<Tag>(), pt => pt.TagId, t => t.TagId, (pt, t) => new { pt.ProductId, t.TagText }).Select(x => x.TagText).ToList(),
                    OriginalPrice = _repo.GetAll<Today.Model.Models.Program>().Where(pm => pm.ProductId == x.ProductId).Join(_repo.GetAll<ProgramSpecification>(), pm => pm.ProgramId, ps => ps.ProgramId, (pm, ps) => new { pm.ProgramId, ps.OriginalUnitPrice, ps.UnitPrice }).Select(ps => ps.OriginalUnitPrice).Min(),
                    Price = _repo.GetAll<Today.Model.Models.Program>().Where(pm => pm.ProductId == x.ProductId).Join(_repo.GetAll<ProgramSpecification>(), pm => pm.ProgramId, ps => ps.ProgramId, (pm, ps) => new { pm.ProgramId, ps.OriginalUnitPrice, ps.UnitPrice }).Select(ps => ps.UnitPrice).Min()
                }).ToList()
            };

            return result;
        }
    }
}
