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

namespace Today.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly TodayDBContext _context;
        //public HomeController(TodayDBContext context)
        //{
        //    _context = context;
        //}
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var cardshow = _productService.GetProductCard();
            //ViewData["SearchBox"] = "None";
            return View(cardshow);
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
