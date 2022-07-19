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

            var product = _repo.GetAll<Product>().ToList();
            var productPhoto = _repo.GetAll<ProductPhoto>().ToList();
            var city = _repo.GetAll<City>().ToList();

            var tem = (from p in product
                       join y in productPhoto on p.ProductId equals y.ProductId
                       where y.ProductId <= 10 && y.Sort == 1
                       join c in city on p.CityId equals c.CityId
                       select new { ProductName = p.ProductName, CityName = c.CityName, Path = y.Path })
                        .ToList();
            foreach (var item in tem)
            {
                result.ClassifyCardList.Add(new ClassifyDTO.ClassifyCardInfo() {CityName = item.CityName, Path = item.Path, ProductName = item.ProductName });
            }


            var category = _repo.GetAll<Category>().ToList();
            var city1 = _repo.GetAll<City>().ToList();
            var maincategory = category.Where(category => category.ParentCategoryId == null);
            var categoryGroup = category.Where(category => category.ParentCategoryId != null).GroupBy(category => category.ParentCategoryId);
            foreach (var cy in maincategory)
            {
                var mainTemp = new ClassifyDTO.CategoryDestinations()
                {
                    Id = cy.CategoryId,
                    CategoryName = cy.CategoryName,
                    ChildCategory = new List<CategoryDestinations>()

                };

                foreach (var group in categoryGroup)
                {
                    if (mainTemp.Id == group.Key)
                    {
                        foreach (var item in group)
                        {
                            var x = new CategoryDestinations()
                            {
                                Id = item.CategoryId,
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
    }
}
