using Microsoft.AspNetCore.Authorization;
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

namespace Today.Web.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //ViewData["SearchBox"] = "None";
            return View();
            //return Challenge(); //首頁導到登入頁，要你去登入
        }

        [Authorize]
        //(不明白??????)
        //未驗證(登入) => 要求你去登入 (發起【挑戰】)
        //已驗證(登入) => 看你權限夠不夠
        //  > 夠 ，就進頁面
        //  > 不夠 ，就Forbid禁止 => 導到【存取拒絕頁】 /Account/AccessDenied
        public IActionResult Privacy()
        {
            //TodayDBContext context = new TodayDBContext();
            //var result = await context.Categories.ToListAsync();
            return View();
        }
        //public async Task<IActionResult> Privacy()
        //{
        //    //TodayDBContext context = new TodayDBContext();
        //    //var result = await context.Categories.ToListAsync();
        //    return View();
        //}

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
