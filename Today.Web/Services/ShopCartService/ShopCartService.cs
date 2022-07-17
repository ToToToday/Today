using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels;
using Today.Web.Services.CityService;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ShopCartMemberDTO;

namespace Today.Web.Services.ShopCartService
{

    public class ShopCartService:IShopCartService
    {
      
        private readonly IGenericRepository _repo;
        public ShopCartService(IGenericRepository repo)
        {
            _repo = repo;
        }
        
        public ShopCartMemberResponseDTO GetShopCartCard(ShopCartMemberRequestDTO Id)
        {
            var material = _repo.GetAll<ShoppingCart>().Where(m => m.MemberId == Id.MemberId).ToList();//抓資料
            if(material == null) return null;
            
            var ShopCartResult = new ShopCartMemberResponseDTO
            {
                CartCard = new CartCard
                {
                    ShopCartCards =  material.Select(m =>
                    new ShopCartCard
                    {
                        ShopCartId = m.ShoppingCartId,
                        SpecificationId = m.SpecificationId,
                    }).ToList(),
                }
            };
            ShopCartResult.CartCard.ShopCartCards.ForEach(sc =>
            {
                _repo.GetAll<ProgramSpecification>().Where(ps => sc.SpecificationId == ps.SpecificationId)
                .ToList()
                .ForEach(ps =>
                {
                    _repo.GetAll<Model.Models.Program>().Where(pg => pg.ProgramId == ps.ProgramId)
                    .ToList()
                    .ForEach(pg =>
                    {
                        _repo.GetAll<Product>().Where(pd => pd.ProductId == pg.ProductId)
                       .ToList()
                       .ForEach(p =>
                       new ShopCartCard
                       {
                           ProductName = p.ProductName,
                           ProgramTitle = pg.Title,
                           DepartureDate = sc.DepartureDate,
                           ScreenTime = sc.ScreenTime,
                           ProductPhoto = _repo.GetAll<ProductPhoto>().First(pp => pp.ProductId == p.ProductId).ToString(),
                           JoinTime = sc.JoinTime,
                           Quantity = sc.Quantity,
                           Productdeleted = p.Isdeleted,
                           Programdeleted = pg.Isdeleted,

                       });
                       
                    });

                });
            });

            //ShopCartResult.CartCard.RecommendCartCards.ForEach(cr =>
            //{
            //    _repo.GetAll<Product>().Where(pd => pd.ProductId == cr.ProductId).ToList()
            //    .ForEach(pd =>
            //    {
            //        _repo.GetAll<ProductTag>().Where(pt => pt.ProductId == pd.ProductId).ToList()
            //        .ForEach(pt =>
            //        {
            //            _repo.GetAll<City>().Where(c => c.CityId == pd.CityId).ToList()
            //            .ForEach(p => new RecommendCartCard
            //            {
            //                ProductName = pd.ProductName,
            //                ProductPhoto = _repo.GetAll<ProductPhoto>().First(pp => pp.ProductId == pd.ProductId).ToString(),
            //                CityName = p.CityName,
            //                TagText = _repo.GetAll<Tag>().Where(t => t.TagId == pt.TagId).ToString(),
            //            });
            //        });
            //    });
            //});
            //material.ForEach(m =>
            //{
            //    _repo.GetAll<ProgramSpecification>().Where(ps => m.SpecificationId == ps.SpecificationId)
            //    .ToList()
            //    .ForEach(ps =>
            //    {
            //        _repo.GetAll<Model.Models.Program>().Where(pg => pg.ProgramId == ps.ProgramId)
            //        .ToList()
            //        .ForEach(pg =>
            //        {
            //            var product = _repo.GetAll<Product>().Where(pd => pd.ProductId == pg.ProductId).ToList();
                        
            //        });

            //    });
            //});
            return ShopCartResult;
        }




    }
}
