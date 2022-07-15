using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Web.Data;
using Today.Web.Models;
using Today.Web.Services.ProductService;
using Today.Web.ViewModels;
using static Today.Web.ViewModels.ProductVM;

namespace Today.Web.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var source = _productService.GetProduct();
            var homeshow = new ProductVM()
            {
                RecentlyViewed = source.productList.Select(s => new ProductVM.RecentlyCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    Price = s.Price
                }).Take(10).ToList(),
                Featured = source.productList.Where(s => s.CityName.Contains("台北") || s.CityName.Contains("台南")).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = s.OriginalPrice,
                    Price = s.Price
                }).Take(10).ToList(),
                Paradise = source.productList.Where(s => s.ChildCategoryName.Contains("樂園")).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = s.OriginalPrice,
                    Price = s.Price
                }).Take(10).ToList(),
                AttractionTickets = source.productList.Where(s => s.ParentCategoryName.Contains("景點")).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = s.OriginalPrice,
                    Price = s.Price
                }).Take(10).ToList(),
                Exhibition = source.productList.Where(s => s.ParentCategoryName.Contains("展覽")).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = s.OriginalPrice,
                    Price = s.Price
                }).Take(10).ToList(),
                Hotel = source.productList.Where(s => s.ParentCategoryName.Contains("住宿")).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = s.OriginalPrice,
                    Price = s.Price
                }).Take(10).ToList(),
                Taoyuan = source.productList.Where(s => s.CityName.Contains("桃園")).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = s.OriginalPrice,
                    Price = s.Price
                }).Take(10).ToList(),
            };
            
            return View(homeshow);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Data()
        {
            InitDB data = new InitDB();
            data.CreateBaseData();
            return Content("成功");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
