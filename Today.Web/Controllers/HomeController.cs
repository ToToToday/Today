﻿using Microsoft.AspNetCore.Authorization;
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
using Today.Web.DTOModels.AccountDTO;
using Today.Web.DTOModels.ProductDTO;
using Today.Web.Models;
using Today.Web.Services.CityService;
using Today.Web.Services.ProductService;
using Today.Web.ViewModels;

namespace Today.Web.Controllers
{
    //[Authorize(Roles = "A")] //限制「具備A角色」才可拜訪******
    //[Authorize(Roles = "B")]
    //[Authorize(Roles = "A,B")] //限制「具備A角色 或 B角色」才可拜訪

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
        [HttpGet]
        public IActionResult Index()
        {
            var homeproductSource = _productService.GetAllProductCard();
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
                    Price = (h.Price == null) ? null : h.Price
                }).ToList(),
                TopProduct = homeproductSource.TopProduct.Select(h => new ProductVM.ProductCardInfo()
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
                Featured = homeproductSource.Featured.Select(h => new ProductVM.ProductCardInfo()
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
                Paradise = homeproductSource.Paradise.Select(h => new ProductVM.ProductCardInfo()
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
                AttractionTickets = homeproductSource.AttractionTickets.Select(h => new ProductVM.ProductCardInfo()
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
                Exhibition = homeproductSource.Exhibition.Select(h => new ProductVM.ProductCardInfo()
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
                Hotel = homeproductSource.Hotel.Select(h => new ProductVM.ProductCardInfo()
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
                Taoyuan = homeproductSource.Taoyuan.Select(h => new ProductVM.ProductCardInfo()
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
                Evaluation = homeproductSource.Evaluation.Select(h => new ProductVM.ProductCardInfo()
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

        [HttpPost]
        public IActionResult Index(string searchword)
        {
            var couponcode = new ProductDTO.ProductRequestDTO
            {
                CityName = searchword
            };

            var result = _productService.ToCityPage(couponcode);

            return RedirectToAction("CityTour", "Product", new { id = result.CityId });
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
