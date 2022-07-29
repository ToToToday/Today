using System;

namespace Today.Web.ViewModels
{
    public class ShopCartRequestVM
    {
        public int SpecificationId { get; set; }
        public int ScreeningId{ get; set; }
        public string ProductName{ get; set; }
        public string ProgramTitle{ get; set; }
        public string Path{ get; set; }
        public DateTime DepartureDate{ get; set; }
        public string UnitText{ get; set; }
        public decimal UnitPrice{ get; set; }
        public int Quantity{ get; set; }
        public string ScreenTime{ get; set; }
}
}
