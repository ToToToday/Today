using System;

namespace TodayMVC.Admin.ViewModels
{
    public class OrderVM
    {
        public int OrderID { get; set; }
        public string MemberName { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
    }
}
