﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels.ProductDTO;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ProductDTO.ProductDTO;

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
            var result = new ProductDTO { ProductList = new List<ProductDTO.ProductInfo> { }, CateoryList = new List<ProductDTO.CategoryInfo> { } };
            var productList = _repo.GetAll<Product>().ToList();
            var productPhotoList = _repo.GetAll<ProductPhoto>().ToList();
            var productCategoryList = _repo.GetAll<ProductCategory>().ToList();
            var categoryList = _repo.GetAll<Category>().ToList();
            var cityList = _repo.GetAll<City>().ToList();
            var productTagList = _repo.GetAll<ProductTag>().ToList();
            var tagList = _repo.GetAll<Tag>().ToList();
            var programList = _repo.GetAll<Model.Models.Program>().ToList();
            var specificationList = _repo.GetAll<ProgramSpecification>().ToList();
            var commentList = _repo.GetAll<Comment>().ToList();
            var orderDetailList = _repo.GetAll<OrderDetail>().ToList();
            var mainCategoryList = categoryList.Where(category => category.ParentCategoryId == null);
            var categoryListGroup = categoryList.Where(category => category.ParentCategoryId != null).GroupBy(category => category.ParentCategoryId);

            //result.cateoryList = new List<CategoryInfo>();

            #region categoryList
            foreach (var category in mainCategoryList)
            {
                var mainTemp = new CategoryInfo
                {
                    Id = category.CategoryId,
                    Name = category.CategoryName,
                    ChildCategoryList = new List<CategoryInfo>()
                };

                foreach (var group in categoryListGroup)
                {
                    if (mainTemp.Id == group.Key)
                    {
                        foreach (var item in group)
                        {
                            var temp = new CategoryInfo
                            {
                                Id = item.CategoryId,
                                Name = item.CategoryName
                            };
                            mainTemp.ChildCategoryList.Add(temp);
                        }
                    }
                }
                result.CateoryList.Add(mainTemp);
            }
            #endregion

            #region productList
            foreach (var product in productList)
            {
                var tempCategoryList = productCategoryList.Where(productCategory => productCategory.ProductId == product.ProductId);
                var tempProductTagList = productTagList.Where(productTag => productTag.ProductId == product.ProductId);
                var totalComment = commentList.Where(comment => comment.ProductId == product.ProductId).Count();
                var sumRating = commentList.Where(comment => comment.ProductId == product.ProductId).Sum(comment => comment.RatingStar);

                var productTemp = new ProductDTO.ProductInfo
                {
                    Id = product.ProductId,
                    ProductPhoto = productPhotoList.Where(photo => photo.ProductId == product.ProductId).Select(x => x.Path).First(),
                    ProductName = product.ProductName,
                    ChildCategoryName = tempCategoryList.Select(productCategory => categoryList.Where(category => category.CategoryId == productCategory.CategoryId).Select(category => category.CategoryName).First()).First(),
                    CityName = string.Join("", cityList.Where(city => city.CityId == product.CityId).Select(city => city.CityName)),
                    Tags = tempProductTagList.Join(tagList, productTag => productTag.TagId, tag => tag.TagId, (productTag, tag) => new { tag.TagText }).Select(tag => tag.TagText).ToList(),
                    Rating = new RatingInfo() { RatingStar = (totalComment != 0) ? (float)sumRating / totalComment : 0, TotalGiveComment = totalComment},
                    TotalOrder = programList.Where(program => program.ProductId == product.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new { program.ProgramId, specification.SpecificationId }).Join(orderDetailList, specification => specification.SpecificationId, orderDetail => orderDetail.SpecificationId, (specification, orderDetail) => new { orderDetail.Quantity }).Sum(n => n.Quantity),
                    Prices = programList.Where(program => program.ProductId == product.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new PriceInfo { OriginalPrice = specification.OriginalUnitPrice, Price = specification.UnitPrice }).OrderBy(specification => specification.Price).FirstOrDefault()
                };

                result.ProductList.Add(productTemp);

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

            }
            #endregion

            return result;
        }

        public ProductDTO test1()
        {
            //var result = new ProductDTO { Featured = new List<ProductInfo>(),  };
            var productList = _repo.GetAll<Product>();
            var productPhotoList = _repo.GetAll<ProductPhoto>();
            var productCategoryList = _repo.GetAll<ProductCategory>();
            var categoryList = _repo.GetAll<Category>();
            var cityList = _repo.GetAll<City>();
            var productTagList = _repo.GetAll<ProductTag>();
            var tagList = _repo.GetAll<Tag>();
            var programList = _repo.GetAll<Model.Models.Program>();
            var specificationList = _repo.GetAll<ProgramSpecification>();
            var commentList = _repo.GetAll<Comment>();
            var orderDetailList = _repo.GetAll<OrderDetail>();
            var mainCategoryList = categoryList.Where(category => category.ParentCategoryId == null);
            var categoryListGroup = categoryList.Where(category => category.ParentCategoryId != null).ToList().GroupBy(category => category.ParentCategoryId);

            //result.cateoryList = new List<CategoryInfo>();

            //#region categoryList
            //foreach (var category in mainCategoryList.AsEnumerable())
            //{
            //    var mainTemp = new CategoryInfo
            //    {
            //        Id = category.CategoryId,
            //        Name = category.CategoryName,
            //        ChildCategoryList = new List<CategoryInfo>()
            //    };

            //    foreach (var group in categoryListGroup)
            //    {
            //        if (mainTemp.Id == group.Key)
            //        {
            //            foreach (var item in group)
            //            {
            //                var temp = new CategoryInfo
            //                {
            //                    Id = item.CategoryId,
            //                    Name = item.CategoryName
            //                };
            //                mainTemp.ChildCategoryList.Add(temp);
            //            }
            //        }
            //    }
            //    result.CateoryList.Add(mainTemp);
            //}
            //#endregion

            //#region productList
            //foreach (var product in productList.AsEnumerable())
            //{
            //    var tempCategoryList = productCategoryList.Where(productCategory => productCategory.ProductId == product.ProductId);
            //    var tempProductTagList = productTagList.Where(productTag => productTag.ProductId == product.ProductId);
            //    var totalComment = commentList.Where(comment => comment.ProductId == product.ProductId).Count();
            //    var sumRating = commentList.Where(comment => comment.ProductId == product.ProductId).Sum(comment => comment.RatingStar);

            //    var productTemp = new ProductDTO.ProductInfo
            //    {
            //        Id = product.ProductId,
            //        ProductPhoto = productPhotoList.Where(photo => photo.ProductId == product.ProductId).Select(x => x.Path).First(),
            //        ProductName = product.ProductName,
            //        ChildCategoryName = tempCategoryList.Select(productCategory => categoryList.Where(category => category.CategoryId == productCategory.CategoryId).Select(category => category.CategoryName).First()).First(),
            //        CityName = string.Join("", cityList.Where(city => city.CityId == product.CityId).Where(city => city.CityName.Contains("台北") || city.CityName.Contains("台南")).Select(city => city.CityName)),
            //        Tags = tempProductTagList.Join(tagList, productTag => productTag.TagId, tag => tag.TagId, (productTag, tag) => new { tag.TagText }).Select(tag => tag.TagText).ToList(),
            //        Rating = new RatingInfo() { RatingStar = (totalComment != 0) ? (float)sumRating / totalComment : 0, TotalGiveComment = totalComment },
            //        TotalOrder = programList.Where(program => program.ProductId == product.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new { program.ProgramId, specification.SpecificationId }).Join(orderDetailList, specification => specification.SpecificationId, orderDetail => orderDetail.SpecificationId, (specification, orderDetail) => new { orderDetail.Quantity }).Sum(n => n.Quantity),
            //        Prices = programList.Where(program => program.ProductId == product.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new PriceInfo { OriginalPrice = specification.OriginalUnitPrice, Price = specification.UnitPrice }).OrderBy(specification => specification.Price).FirstOrDefault()
            //    };

            //    result.Featured.Add(productTemp);
            //}
            //#endregion

            var result = new ProductDTO()
            {
                Featured = productList.Where(x => cityList.Where(c => c.CityName.Contains("台北") || c.CityName.Contains("台南")).Select(c => c.CityId).Contains(x.CityId)).Select(x => new ProductDTO.ProductInfo
                {
                    Id = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPhoto = string.Join("", productPhotoList.Where(pp => pp.ProductId == x.ProductId).Take(1).Select(x => x.Path)),
                    ChildCategoryName = string.Join("", productCategoryList.Where(pc => pc.ProductId == x.ProductId).Join(categoryList, pc => pc.CategoryId, c => c.CategoryId, (pc, c) => new { pc.ProductId, c.CategoryName }).Select(c => c.CategoryName)),
                    CityName = string.Join("", cityList.Where(c => c.CityId == x.CityId).Select(c => c.CityName)),
                    Tags = productTagList.Where(pt => pt.ProductId == x.ProductId).Join(tagList, pt => pt.TagId, t => t.TagId, (pt, t) => new { pt.ProductId, t.TagText }).Select(x => x.TagText).ToList(),
                    Rating = new RatingInfo() { RatingStar = (commentList.Where(comment => comment.ProductId == x.ProductId).Count() != 0) ? (float)commentList.Where(comment => comment.ProductId == x.ProductId).Sum(comment => comment.RatingStar) / commentList.Where(comment => comment.ProductId == x.ProductId).Count() : 0, TotalGiveComment = commentList.Where(comment => comment.ProductId == x.ProductId).Count() },
                    TotalOrder = programList.Where(program => program.ProductId == x.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new { program.ProgramId, specification.SpecificationId }).Join(orderDetailList, specification => specification.SpecificationId, orderDetail => orderDetail.SpecificationId, (specification, orderDetail) => new { orderDetail.Quantity }).Sum(n => n.Quantity),
                    Prices = programList.Where(program => program.ProductId == x.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new PriceInfo { OriginalPrice = specification.OriginalUnitPrice, Price = specification.UnitPrice }).OrderBy(specification => specification.Price).FirstOrDefault()
                }).Take(8).ToList(),
                //Paradise = productList.Where(x => )
            };

            return result;
        }
    }
}
