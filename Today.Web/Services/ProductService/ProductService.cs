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
        public class CategoryDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<CategoryDto> ChildCategoryList { get; set; }
        }
        public ProductDTO GetProduct()
        {
            var result = new ProductDTO { productList = new List<ProductDTO.ProductInfo> { } };
            var productList = _repo.GetAll<Product>().ToList();
            var productPhotoList = _repo.GetAll<ProductPhoto>().ToList();
            var productCategoryList = _repo.GetAll<ProductCategory>().ToList();
            var CategoryList = _repo.GetAll<Category>().ToList();
            var cityList = _repo.GetAll<City>().ToList();
            var TagList = _repo.GetAll<ProductTag>().ToList();
            var programList = _repo.GetAll<Model.Models.Program>().ToList();
            var specificationList = _repo.GetAll<ProgramSpecification>().ToList();
            var mainCategoryList = CategoryList.Where(category => category.ParentCategoryId == null);
            var CategoryListGroup = CategoryList.Where(category => category.ParentCategoryId != null).GroupBy(category => category.ParentCategoryId);

            var CategoryDtoList = new List<CategoryDto>();
            #region 組合Category
            foreach (var category in mainCategoryList)
            {
                var mainTemp = new CategoryDto
                {
                    Id = category.CategoryId,
                    Name = category.CategoryName,
                    ChildCategoryList = new List<CategoryDto>()

                };

                foreach (var group in CategoryListGroup)
                {
                    if (mainTemp.Id == group.Key)
                    {
                        foreach (var item in group)
                        {
                            var temp = new CategoryDto
                            {
                                Id = item.CategoryId,
                                Name = item.CategoryName
                            };
                            mainTemp.ChildCategoryList.Add(temp);
                        }

                    }

                }
                CategoryDtoList.Add(mainTemp);
            }
            #endregion

            foreach (var product in productList)
            {
                var tempCategoryList = productCategoryList.Where(productCategory => productCategory.ProductId == product.ProductId);

                var productTemp = new ProductDTO.ProductInfo
                {
                    Id = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPhoto = productPhotoList.Where(photo => photo.ProductId == product.ProductId).Select(x => x.Path).First(),
                    ChildCategoryName = tempCategoryList.Select(productCategory => CategoryList.Where(category => category.CategoryId == productCategory.CategoryId).Select(category => category.CategoryName).First()).First()

                };

                //}
                //var result = new ProductDTO()
                //{
                //    productList = _repo.GetAll<Product>().Select(x => new ProductDTO.ProductInfo
                //    {
                //        Id = x.ProductId,
                //        ProductName = x.ProductName,
                //        ProductPhoto = string.Join("", _repo.GetAll<ProductPhoto>().Where(pp => pp.ProductId == x.ProductId).Take(1).Select(x => x.Path)),
                //        ChildCategoryName = string.Join("", _repo.GetAll<ProductCategory>().Where(pc => pc.ProductId == x.ProductId).Join(_repo.GetAll<Category>(), pc => pc.CategoryId, c => c.CategoryId, (pc, c) => new {pc.ProductId, c.CategoryName}).Select(c => c.CategoryName)),
                //        ParentCategoryName = string.Join("", _repo.GetAll<ProductCategory>().Where(pc => pc.ProductId == x.ProductId).Join(_repo.GetAll<Category>(), pc => pc.CategoryId, c => c.CategoryId, (pc, c) => new { pc.ProductId, c.ParentCategoryId }).Join(_repo.GetAll<Category>(), c => c.ParentCategoryId, cc => cc.CategoryId, (c, cc) => new {cc.CategoryName}).Select(cc => cc.CategoryName)),
                //        CityName = string.Join("", _repo.GetAll<City>().Where(c => c.CityId == x.CityId).Select(c => c.CityName)),
                //        Tags = _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == x.ProductId).Join(_repo.GetAll<Tag>(), pt => pt.TagId, t => t.TagId, (pt, t) => new { pt.ProductId, t.TagText }).Select(x => x.TagText).ToList(),
                //        OriginalPrice = _repo.GetAll<Today.Model.Models.Program>().Where(pm => pm.ProductId == x.ProductId).Join(_repo.GetAll<ProgramSpecification>(), pm => pm.ProgramId, ps => ps.ProgramId, (pm, ps) => new { pm.ProgramId, ps.OriginalUnitPrice, ps.UnitPrice }).Select(ps => ps.OriginalUnitPrice).Min(),
                //        Price = _repo.GetAll<Today.Model.Models.Program>().Where(pm => pm.ProductId == x.ProductId).Join(_repo.GetAll<ProgramSpecification>(), pm => pm.ProgramId, ps => ps.ProgramId, (pm, ps) => new { pm.ProgramId, ps.OriginalUnitPrice, ps.UnitPrice }).Select(ps => ps.UnitPrice).Min()
                //    }).ToList()
                //};

                return result;
        }
    }
}
