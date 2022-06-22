using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            Locations = new HashSet<Location>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductStatuses = new HashSet<ProductStatus>();
            Programs = new HashSet<Program>();
            Tags = new HashSet<Tag>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UseInfo { get; set; }
        public string Illustrate { get; set; }
        public string ShoppingNotice { get; set; }
        public string CancellationPolicy { get; set; }
        public int StockMax { get; set; }
        public string UnitsOnOrder { get; set; }
        public string HowUse { get; set; }
        public int CityId { get; set; }
        public int SupplierId { get; set; }

        public virtual City City { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductStatus> ProductStatuses { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
