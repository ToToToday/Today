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
            var productSource = source.productList;
            var categorySource = source.cateoryList;
            var homeshow = new ProductVM()
            {
                RecentlyViewed = productSource.Select(s => new ProductVM.RecentlyCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    Price = s.Prices.Price
                }).Take(10).ToList(),
                Featured = productSource.Where(s => s.CityName.Contains("台北") || s.CityName.Contains("台南")).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = (s.Prices == null || s.Prices.OriginalPrice == s.Prices.Price) ? null : s.Prices.OriginalPrice,
                    Price = (s.Prices == null) ? null : s.Prices.Price
                }).Take(10).ToList(),
                Paradise = productSource.Where(s => s.ChildCategoryName.Contains("樂園")).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = (s.Prices.OriginalPrice == s.Prices.Price) ? null : s.Prices.OriginalPrice,
                    Price = s.Prices.Price
                }).Take(10).ToList(),
                AttractionTickets = productSource.Where(s => MaybeCategoryList(categorySource, "景點").Contains(s.ChildCategoryName)).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = (s.Prices.OriginalPrice == s.Prices.Price) ? null : s.Prices.OriginalPrice,
                    Price = s.Prices.Price
                }).Take(10).ToList(),
                Exhibition = productSource.Where(s => MaybeCategoryList(categorySource, "展覽").Contains(s.ChildCategoryName)).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = (s.Prices.OriginalPrice == s.Prices.Price) ? null : s.Prices.OriginalPrice,
                    Price = s.Prices.Price
                }).Take(10).ToList(),
                Hotel = productSource.Where(s => MaybeCategoryList(categorySource, "住宿").Contains(s.ChildCategoryName)).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = (s.Prices.OriginalPrice == s.Prices.Price) ? null : s.Prices.OriginalPrice,
                    Price = s.Prices.Price
                }).Take(10).ToList(),
                Taoyuan = productSource.Where(s => s.CityName.Contains("桃園")).Select(s => new ProductVM.ProductCardInfo
                {
                    Id = s.Id,
                    ProductPhoto = s.ProductPhoto,
                    ProductName = s.ProductName,
                    CityName = s.CityName,
                    Tags = s.Tags,
                    OriginalPrice = (s.Prices.OriginalPrice == s.Prices.Price) ? null : s.Prices.OriginalPrice,
                    Price = s.Prices.Price
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
