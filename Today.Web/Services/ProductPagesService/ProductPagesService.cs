//using System.Collections.Generic;
//using Today.Model.Models;
//using Today.Web.ViewModels;
//using System.Linq;
//using Today.Model.Repositories;
//using System;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;

namespace Today.Web.Services.ProductPagesService
{
    public class ProductPagesService : IProductPagesService
    {
        private readonly IGenericRepository _repo;
        public ProductPagesService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public List<ProductPagesVM> GetProductPages()
        {
            var ID = 5;
            //var ProductID = _repo.GetAll<Product>().Where(p => p.ProductId <= ID).Select(p=>p.ProductId);
            //var product = (from p in _repo.GetAll<Product>()
            //               where p.ProductId == ID
            //               join pc in _repo.GetAll<ProductPhoto>() on p.ProductId equals pc.ProductId
            //               select p).Select(p => new ProductPagesVM { ProductId = p.ProductId, ProductName = p.ProductName, PotoUrl = p. }).ToList();
            //var product = (from p in _repo.GetAll<ProductPhoto>()
            //               where p.ProductId <= 4 && p.Sort == 1
            //               select p).Select(p => new ProductPagesVM { PotoUrl = p.Path }).ToList();

            //var product = new ProductPagesVM
            //{
            //    ProductId = ProductID
            //};
            var pw = from p in _repo.GetAll<Product>()
                     where p.ProductId == ID
                     join pc in _repo.GetAll<ProductPhoto>() on p.ProductId equals pc.ProductId
                     select new ProductPagesVM { productId = p.ProductId };

            var product = from p in _repo.GetAll<Product>()
                          where p.ProductId == ID
                          join ct in _repo.GetAll<City>() on p.CityId equals ct.CityId
                          join pg in _repo.GetAll<Today.Model.Models.Program>() on p.ProductId  equals pg.ProductId
                          join apgo in _repo.GetAll<AboutProgramOption>() on p.ProductId equals apgo.ProductId
                          join apg in _repo.GetAll<AboutProgram>() on pg.ProgramId equals apg.ProgramId
                          join pgld in _repo.GetAll<ProgramInclude>() on pg.ProgramId equals pgld.ProgramId
                          join pgsf in _repo.GetAll<ProgramSpecification>() on pg.ProgramId equals pgsf.ProgramId
                          //join od in _repo.GetAll<OrderDetail>() on pgsf.SpecificationId equals od.SpecificationId
                          //join o in _repo.GetAll<Order>() on od.OrderId equals o.OrderId
                          join pc in _repo.GetAll<ProductPhoto>() on p.ProductId equals pc.ProductId
                          //join lc in _repo.GetAll<Location>() on p.ProductId equals lc.ProductId
                          //join mess in _repo.GetAll<Message>() on o.OrderId equals mess.OrderId
                          //join cud in _repo.GetAll<ProgramCantUseDate>() on pg.ProgramId equals cud.ProgramId
                          //join ptg in _repo.GetAll<ProductTag>() on p.ProductId equals ptg.ProductId
                          //join pt in _repo.GetAll<Tag>() on p.ProductTags equals pt.ProductTags
                          //join meb in _repo.GetAll<Member>() on o.MemberId equals meb.MemberId
                          select new ProductPagesVM { 
                              productId = p.ProductId, 
                              productname = p.ProductName ,
                              //producttag = pt.TagText,
                              cityname = ct.CityName,
                              potourl = pc.Path,
                              porgramname = pg.Title,
                              porgarmunitprice = pgsf.UnitPrice,
                              producttext = pg.Context,
                              aboutprogram = apgo.Context,
                              programinciudefalse = pgld.Text,
                          };

            return product.ToList();
        }
    }
}
