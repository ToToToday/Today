using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodayMVC.Admin.Helper;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MailApiController : ControllerBase
    {
        
        public IActionResult MailSend([FromBody]MailVM mailtext)
        {
            //List<string> recipientList = new List<string> { "kitty881215@gmail.com" };
            //信件主旨
            string subject = mailtext.EmailTitle;/*"註冊驗證信";*/

            //信件內容
            string body = mailtext.EmailBody; /*"<h1>請點<a herf='#API網址'>連結</a>驗證</h1>"; //注意單雙引號*/

            //MailHelper.SendMail(recipientList, subject, body);
            return null;
        }
    }
}
