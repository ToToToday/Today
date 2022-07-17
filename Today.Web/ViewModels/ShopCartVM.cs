using System;
using System.Collections.Generic;

namespace Today.Web.ViewModels
{
    public class ShopCartVM
    {

        public List<ShopCartCardVM> ShopCartCardList { get; set; }

    }
    public class ShopCartCardVM
    {
        public string ProductName { get; set; }
        public string ProgramTitle { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Quantity { get; set; }

        public DateTime ScreenTime { get; set; }

        public string ProductPhoto { get; set; }
    }
}
