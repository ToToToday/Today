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
        public int ProductId { get; set; }
        public string DetailJson { get; set; }
        public string Note { get; set; }
        public DateTime LeaseTime { get; set; }
        public int TicketsId { get; set; }
        public decimal SubTotal { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual Ticket Tickets { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
