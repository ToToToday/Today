using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels.ShopCartDTO;
using Today.Web.Models;
using Today.Web.Services.ShopCartService;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ShopCartMemberDTO;
using static Today.Web.ViewModels.ShopCartVM;

namespace Today.Web.Controllers
{
    public class MemberController : Controller
    {

        private readonly IShopCartService _shopCartService;
        public MemberController(IShopCartService shopCartService)
        {
            _shopCartService = shopCartService;
        }
        public IActionResult CountSetting()
        {
            return View();
        }
        public IActionResult Coupon()
        {
            return View();
        }
        public IActionResult OrderManage()
        {
            return View();
        }
        public IActionResult MessageManage()
        {
            return View();
        }
        public IActionResult MyCollect()
        {
            return View();
        }
        [HttpGet]//請求
        public IActionResult ShopCart(/*ShopCartCardVM vm,*/ int Id = 2)
        {
            var ShopCartCardDTO = _shopCartService.GetShopCartCard(new ShopCartMemberRequestDTO { MemberId = Id });   //int.Parse(User.Identity.Name)
            var ShopCartVMs = new ShopCartVM
            {
                ShopCartCardList = ShopCartCardDTO
                .Select(s => new ShopCartCardVM
                {
                    ProductName = s.ProductName,
                    ProgramTitle = s.ProgramTitle,
                    Path = s.ProductPhoto,
                    DepartureDate = s.DepartureDate,
                    Quantity = s.Quantity,
                    ScreenTime = s.ScreenTime,
                    UnitPrice = s.UnitPrice,
                    UnitText = s.UnitText,

                }).ToList()
            };
            return View(ShopCartVMs);
        }
        //[HttpPost]//提交
        [HttpPost("~/[controller]/[action]/{memberId}/{SpecificationId}/{Quantity}")]
        public IActionResult ShopCart([FromRoute] string memberId, string specificationId, string quantity,string unitPrice, string screeningId)     //CreateShopCartInputDTO input /*, string ProgramTitle*/
        {
            //var userId = User.Identity.Name;


            var input = new CreateShopCartInputDTO
            {
                MemberId = int.Parse(memberId),
                SpecificationId = int.Parse(specificationId),
                //DepartureDate = DateTime.Now.AddDays(-1),
                DepartureDate = (DateTime)TempData["DepartureDate"],
                Quantity = int.Parse(quantity),
                ProgramTitle = TempData["ProgramTitle"] as string,
                Path = TempData["Path"] as string,
                ProductName = TempData["ProductName"] as string,
                UnitText = TempData["UnitText"] as string,
                ScreeningId = int.Parse(screeningId),
                UnitPrice = int.Parse(unitPrice),
                //ScreenTime = (TimeSpan)TempData["ScreenTime"]
            };
            
            
            var ShopCartCardDTO = _shopCartService.CreateShopCart(input);

            if(ShopCartCardDTO.IsSuccess == false)
            {
                return Content(ShopCartCardDTO.Message);
            }
            //var ShopCartVMs = new ShopCartVM
            //{

            //    ShopCartCardList = ShopCartCardDTO.ShopCartCards
            //    .Select(s => new ShopCartCardVM
            //    {
            //        ProductName = s.ProductName,
            //        ProgramTitle = s.ProgramTitle,
            //        Path = s.ProductPhoto,
            //        DepartureDate = s.DepartureDate,
            //        Quantity = s.Quantity,
            //        ScreenTime = s.ScreenTime,
            //        UnitPrice = s.UnitPrice,
            //        UnitText = s.UnitText,

            //    }).ToList()
            //};
            //User.Identity.Name;

            var result = new ShopCartVM();
            result.ShopCartCardList = new List<ShopCartCardVM>()
            { new ShopCartCardVM
            { Quantity = input.Quantity,ProgramTitle = input.ProgramTitle , Path = input.Path, ProductName = input.ProductName, UnitText = input.UnitText, DepartureDate = input.DepartureDate, UnitPrice = input.UnitPrice,ScreeningId = input.ScreeningId/*ScreenTime = input.ScreenTime*/} };
            //ViewData["id"] = id;
            //ViewData["SDate"] = StartDate;
            //ViewData["PersonCount"] = Person;
            //return Content("success");  //ShopCartVMs
            return View(result);
        }

        public IActionResult Checkout()
        {
            return View();
        }


    }
}
