using System.Collections.Generic;

namespace TodayMVC.Admin.ViewModels
{
    public class SubscriptionVM
    {
        public List<MailInfo> MailList { get; set; }
       

        public class MailInfo
        {
            public string Email { get; set; }
        }
    }
}
