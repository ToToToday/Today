using System;
using System.Collections.Generic;

namespace Today.Web.ViewModels
{
    public class ChenkoutVM
    {
        public MemberInfo OrderMember { get; set; }
        public OrderInfo OrderProduct { get; set; }

        public class MemberInfo
        {
            public string Name { get; set; }
            public string CityName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }

        public class OrderInfo
        {
            public string ProductName { get; set; }
            public string ProgramTitle { get; set; }
            public string Photo { get; set; }
            public DateTime DepartureDate { get; set; }
            public string Screen { get; set; }
            public string Itemtext { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string UnitText { get; set; }
        }
    }
}
