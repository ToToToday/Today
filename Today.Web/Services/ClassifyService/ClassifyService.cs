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

namespace Today.Web.Services.ClassifyService
{
    public class ClassifyService : IClassifyService
    {
        private readonly IGenericRepository _repo;

        public ClassifyService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public ClassifyDTO GetClassifyPages()
        {
            var result = new ClassifyDTO() { ClassifyCardList = new List<ClassifyDTO.ClassifyCardInfo>(), CategoryList = new List<CategoryDestinations>() };

            var product = _repo.GetAll<Product>();
            var productPhoto = _repo.GetAll<ProductPhoto>();
            var city = _repo.GetAll<City>();
            var productTag = _repo.GetAll<ProductTag>();
            var tag = _repo.GetAll<Tag>();
            var comment = _repo.GetAll<Comment>();

            var tem = (from p in product
                       join y in productPhoto on p.ProductId equals y.ProductId
                       where /*y.ProductId == 1 &&*/ y.Sort == 1
                       join c in city on p.CityId equals c.CityId
                       join pt in productTag on p.ProductId equals pt.ProductId
                       join t in tag on pt.TagId equals t.TagId
                       //where rs.ProductId <= 10 && y.Sort == 1
                       select new { ProductId = p.ProductId, ProductName = p.ProductName, CityName = c.CityName, Path = y.Path, TagText = t.TagText })
                        .ToList();

            var tem2 = (from p in product
                        join rs in comment on p.ProductId equals rs.ProductId
                        select new { ProductId = p.ProductId, RatingStar = rs.RatingStar });
            var commentdata = product.Select(x => new { ProductId = x.ProductId, /*TotalStar = comment.Where(c => c.ProductId == x.ProductId).Sum(c => c.RatingStar),*/ TotalComment = comment.Where(c => c.ProductId == x.ProductId).Count(), AvgStar = (comment.Where(c => c.ProductId == x.ProductId).Count() != 0) ? (float)comment.Where(c => c.ProductId == x.ProductId).Sum(c => c.RatingStar) / comment.Where(c => c.ProductId == x.ProductId).Count() : 0 });

            var tempproduct = string.Empty;
            var temptag = new List<string>();
            foreach (var item in tem)
            {
                if (tempproduct == "" || tempproduct != item.ProductName)
                {
                    temptag.Clear();
                    tempproduct = item.ProductName;
                    temptag.Add(item.TagText);
                }
                else
                {
                    temptag.Add(item.TagText);
                    var totalstartemp = commentdata.Where(x => x.ProductId == item.ProductId).Select(x => x.AvgStar).First();
                    var totalComment = commentdata.Where(x => x.ProductId == item.ProductId).Select(x => x.TotalComment).First();
                    result.ClassifyCardList.Add(new ClassifyDTO.ClassifyCardInfo() {
                       ProductId = item.ProductId , CityName = item.CityName, Path = item.Path, ProductName = item.ProductName, TagText = temptag, RatingStar = totalstartemp, TotalComment = totalComment });
                }
            }

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

        //篩選邏輯
        public ClassifyDTO GetClassifylogic()
        {
            throw new System.NotImplementedException();
        }

    }
}
