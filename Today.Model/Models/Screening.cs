using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Screening
    {
        public Screening()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int ScreeningId { get; set; }
        public string Time { get; set; }
        public int? SpecificationId { get; set; }
        public int? Status { get; set; }

        public virtual ProgramSpecification Specification { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
