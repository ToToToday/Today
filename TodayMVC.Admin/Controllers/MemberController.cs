using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TodayMVC.Admin.Services.MemberService;
using static TodayMVC.Admin.ViewModels.MemberVM;

namespace TodayMVC.Admin.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult MemberManage()
        {
            return View();
        }
        public IActionResult CommentManage()
        {
            return View();
        }
    }
}
