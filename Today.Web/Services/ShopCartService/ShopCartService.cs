using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels;
using Today.Web.DTOModels.ShopCartDTO;
using Today.Web.Services.CityService;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ShopCartMemberDTO;
using static Today.Web.DTOModels.ShopCartMemberDTO.ShopCartMemberResponseDTO;

namespace Today.Web.Services.ShopCartService
{

    public class ShopCartService:IShopCartService
    {
      
        private readonly IGenericRepository _repo;
        public ShopCartService(IGenericRepository repo)
        {
            _repo = repo;
        }
        
        public List<ShopCartCard> GetShopCartCard(ShopCartMemberRequestDTO Id)
        {
            

            var shoppingCart =  _repo.GetAll<ShoppingCart>().Where(m => m.MemberId == Id.MemberId).ToList();
            if(shoppingCart == null) return null;



            var spec = _repo.GetAll<ProgramSpecification>();

            var cartSpec = shoppingCart.Join(spec, c => c.SpecificationId, s => s.SpecificationId, (c, s) => 
            new {
                c.MemberId,
                c.ShoppingCartId,
                c.ScreeningId,
                c.SpecificationId,
                c.DepartureDate,
                c.Quantity,
                s.UnitPrice,
                s.ProgramId,
                s.UnitText,
            }).ToList();

            var program = _repo.GetAll<Model.Models.Program>();

            var cartPr = cartSpec.Join(program, sp => sp.ProgramId, p => p.ProgramId, (sp, p) => new {
                sp.MemberId,
                sp.ShoppingCartId,
                sp.ScreeningId,
                //c.SpecificationId,
                sp.DepartureDate,
                sp.Quantity,
                sp.UnitPrice,
                sp.UnitText,
                //c.ProgramId,
                p.Title,
                p.ProductId
            }).ToList();

            var product = _repo.GetAll<Product>();

            var newProduct = cartPr.Join(product, pr => pr.ProductId, p => p.ProductId, (pr, p) => new {
                pr.MemberId,
                pr.ShoppingCartId,
                pr.ScreeningId,
                pr.DepartureDate,
                pr.Quantity,
                pr.UnitPrice,
                pr.UnitText,
                pr.Title,
                p.ProductId,
                p.ProductName
            }).ToList();

            var photo = _repo.GetAll<ProductPhoto>();

      
            var cartPhoto = newProduct.Join(photo, pd => pd.ProductId, pp => pp.ProductId, (pd, pp) => new
            {
                pd.MemberId,
                pd.ShoppingCartId,
                pd.ScreeningId,
                pd.DepartureDate,
                pd.Quantity,
                pd.UnitPrice,
                pd.UnitText,
                pd.Title,
                pd.ProductName,
                pd.ProductId,
                pp.Path
            }).ToList();


            var scr = _repo.GetAll<Screening>();



            var cartScr = cartPhoto.Join(scr, pp => pp.ScreeningId, sc => sc.ScreeningId, (pp, sc) => new {
                pp.MemberId,
                pp.ShoppingCartId,
                pp.ScreeningId,
                pp.DepartureDate,
                pp.Quantity,
                pp.UnitPrice,
                pp.UnitText,
                pp.Title,
                pp.ProductName,
                pp.Path,
                sc.Time
            }).ToList();


            //var result = new ShopCartMemberResponseDTO
            //{
            //    //ShopCartCards = new List<ShopCartCard> {
            //    //    new ShopCartCard {
            //    //        ProductName = cartScr.ProductName,
            //    //        ProgramTitle = cartScr.Title,
            //    //        DepartureDate = cartScr.DepartureDate,
            //    //        Quantity = cartScr.Quantity,
            //    //        ScreenTime = cartScr.Time,
            //    //        ProductPhoto = cartScr.Path,
            //    //        UnitPrice = cartScr.UnitPrice,
            //    //        UnitText = cartScr.UnitText,
            //    //    }
            //    //}.ToList()

            //};

            var result = cartScr.Select(c => new ShopCartMemberResponseDTO.ShopCartCard
            {
                ProductName = c.ProductName,
                ProgramTitle = c.Title,
                DepartureDate = c.DepartureDate,
                Quantity = c.Quantity,
                ScreenTime = c.Time,
                ProductPhoto = c.Path,
                UnitPrice = c.UnitPrice,
                UnitText = c.UnitText,
            }).ToList();





            

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
            return result;
        }

        //public async Task<string> AddItem(int memberId,int shopCartId)
        //{
        //    var shoppingCart = await _repo.GetAll<ShoppingCart>().Where(m => m.MemberId == memberId).FirstOrDefaultAsync();


        //    var result =
        //        new ShopCartCard
        //        {
        //            MemberId = memberId,
        //            ShopCartId = shopCartId,
        //            JoinTime = System.DateTime.UtcNow,

        //        };







        //    //var joinTime = await _repo.GetAll<ShopCartCard>().OrderBy(m => m.JoinTime).FirstOrDefaultAsync();

        //    //var isNull = await _repo.GetAll<ShopCartCard>()
        //    //    .Where(c => c.ShopCartId == result.ShopCartId).FirstOrDefaultAsync();

        //    //if (isNull == null)
        //    //{
        //    //    return "已成功加入購物車";
        //    //}



        //    return result.ToString();
        //}

        public CreateShopCartOutputDTO CreateShopCart(CreateShopCartInputDTO input)
        {
            var result = new CreateShopCartOutputDTO
            {
                IsSuccess = false,
            };
            var cartEntity = new ShoppingCart
            {
                //ShoppingCartId = input.ShoppingCartId,
                MemberId = input.MemberId,
                SpecificationId = input.SpecificationId,
                DepartureDate = input.DepartureDate,
                Quantity = input.Quantity,
                ScreeningId = 2,
                JoinTime = DateTime.Now,
                
            };

            //var shopcart = _repo.GetAll<ShoppingCart>();
            //var specEntity = _repo.GetAll<ProgramSpecification>().Join(shopcart, sc => sc.SpecificationId, sp => sp.SpecificationId, (sc, sp) => new
            //{
            //    sp.SpecificationId,
            //    sc.ProgramId,
            //});
            //var program = _repo.GetAll<Model.Models.Program>();
            //var programEntity = specEntity.Join(program, sc => sc.ProgramId, pr => pr.ProgramId, (sc, pr) => new
            //{
            //    sc.ProgramId,
            //    pr.Title,
            //    pr.ProductId
            //});

            

            try
            {
                _repo.Create<ShoppingCart>(cartEntity);
                //_repo.Create(programEntity);
                _repo.SavaChanges();
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            


            return result;
        }
    }
}
