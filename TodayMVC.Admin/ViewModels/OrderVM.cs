using System;
using System.Collections.Generic;

namespace TodayMVC.Admin.ViewModels
{
    
    public class OrderVM
    {
        public List<OrderInfo> OrderList { get; set; }

        public class OrderInfo
        {
            
            public int OrderId { get; set; }
            public string OrderDate { get; set; }
            public string MemberName { get; set; }
            public string ProductName { get; set; }
            public string ProgramName { get; set; }
            public string ItemText { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
            public string Status { get; set; }

        }
    }
}
