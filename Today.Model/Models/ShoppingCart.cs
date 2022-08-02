using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int MemberId { get; set; }
        public int SpecificationId { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Quantity { get; set; }
        public int? ScreeningId { get; set; }
        public DateTime JoinTime { get; set; }

        public virtual Member Member { get; set; }
        public virtual Screening Screening { get; set; }
        public virtual ProgramSpecification Specification { get; set; }
    }
}
