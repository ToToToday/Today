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
using Today.Web.Services.CityService;
using Today.Web.Services.ProductService;
using Today.Web.ViewModels;

namespace Today.Web.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICityService _cityService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICityService cityService)
        {
            _logger = logger;
            _productService = productService;
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            var citysource = _cityService.GetAllCity();
            var homeproductsource = _productService.GetAllProductCard();
            var categorySource = homeproductsource.CategoryList;

            var homeshow = new ProductVM
            {
                RecentlyViewed = homeproductsource.RecentlyViewed.Select(h => new ProductVM.RecentlyCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    Price = (h.Price == null) ? null : h.Price
                }).ToList(),
                TopProduct = homeproductsource.TopProduct.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Tags = h.Tags,
                    Rating = h.Rating.RatingStar,
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Featured = homeproductsource.Featured.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Tags = h.Tags,
                    Rating = h.Rating.RatingStar,
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Paradise = homeproductsource.Paradise.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Tags = h.Tags,
                    Rating = h.Rating.RatingStar,
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                AttractionTickets = homeproductsource.AttractionTickets.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Tags = h.Tags,
                    Rating = h.Rating.RatingStar,
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Exhibition = homeproductsource.Exhibition.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Tags = h.Tags,
                    Rating = h.Rating.RatingStar,
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Hotel = homeproductsource.Hotel.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Tags = h.Tags,
                    Rating = h.Rating.RatingStar,
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Taoyuan = homeproductsource.Taoyuan.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Tags = h.Tags,
                    Rating = h.Rating.RatingStar,
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Evaluation = homeproductsource.Evaluation.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Tags = h.Tags,
                    Rating = h.Rating.RatingStar,
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
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
