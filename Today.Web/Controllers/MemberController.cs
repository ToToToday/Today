using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.Models;
using Today.Web.Services.ShopCartService;
using Today.Web.ViewModels;

namespace Today.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly TodayDBContext _context;
        private readonly IGenericRepository _cart;
        private readonly IShopCartService _shopCartService;
        public MemberController(TodayDBContext context, IGenericRepository cart, IShopCartService shopCartService)
        {
            _cart = cart;
            _shopCartService = shopCartService;
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
            //return View(new CartIndex
            //{
            //    Cart = cart,
            //    ReturnUrl = returnUrl
            //});

            //var result = from p in _cart.GetAll<Product>()
            //             where p.ProductId == 12
            //             select new { p = p.ProductName };



            var result = _shopCartService.GetShopCartCard();
            return View(result);
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult AddToCart(Cart cart, int id, string returnUrl)
        {
            //Product product = _context.Products.Where(p => p.ProductId == id).Select(p => p).FirstOrDefault();
            var result = _shopCartService.GetShopCartCard();
            List<CartItem> cartItems = new();
            

            if (result != null)
            {

                cartItems.Add(result.ConvertAll(x => new CartItem { Name = x.Name }).FirstOrDefault());
                
                //cart.AddItem(product, 1);
            }
            //return View();
            return RedirectToAction("ShopCart",cartItems);
            //Product product = _context.Products.Where(p => p.ProductId == id).Select(p => p).FirstOrDefault();
            //if (product != null)
            //{
            //    cart.AddItem(product, 1);
            //}
            //return View(cart);
        }

    }
}
