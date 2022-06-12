using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Product
    {
        public Product()
        {
            Collects = new HashSet<Collect>();
            Comments = new HashSet<Comment>();
            EventLocations = new HashSet<EventLocation>();
            HowUses = new HashSet<HowUse>();
            OffersDetails = new HashSet<OffersDetail>();
            OrderDetais = new HashSet<OrderDetai>();
            Tags = new HashSet<Tag>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UseInfo { get; set; }
        public string CancellationPolicy { get; set; }
        public string ShoppingNotice { get; set; }
        public string Illustrate { get; set; }
        public int CityId { get; set; }
        public int MinorCategoryId { get; set; }
        public int SupplierId { get; set; }

        public virtual City City { get; set; }
        public virtual MinorCategory MinorCategory { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Collect> Collects { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EventLocation> EventLocations { get; set; }
        public virtual ICollection<HowUse> HowUses { get; set; }
        public virtual ICollection<OffersDetail> OffersDetails { get; set; }
        public virtual ICollection<OrderDetai> OrderDetais { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
