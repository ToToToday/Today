using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;


namespace Today.Web.Services.ClassifyService
{
    public class ClassifyService : IClassifyService
    {
        private readonly IGenericRepository _repository;

        public ClassifyService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public List<ClassifyVM> GetClassifyPages()
        {
            //var product = _repository.GetAll<Product>().OrderBy(x => x.ProductId);
            //var pic = _repository.GetAll<ProductPhoto>().Where(y => /*x.ProductId <= 10 &&*/ y.Sort == 1);
            //var newpro = product.Join(pic, x => x.ProductId, y => y.ProductId, (x, y) => new { x.ProductId, x.ProductName, x.ProductTags, y.Path });


            //var tag = _repository.GetAll<ProductTag>();
            //var tagList = _repository.GetAll<Tag>();
            //var tagDetails = tag.Join(tagList, c => c.TagId, b => b.TagId, (c, b) => new { c.ProductId, b.TagText });

            //var pic = _repository.GetAll<ProductPhoto>().Where(x=>x.Sort == 1).Take(8);

            //var p1 = _repository.GetAll<Product>().Where(p => p.ProductId <= 8);
            //var p2 = _repository.GetAll<ProductPhoto>().Where(pp => pp.Sort == 1);
            //var p3 = _repository.GetAll<Tag>().Where(pp => pp.ProductId == 9);
            //var p4 = _repository.GetAll<City>().Where(cn => cn.CityId <= 8);
            //var p5 = _repository.GetAll<ProgramSpecification>().Where(cn => cn.CityId <= 8);

            //var PN = (from x in _repository.GetAll<ProductPhoto>().Where(x => /*x.ProductId <= 10 &&*/ x.Sort == 1)
            //          join p in _repository.GetAll<Product>().OrderBy(x => x.ProductName) on x.ProductId equals p.ProductId
            //          join pt in _repository.GetAll<ProductTag>() on p.ProductId equals pt.ProductId
            //          //join pp in pic on y.Path equals
            //          //join t in _repository.GetAll<Tag>().Where(pt => pt.TagId <= 8) on pt.TagId equals t.TagId
            //          //() on pt.TagId equals t.TagId.Where (t => t.TagText <= 8)
            //          join c in _repository.GetAll<City>() on p.CityId equals c.CityId


            //          //join t in _repository.GetAll<Tag>() on p.ProductId equals t.Tag
            //          //where pp.Sort == 1

            //          select new ClassifyVM { ProductName = p.ProductName, Path = x.Path, CityName = c.CityName,/* TagText = t.TagText*/ })
            //                     .ToList();


            //var cat in _repository.GetAll<Category>().Where(cat =>cat.CategoryName)

            var CN = (from p in _repository.GetAll<Product>()
                      join y in _repository.GetAll<ProductPhoto>() on p.ProductId equals y.ProductId
                      where y.ProductId <= 10 && y.Sort == 1
                      join c in _repository.GetAll<City>() on p.CityId equals c.CityId
                      where p.ProductId <= 8
                      select new ClassifyVM { ProductName = p.ProductName, CityName = c.CityName, Path = y.Path })
                                 .ToList();
            return CN;
        }

        public List<ClassifyVM> GetClassifydestination()
        {
            var des = (from p in _repository.GetAll<Product>()
                       join pc in _repository.GetAll<ProductCategory>() on p.ProductId equals pc.CategoryId
                       join c in _repository.GetAll<Category>() on pc.CategoryId equals c.CategoryId
                       where c.ParentCategoryId >= 13
                       select new ClassifyVM { CategoryName = c.CategoryName/*, ParentCategoryId = c.ParentCategoryId*/ })
                                 .ToList();
            var query = des.Skip(10)/*.Take(10)*/.ToList();
            return query;

        }
        //public async Task<IActionResult> GetClassifyPages()
        //{
        //    var classifypages = await (from pro in _repository.GetAll<ClassifyService>()
        //                         select new ClassifyVM
        //                         {
        //                             ProductName = pro.ProductName,
        //                             Path = pro.Path,
        //                             TagText = pro.TagText,
        //                             CityName = pro.CityName,
        //                             UnitPrice = pro.UnitPrice
        //                         }).ToListAsync();
        //    return classifypages;
        //}







    }

}
