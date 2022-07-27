using System;
using System.Collections.Generic;

namespace Today.Web.ViewModels
{
    public class ShopCartVM
    {

        public List<ShopCartCardVM> ShopCartCardList { get; set; }

        
        public class ShopCartCardVM
        {
            public string ProductName { get; set; }
            public string ProgramTitle { get; set; }
            public DateTime DepartureDate { get; set; }
            public int Quantity { get; set; }

            public int ScreeningId { get; set; }
            public TimeSpan ScreenTime { get; set; }

            public string Path { get; set; }
            public decimal UnitPrice { get; set; }
            public string UnitText { get; set; }

            public int SpecificationId { get; set; }
        }

    }

    public class AddCartVM
    {
        public int SpecificationId { get; set; }

        public string ProductName { get; set; }
        public string ProgramTitle { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Quantity { get; set; }

        public TimeSpan ScreenTime { get; set; }

        public string Path { get; set; }
        public decimal UnitPrice { get; set; }
        public string UnitText { get; set; }
    }
    
}
