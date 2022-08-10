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
            var favoriteList = _repo.GetAll<Collect>().Where(c => c.MemberId == Id.MemberId).Select(x => x.ProductId);

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

            var photo = _repo.GetAll<ProductPhoto>().Where(p => p.Sort == 1);

      
            var cartPhoto = newProduct.Join(photo, pd => pd.ProductId, pp => pp.ProductId, (pd, pp) => new
            {
                MemberId = pd.MemberId,
                ShoppingCartId = pd.ShoppingCartId,
                ScreeningId = pd.ScreeningId,
                DepartureDate = pd.DepartureDate,
                Quantity = pd.Quantity,
                UnitPrice = pd.UnitPrice,
                UnitText = pd.UnitText,
                Title = pd.Title,
                ProductName = pd.ProductName,
                ProductId = pd.ProductId,
                Favorite = favoriteList.Contains(pd.ProductId),
                Path = pp.Path
            }).ToList();


            var scr = _repo.GetAll<Screening>();



            var cartScr = cartPhoto.Join(scr, pp => pp.ScreeningId, sc => sc.ScreeningId, (pp, sc) => new {
                sc.SpecificationId,
                pp.MemberId,
                pp.ShoppingCartId,
                sc.ScreeningId,
                pp.DepartureDate,
                pp.Quantity,
                pp.UnitPrice,
                pp.UnitText,
                pp.Title,
                pp.ProductName,
                pp.ProductId,
                pp.Favorite,
                pp.Path,
                sc.Time
            }).ToList();



            var result = cartScr.Select(c => new ShopCartMemberResponseDTO.ShopCartCard
            {
                ScreenId = c.ScreeningId,
                ShopCartId = c.ShoppingCartId,
                SpecificationId = c.SpecificationId,
                ProductName = c.ProductName,
                Favorite = c.Favorite,
                ProgramTitle = c.Title,
                DepartureDate = c.DepartureDate,
                Quantity = c.Quantity,
                ScreenTime = c.Time,
                ProductPhoto = c.Path,
                ProductId = c.ProductId,
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

        public void CreateShopCard(ShopCartRequestVM request)
        {


            var input = request.SpecificationList.Select(sp =>
              new ShoppingCart
              {
                  MemberId = request.MemberId,
                  DepartureDate = DateTime.Parse(request.DepartureDate),
                  ScreeningId = 1,
                  SpecificationId = sp.SpecificationId,
                  Quantity = sp.Quantity,
                  JoinTime = DateTime.UtcNow.AddHours(8),
              })
            .ToList();


            input.ForEach(s =>
            {
                _repo.Create<ShoppingCart>(s);
                _repo.SavaChanges();
            });


            //var isNull =  _repo.GetAll<ShopCartCard>()
            //        .Where(c => c.ShopCartId == request.ShoppingCartId).FirstOrDefaultAsync();

            //if (isNull == null)
            //{
            //    return "已成功加入購物車";
            //}

            //var result = new CreateShopCartOutputDTO
            //{
            //    IsSuccess = false,
            //};
            //var cartEntity = new ShoppingCart
            //{
            //    //ShoppingCartId = input.ShoppingCartId,
            //    MemberId = 5,
            //    SpecificationId = input.SpecificationId,
            //    DepartureDate = input.DepartureDate,
            //    Quantity = input.Quantity,
            //    ScreeningId = input.ScreeningId,
            //    JoinTime = DateTime.UtcNow.AddHours(8),

            //};

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



            //try
            //{
            //    _repo.Create<ShoppingCart>(input);
            //    //_repo.Create(programEntity);
            //    _repo.SavaChanges();
            //    result.IsSuccess = true;
            //}
            //catch (Exception ex)
            //{
            //    result.IsSuccess = false;
            //    result.Message = ex.Message;
            //}




            //return result;
        }

        public void DeleteShopCard(DeleteCardVM request)
        {
            var removeCard = _repo.GetAll<ShoppingCart>().FirstOrDefault(r => r.ShoppingCartId == request.ShoppingCartId);

            _repo.Delete(removeCard);
            _repo.SavaChanges();
        }
    }
}
