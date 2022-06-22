using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class PricingItem
    {
        public int PricingItemId { get; set; }
        public int SpecificationId { get; set; }
        public string Itemtext { get; set; }
        public string UnitText { get; set; }
        public decimal OriginalUnitPrice { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual ProgramSpecification Specification { get; set; }
    }
}
