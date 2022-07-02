using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public string PaymentWay { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
