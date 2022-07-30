using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            Comments = new HashSet<Comment>();
        }

        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int SpecificationId { get; set; }
        public int Quantity { get; set; }
        public decimal? Discount { get; set; }
        public string Itemtext { get; set; }
        public DateTime? LeaseTime { get; set; }
        public int? TicketsId { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime DepartureDate { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProgramSpecification Specification { get; set; }
        public virtual Ticket Tickets { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
