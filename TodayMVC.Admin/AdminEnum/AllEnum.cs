using System.ComponentModel;

namespace TodayMVC.Admin.AdminEnum
{
    public class AllEnum
    {
        public enum OrderStatu
        {
            [Description("未付款")]
            Arrearage = 1,
            [Description("已付款")]
            Paid = 2,
        }
        public enum Gender
        {
            [Description("男")]
            Mam = 0,
            [Description("女")]
            Woman = 1,
        }
    }
}
