using Microsoft.AspNetCore.Mvc;

namespace TodayMVC.Admin.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult OrderManage()
        {
            return View();
        }
    }
}
