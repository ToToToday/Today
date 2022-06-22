using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ProgramSpecification
    {
        public ProgramSpecification()
        {
            PricingItems = new HashSet<PricingItem>();
        }

        public int SpecificationId { get; set; }
        public string SpecificationJson { get; set; }
        public int ProgramId { get; set; }

        public virtual Program Program { get; set; }
        public virtual ICollection<PricingItem> PricingItems { get; set; }
    }
}
