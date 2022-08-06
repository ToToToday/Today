using Microsoft.AspNetCore.Mvc;

namespace TodayMVC.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult CreateProduct()
        {
            return View();
        }
        public IActionResult UpdateProduct()
        {
            return View();
        }
    }
}
