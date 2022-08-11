using System;
using System.Collections.Generic;

namespace Today.Web.ViewModels
{
    public class ShopCartVM
    {

        public int MemberId { get; set; }

        public List<ShopCartCardVM> ShopCartCardList { get; set; }


        public class ShopCartCardVM
        {
            public int ShoppingCartId { get; set; }
            public string ProductName { get; set; }
            public int ProductId { get; set; }
            public bool Favorite { get; set; }
            public string ProgramTitle { get; set; }
            public DateTime DepartureDate { get; set; }
            public int Quantity { get; set; }

            public int? ScreeningId { get; set; }
            public string ScreenTime { get; set; }

            public string Path { get; set; }
            public decimal UnitPrice { get; set; }
            public string UnitText { get; set; }
            public decimal Sum { get; set; }
            public int? SpecificationId { get; set; }
        }

    }

    

}
