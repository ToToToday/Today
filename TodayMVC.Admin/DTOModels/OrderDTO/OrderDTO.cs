using System;
using System.Collections.Generic;

namespace TodayMVC.Admin.DTOModels.OrderDTO
{
    public class OrderDTO
    {
        public List<OrderInfo> AllOrderList { get; set; }
        public class OrderInfo
        {
            public int OrderID { get; set; }
            public string MemberName { get; set; }
            public string ProductName { get; set; }
            public DateTime OrderDate { get; set; }
            public int OrderStatus { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
        }
    }
}
