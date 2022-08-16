using Microsoft.AspNetCore.Mvc;
using System;
using TodayMVC.Admin.Services.MailService;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.Controllers
{
    public class MailController : Controller
    {
        private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult MailManage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MailManage(MailVM mailtext)
        {

            try
            {
                _mailService.GetEmail(mailtext);
                ViewData["success"] = "true";
            }
            catch(Exception ex)
            {
                ViewData["success"] = "false";

            }

            return View();
            
        }
    }
}
