using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Order
    {
        public Order()
        {
            Comments = new HashSet<Comment>();
            OrderDetais = new HashSet<OrderDetai>();
        }

        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? PaymentId { get; set; }
        public int? InvoiceId { get; set; }
        public DateTime? LeaseTime { get; set; }
        public decimal? SumPrice { get; set; }
        public int? Total { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual MemberInfoId Member { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDetai> OrderDetais { get; set; }
    }
}
