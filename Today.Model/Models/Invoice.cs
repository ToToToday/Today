using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            Orders = new HashSet<Order>();
        }

        public int InvoiceId { get; set; }
        public string InvoiceWay { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
