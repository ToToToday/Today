using System;
using System.Collections.Generic;

namespace Today.Web.ViewModels
{
    public class ShopCartRequestVM
    {
        public int ShoppingCartId { get; set; }
        public int MemberId { get; set; }

        public List<Specification> SpecificationList { get; set; }

        public int? ScreeningId { get; set; }
        public string DepartureDate { get; set; }
    }

    public class Specification
    {
        public int SpecificationId { get; set; }
        public int Quantity { get; set; }
    }
}
