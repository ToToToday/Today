using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class OrderDetai
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? Discount { get; set; }
        public string Note { get; set; }
        public int? TicketsId { get; set; }
        public int? VehiclesNumber { get; set; }
        public int? CarModelId { get; set; }
        public int? Seats { get; set; }
        public DateTime? ShowTime { get; set; }

        public virtual CarModel CarModel { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual Ticket Tickets { get; set; }
    }
}
