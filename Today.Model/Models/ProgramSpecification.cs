using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ProgramSpecification
    {
        public ProgramSpecification()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Screenings = new HashSet<Screening>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int SpecificationId { get; set; }
        public bool IsScreening { get; set; }
        public int ProgramId { get; set; }
        public string Itemtext { get; set; }
        public string UnitText { get; set; }
        public decimal OriginalUnitPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int? Inventory { get; set; }
        public int Status { get; set; }

        public virtual Program Program { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Screening> Screenings { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
