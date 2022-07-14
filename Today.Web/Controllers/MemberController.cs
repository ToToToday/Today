using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.Models;
using Today.Web.Services.ShopCartService;

namespace Today.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly TodayDBContext _context;
        private readonly IGenericRepository _cart;
        public MemberController(TodayDBContext context, IGenericRepository cart)
        {
            _cart = cart;
            _context = context;
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
        public IActionResult ShopCart(Cart cart, string returnUrl)
        {
            return View(new CartIndex
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
            //return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult AddToCart(Cart cart, int id, string returnUrl)
        {
            //Product product = _context.Products.Where(p => p.ProductId == id).Select(p => p).FirstOrDefault();
            //if (product != null)
            //{
            //    cart.AddItem(product, 1);
            //}
            //return RedirectToAction("ShopCart", new { returnUrl });
            Product product = _context.Products.Where(p => p.ProductId == id).Select(p => p).FirstOrDefault();
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return View(cart);
        }

    }
}
