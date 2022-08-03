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

        public ClassifyDTOinfo GetClassifyPages(ClassifyDTORequest filter)
        {
            var result = new ClassifyDTOinfo() { ClassifyCardList = new List<ClassifyDTO.ClassifyCardInfo>(), CategoryList = new List<CategoryDestinations>() };
            var categoryList = _repo.GetAll<ProductCategory>().ToList();
            var products = _repo.GetAll<Product>().ToList();

            var ProgramList = _repo.GetAll<Today.Model.Models.Program>().ToList();
            var ProgramCantUseDate = _repo.GetAll<ProgramCantUseDate>().ToList();

            filter.RealDate.ForEach(ds =>
            {
                var date = ds.Substring(6, 4) + '-' + ds.Substring(0, 2) + '-' + ds.Substring(3, 2);
                var dateTo = DateTime.Parse(date);
                    ProgramCantUseDate = _repo.GetAll<ProgramCantUseDate>().Where(x => x.Date != dateTo).ToList();
            });

            if (filter.categoryId!=0)
            {
                products = new List<Product>();
                categoryList = _repo.GetAll<ProductCategory>().Where(c => c.CategoryId == filter.categoryId).ToList();
                categoryList.ForEach(c =>
                {
                    var product = _repo.GetAll<Product>().First(p => p.ProductId == c.ProductId);
                    products.Add(product);
                });
            }


            //if (filter.Date != 0)
            //{     
            //    ProgramCantUseDate = _repo.GetAll<ProgramCantUseDate>().Where(x => x.ProgramDateId != filter.Date ).ToList();
            //}
            
            var productPhoto = _repo.GetAll<ProductPhoto>().ToList();
            var city = _repo.GetAll<City>().ToList();
            var tem = (from p in products
                       where p.ProductId  <= 10
                       join y in productPhoto on p.ProductId equals y.ProductId
                       where y.Sort == 1
                       join c in city on p.CityId equals c.CityId
                       join pg in ProgramList on p.ProductId equals pg.ProgramId
                       where pg.ProductId > 0
                       join pc in ProgramCantUseDate on pg.ProgramId equals pc.ProgramDateId
                       group pg by new {  p.ProductId, p.ProductName, c.CityId , c.CityName , y.Path } into g
                    select new
                       {
                            CityId = g.Key.CityId ,
                            CityName = g.Key.CityName,
                            ProductId = g.Key.ProductId,
                            ProductName = g.Key.ProductName,
                            Path = g.Key.Path,
                        }).ToList();

            foreach (var item in tem)
            {
                result.ClassifyCardList.Add(new ClassifyDTO.ClassifyCardInfo() {CityId = item.CityId,CityName = item.CityName, Path = item.Path, ProductName = item.ProductName});
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
        //public ClassifyDTOinfo GetDate (ClassifyDTORequest filter)
        //{
        //    var ProgramCantUseDate = _repo.GetAll<ProgramCantUseDate>().ToList();
        //    if (filter.Date != 0)
        //    {
        //        ProgramCantUseDate = _repo.GetAll<ProgramCantUseDate>().Where(x => x.ProgramDateId != filter.Date).ToList();
        //    }
        //    return ProgramCantUseDate;
        //}
    }

}
