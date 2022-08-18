using System;
using System.Collections.Generic;

namespace TodayMVC.Admin.ViewModels
{
    public class SalesVM
    {
        public List<SalesInfo> SalesList { get; set; }
        public List<WeekSalesInfo> WeekSalesList { get; set; }
        public class SalesInfo
        {
            public int Month { get; set; }

            //public int Quantity { get; set; }
            //public decimal UnitPrice { get; set; }

            public decimal SalesAmount { get; set; }
        }
        public class WeekSalesInfo
        {
            //public int Day { get; set; }
            public string DateTime { get; set; }
            public decimal SalesAmount { get; set; }
        }
    }
}
