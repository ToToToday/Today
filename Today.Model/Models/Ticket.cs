using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int TicketsId { get; set; }
        public string TicketQrcode { get; set; }
        public int Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
