using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using Today.Web.Data;
using Today.Web.DTOModels.MemberDTO;
using Today.Web.DTOModels.ProductDTO;
using Today.Web.Models;
using Today.Web.Services.CityService;
using Today.Web.Services.MemberService;
using Today.Web.Services.ProductService;
using Today.Web.ViewModels;

namespace Today.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICityService _cityService;
        private readonly IMemberService _memberService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICityService cityService, IMemberService memberService)
        {
            _logger = logger;
            _productService = productService;
            _cityService = cityService;
            _memberService = memberService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["SearchMessage"] = string.Empty;
            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;

            var homeproductSource = _productService.GetAllProductCard(userId);
            var citySource = _productService.PopularCityCard().CityList;
            var categorySource = homeproductSource.CategoryList;

            var homeshow = new ProductVM
            {
                PopularCity = citySource.Select(c => new ProductVM.City
                {
                    Id = c.Id,
                    CityName = c.CityName,
                    CityImage = c.CityImage
                }).ToList(),
                RecentlyViewed = homeproductSource.RecentlyViewed.Select(h => new ProductVM.RecentlyCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    Favorite = h.Favorite,
                    Price = (h.Price == null) ? null : h.Price
                }).ToList(),
                TopProduct = homeproductSource.TopProduct.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Favorite = h.Favorite,
                    Tags = h.Tags,
                    Rating = Math.Round(h.Rating.RatingStar, 1),
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Featured = homeproductSource.Featured.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Favorite = h.Favorite,
                    Tags = h.Tags,
                    Rating = Math.Round(h.Rating.RatingStar, 1),
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Paradise = homeproductSource.Paradise.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Favorite = h.Favorite,
                    Tags = h.Tags,
                    Rating = Math.Round(h.Rating.RatingStar, 1),
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                AttractionTickets = homeproductSource.AttractionTickets.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Favorite = h.Favorite,
                    Tags = h.Tags,
                    Rating = Math.Round(h.Rating.RatingStar, 1),
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Exhibition = homeproductSource.Exhibition.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Favorite = h.Favorite,
                    Tags = h.Tags,
                    Rating = Math.Round(h.Rating.RatingStar, 1),
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Hotel = homeproductSource.Hotel.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Favorite = h.Favorite,
                    Tags = h.Tags,
                    Rating = Math.Round(h.Rating.RatingStar, 1),
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Taoyuan = homeproductSource.Taoyuan.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Favorite = h.Favorite,
                    Tags = h.Tags,
                    Rating = Math.Round(h.Rating.RatingStar, 1),
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
                Evaluation = homeproductSource.Evaluation.Select(h => new ProductVM.ProductCardInfo()
                {
                    Id = h.Id,
                    ProductPhoto = h.ProductPhoto,
                    ProductName = h.ProductName,
                    CityName = h.CityName,
                    Favorite = h.Favorite,
                    Tags = h.Tags,
                    Rating = Math.Round(h.Rating.RatingStar, 1),
                    TotalGiveComment = h.Rating.TotalGiveComment,
                    TotalOrder = h.TotalOrder,
                    OriginalPrice = (h.Prices == null || h.Prices.OriginalPrice == h.Prices.Price) ? null : h.Prices.OriginalPrice,
                    Price = (h.Prices == null) ? null : h.Prices.Price
                }).ToList(),
            };

            if (TempData["SearchMessageTemp"] != null && TempData["SearchMessageTemp"].ToString() != string.Empty)
            {
                TempData["SearchMessage"] = JsonConvert.SerializeObject(TempData["SearchMessageTemp"].ToString());
            }

            return View(homeshow);
        }

        [HttpPost]
        public IActionResult Index(string searchword)
        {
            TempData["SearchMessageTemp"] = string.Empty;
            var searchWord = new ProductDTO.ProductRequestDTO
            {
                SearchWord = searchword
            };

            var result = _productService.ConvertPages(searchWord);

            if (result.HasCityId == true)
            {
                return RedirectToRoute(new { controller = "Product", action = "CityTour", id = result.Id });
            }
            else if (result.HasCityId == false && result.Id != 0)
            {
                return RedirectToRoute(new { controller = "Product", action = "Classify", id = result.Id });
            }
            else
            {
                TempData["SearchMessageTemp"] = "找不到此筆資料";
                return Redirect("/");
            }

            //return RedirectToRoute(new { controller = "Product", action = "CityTour", id = ViewData["CityId"] });
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
