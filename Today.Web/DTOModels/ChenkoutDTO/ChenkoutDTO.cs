using System;
using System.Collections.Generic;

namespace Today.Web.DTOModels.ChenkoutDTO
{
    public class ChenkoutDTO
    {
        public class ChenkoutRequestDTO
        {
            public int OrderId { get; set; }
        }
        public class ChenkoutResponseDTO
        {
            public List<MemberInfo> MemberDetail { get; set; }

        }

        public class MemberInfo
        {
            public string Name { get; set; }
            public string CityName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }
        public List<OrderInfo> OrderProductDetail { get; set; }

        public class OrderInfo
        {
            public int OrderId { get; set; }
            public string ProductName { get; set; }
            public string ProgramTitle { get; set; }
            public string Photo { get; set; }
            public DateTime DepartureDate { get; set; }
            public string Itemtext { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string UnitText { get; set; }


        }
        public List<OrderScreen> SceenDetail { get; set; }

        public class OrderScreen
        {
            public string Screen { get; set; }
        }
    }
}
