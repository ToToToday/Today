using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Order
    {
        public Order()
        {
            Messages = new HashSet<Message>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public int PaymentId { get; set; }
        public int Status { get; set; }
        public int StatusUpdate { get; set; }
        public string Note { get; set; }

        public virtual Member Member { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
