using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ShppingCart
    {
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? TicketId { get; set; }

        public virtual MemberInfoId Member { get; set; }
        public virtual Product Product { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
