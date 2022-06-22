using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ShoppinCart
    {
        public int MemberId { get; set; }
        public int SpecificationId { get; set; }
        public DateTime DepartureDate { get; set; }

        public virtual Member Member { get; set; }
        public virtual ProgramSpecification Specification { get; set; }
    }
}
