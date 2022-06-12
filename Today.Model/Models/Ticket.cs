using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            OrderDetais = new HashSet<OrderDetai>();
        }

        public int TicketId { get; set; }
        public string TicketsName { get; set; }

        public virtual ICollection<OrderDetai> OrderDetais { get; set; }
    }
}
