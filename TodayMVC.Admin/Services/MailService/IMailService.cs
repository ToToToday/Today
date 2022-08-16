using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.Services.MailService
{
    public interface IMailService
    {
        void GetEmail(MailVM emailText);
    }
}
