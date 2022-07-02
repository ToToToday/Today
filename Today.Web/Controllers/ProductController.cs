using Microsoft.AspNetCore.Mvc;

namespace Today.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ProductPages() //商品頁面
        {
            return View();
        }
        public IActionResult Classify() //楊 分類
        {
            return View();
        }
       
        public IActionResult Souvenir() //伴手禮
        {
            
            return View();
        }
        public IActionResult CityClassify() //城市 分類
        {
            //TodayDBContext context = new TodayDBContext();
            //var result = await context.Cities.ToListAsync();
            return View();
        }
        public IActionResult CityTour() //各城市導覽頁
        {
            return View();
        }
        public IActionResult CityRaiders() //城市攻略
        {
            return View();
        }
        public IActionResult OffIsland() //離島 分類
        {
            return View();
        }
        public IActionResult ParentChild() //親子 分類
        {
            return View();
        }
        public IActionResult DIY() //DIY 分類
        {
            return View();
        }
        public IActionResult HSRClassify() //高鐵 分類
        {
            return View();
        }
        public IActionResult Rent() //租車 分類
        {
            return View();
        }
        public IActionResult Camping() //露營頁面
        {
            return View();
        }
        public IActionResult QuarantineHotel() //防疫旅館頁面
        {
            return View();
        }
        public IActionResult HSR() //國旅
        {
            return View();
        }
        public IActionResult AboutToday() //公司介紹
        {
            return View();
        }
    }
}
