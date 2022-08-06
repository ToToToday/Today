using Microsoft.AspNetCore.Mvc;

namespace TodayMVC.Admin.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult MemberManage()
        {
            return View();
        }
    }
}
