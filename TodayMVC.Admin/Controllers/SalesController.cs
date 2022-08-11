using Microsoft.AspNetCore.Mvc;

namespace TodayMVC.Admin.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Chart()
        {
            return View();
        }
    }
}
