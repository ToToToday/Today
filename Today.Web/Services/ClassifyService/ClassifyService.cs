using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;
using Today.Web.DTOModels.ClassifyDTO;
using static Today.Web.DTOModels.ClassifyDTO.ClassifyDTO;
using System;
using PagedList;
using System.Web;
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

            var result = new ClassifyDTO()
            {
                CategoryList = new List<CategoryDestinations>(),

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
            );


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
                    ChildCategory = new List<CategoryDestinations>()
                };
                foreach (var group in categoryGroup)
                {
                    if (mainTemp.ProductCategoryId == group.Key)
                    {
                        foreach (var item in group)
                        {
                            var x = new CategoryDestinations()
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
            );

            return result;
        }


        private List<ClassifyDTO.ClassifyCardInfo> AddClassifyCardToResult(List<Product> product)
        {

            var result = new List<ClassifyDTO.ClassifyCardInfo>();


            var productPhoto1 = _repo.GetAll<ProductPhoto>().Where(pp => product.Select(p => p.ProductId).Contains(pp.ProductId));
            var city = _repo.GetAll<City>().Where(c => product.Select(p => p.CityId).Contains(c.CityId));

            var productTag = _repo.GetAll<ProductTag>();
            var Tag = _repo.GetAll<Tag>();

            var comment = _repo.GetAll<Comment>();


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

                p.TagText = _repo.GetAll<Tag>()
                    .Where(t => _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == p.ProductId)
                    .Select(pt => pt.TagId)
                    .Contains(t.TagId))
                    .Select(t => t.TagText).ToList();
            });
            var commentdata = product.Select(x => new
            {
                ProductId = x.ProductId,
                TotalComment = comment.Where(c => c.ProductId == x.ProductId).Count(),
                AvgStar = (comment.Where(c => c.ProductId == x.ProductId)
                    .Count() != 0) ? (float)comment.Where(c => c.ProductId == x.ProductId)
                    .Sum(c => c.RatingStar) / comment.Where(c => c.ProductId == x.ProductId).Count() : 0
            });

            var tempproduct = string.Empty;
            var temptag = new List<string>();

            foreach (var item in result)
            {
                if (tempproduct == "" || tempproduct != item.ProductName)
                {
                    temptag.Clear();
                    tempproduct = item.ProductName;
                    temptag.Add(item.TagText.ToString());
                }
                else
                {
                    temptag.Add(item.TagText.ToString());
                    var totalstartemp = commentdata.Where(x => x.ProductId == item.ProductId).Select(x => x.AvgStar).First();
                    var totalComment = commentdata.Where(x => x.ProductId == item.ProductId).Select(x => x.TotalComment).First();
                    result.Add(new ClassifyDTO.ClassifyCardInfo()
                    {
                        ProductId = item.ProductId,
                        CityId = item.CityId,
                        CityName = item.CityName,
                        Path = item.Path,
                        ProductName = item.ProductName,
                        TagText = temptag,
                        RatingStar = totalstartemp,
                        TotalComment = totalComment
                    });
                }
            }
            return result;
        }

    }
}
