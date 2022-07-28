using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Today.Web.Services.CheenkoutService;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ChenkoutDTO.ChenkoutDTO;

namespace Today.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IChenkoutService _chenkoutService;
        public MemberController(IChenkoutService chenkoutService)
        {
            _chenkoutService = chenkoutService;
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
        public IActionResult ShopCart()
        {
            return View();
        }
        public IActionResult Checkout(int id)
        {
            var orderRequet = new ChenkoutRequestDTO
            {
                OrderId = id,
            };
            var memberinfo = _chenkoutService.GetOrderMember(orderRequet);
            var orderinfo = _chenkoutService.GetOrderProduct(orderRequet);
            var screeninfo = _chenkoutService.GetOrderSceen(orderRequet);
            string s;
            if (screeninfo == null)
            {
                s = screeninfo == null ? "": screeninfo.ToString();
               
            }
            else
            {
                s = $"場次:{screeninfo.Screen}";
                //s = "場次:" + string.Format("{0:yyyy/MM/dd}", screeninfo.Screen);
            }
            var OrderProduct = orderinfo.ProductName;
            var OrderQuantity = orderinfo.Quantity;
            var OrderPrice = (int)orderinfo.UnitPrice;
            var OrderId = orderinfo.OrderId;
            TempData["OrderProduct"] = OrderProduct;
            TempData["OrderQuantity"] = OrderQuantity;
            TempData["OrderPrice"] = OrderPrice;
            TempData["OrderId"] = OrderId;
            
            var orderPage = new ChenkoutVM
            {
                OrderMember = new ChenkoutVM.MemberInfo
                {
                    Name = memberinfo.Name,
                    CityName = memberinfo.CityName,
                    PhoneNumber = memberinfo.PhoneNumber,
                    Email = memberinfo.Email
                },
                OrderProduct = new ChenkoutVM.OrderInfo
                {
                    ProductName = orderinfo.ProductName,
                    ProgramTitle = orderinfo.ProgramTitle,
                    Photo = orderinfo.Photo,
                    DepartureDate = string.Format("{0:yyyy/MM/dd}",orderinfo.DepartureDate),
                    Screen = s,
                    Itemtext = orderinfo.Itemtext,
                    Quantity = orderinfo.Quantity,
                    UnitPrice = orderinfo.UnitPrice,
                    UnitText = orderinfo.UnitText,
                }
               
            };
            return View(orderPage);
        }
        

    }
}
