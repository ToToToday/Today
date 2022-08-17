using System.Collections.Generic;
using System.Linq;
using TodayMVC.Admin.Helper;
using TodayMVC.Admin.Repositories.DapperMailRepositories;
using TodayMVC.Admin.ViewModels;
using static TodayMVC.Admin.ViewModels.SubscriptionVM;

namespace TodayMVC.Admin.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly IDapperMailRepository _mailRepo;
        public MailService(IDapperMailRepository mailRepo)
        {
            _mailRepo = mailRepo;
        }
        public void GetEmail(MailVM emailText)
        {
            var emailData = _mailRepo.SelectAll();
            var getEmail = emailData.Select(e => e.Email

            ).ToList();


            //信件主旨
            string subject = emailText.EmailTitle;

            //信件內容
            string body = emailText.EmailBody; 
            
            MailHelper.SendMail(getEmail, subject, body);
        }
    }
}
