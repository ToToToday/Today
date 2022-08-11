using Microsoft.AspNetCore.Mvc;

namespace TodayMVC.Admin.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
