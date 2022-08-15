using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;
using Today.Web.DTOModels.ClassifyDTO;
using System;
using System.Web;
using Today.Web.Services.ProductService;
using Comment = Today.Model.Models.Comment;
using static Today.Web.DTOModels.ClassifyDTO.ClassifyDTO;
using static Today.Web.ViewModels.ClassifyVM;
using static Today.Web.ViewModels.ClassifyVM.ClassifyCardInfo;

namespace Today.Web.Services.ClassifyService
{
    public class ClassifyService : IClassifyService
    {
        private readonly IGenericRepository _repo;
        public ClassifyService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public ClassifyDTO GetClassifyPages(ClassifyRequestDTO input)
        {
            var memberId = input.MemberId;
            var result = new ClassifyDTO()
            {
                CategoryList = new List<ClassifyDTO.CategoryDestinations>(),
            };

            var categoryList = _repo.GetAll<Category>();
            var productCategoryList = _repo.GetAll<ProductCategory>();
            var productList = _repo.GetAll<Product>();

            if (input.CategoryId != 0)
            {
                productList = productList.Where(p =>
                    _repo.GetAll<ProductCategory>()
                        .Any(pc =>
                            pc.ProductId == p.ProductId &&
                            pc.CategoryId == input.CategoryId)
                );
            };

            result.CardCount = productList.Count();
            result.ClassifyCardList = AddClassifyCardToResult(productList
                .Skip(10 * (input.Page - 1))
                .Take(10)
                .ToList()
            , memberId);


            var category = _repo.GetAll<Category>().ToList();
            var city1 = _repo.GetAll<City>();
            var maincategory = category.Where(category => category.ParentCategoryId == null);
            var categoryGroup = category.Where(category => category.ParentCategoryId != null).GroupBy(category => category.ParentCategoryId);
            foreach (var cy in maincategory)
            {
                var mainTemp = new ClassifyDTO.CategoryDestinations()
                {
                    ProductCategoryId = cy.CategoryId,
                    CategoryName = cy.CategoryName,
                    ChildCategory = new List<ClassifyDTO.CategoryDestinations>()
                };
                foreach (var group in categoryGroup)
                {
                    if (mainTemp.ProductCategoryId == group.Key)
                    {
                        foreach (var item in group)
                        {
                            var x = new ClassifyDTO.CategoryDestinations()
                            {
                                ProductCategoryId = item.CategoryId,
                                CategoryName = item.CategoryName,
                            };
                            mainTemp.ChildCategory.Add(x);
                        }
                    }
                }
                result.CategoryList.Add(mainTemp);
            }
            return result;
        }
        public ClassifyDTO GetClassifyMatchedProducts(FilterDTO input)
        {
            var result = new ClassifyDTO() { };

            var productList = _repo.GetAll<Product>();
            if (input.CityFilterList.Any())
            {
                productList = productList.Where(p => input.CityFilterList.Contains(p.CityId));
            }

            if (input.CategoryFilterList.Any())
            {
                productList = productList.Where(p =>
                _repo.GetAll<ProductCategory>()
                    .Where(pc => pc.ProductId == p.ProductId)
                    .Select(pc => pc.CategoryId)
                    .Any(cId => input.CategoryFilterList.Contains(cId))
                );
            }

            result.CardCount = productList.Count();
            result.ClassifyCardList = AddClassifyCardToResult(productList
                .Skip(10 * (input.Page - 1))
                .Take(10)
                .ToList()
            , input.MemberId);

            return result;
        }

        private List<ClassifyVM.ClassifyCardInfo> AddClassifyCardToResult(List<Product> product, int memberId)
        {
            var result = new List<ClassifyVM.ClassifyCardInfo>();

            var productPhoto1 = _repo.GetAll<ProductPhoto>().Where(pp => product.Select(p => p.ProductId).Contains(pp.ProductId));
            var city = _repo.GetAll<City>().Where(c => product.Select(p => p.CityId).Contains(c.CityId));
            var productTag = _repo.GetAll<ProductTag>();
            var Tag = _repo.GetAll<Tag>();
            var comment = _repo.GetAll<Model.Models.Comment>();
            var programList = _repo.GetAll<Model.Models.Program>();
            var orderDetailList = _repo.GetAll<OrderDetail>();
            var specificationList = _repo.GetAll<ProgramSpecification>();
            var UnitPriceList = new List<ClassifyVM.ClassifyCardInfo>();
            var favoriteList = _repo.GetAll<Collect>().Where(c => c.MemberId == memberId).Select(c => c.ProductId);

            product.ForEach(p => result.Add(
                new ClassifyCardInfo
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CityId = p.CityId,
                }));

            result.ForEach(p =>
            {
                p.Path = _repo.GetAll<ProductPhoto>().First(ph => ph.ProductId == p.ProductId).Path.ToString();
                p.CityName = _repo.GetAll<City>().First(c => c.CityId == p.CityId).CityName.ToString();
                p.Favorite = favoriteList.Contains(p.ProductId);
                p.TagText = _repo.GetAll<Tag>()
                    .Where(t => _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == p.ProductId)
                    .Select(pt => pt.TagId)
                    .Contains(t.TagId))
                    .Select(t => t.TagText).ToList();
                p.RatingStar = (comment.Where(c => c.ProductId == p.ProductId)
                                       .Count() != 0) ? Math.Round((double)comment.Where(c => c.ProductId == p.ProductId)
                                       .Sum(c => c.RatingStar) / comment.Where(c => c.ProductId == p.ProductId).Count(), 1) : 0;
                p.TotalComment = comment.Where(c => c.ProductId == p.ProductId).Count();
                p.TotalOrder = programList.Where(program => program.ProductId == p.ProductId)
                                    .Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new { program.ProgramId, specification.SpecificationId })
                                    .Join(orderDetailList, specification => specification.SpecificationId, orderDetail => orderDetail.SpecificationId, (specification, orderDetail) => new { orderDetail.Quantity })
                                    .Sum(n => n.Quantity);
                p.Prices = programList.Where(program => program.ProductId == p.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new PriceInfo { OriginalPrice = specification.OriginalUnitPrice, Price = specification.UnitPrice }).OrderBy(specification => specification.Price).FirstOrDefault();
            });
            return result;
         }
    }
}



