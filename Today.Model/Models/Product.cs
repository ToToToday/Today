using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Product
    {
        public Product()
        {
            AboutProgramOptions = new HashSet<AboutProgramOption>();
            Collects = new HashSet<Collect>();
            Comments = new HashSet<Comment>();
            Locations = new HashSet<Location>();
            ProductCategories = new HashSet<ProductCategory>();
            ProductPhotos = new HashSet<ProductPhoto>();
            ProductTags = new HashSet<ProductTag>();
            Programs = new HashSet<Program>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Illustrate { get; set; }
        public string ShoppingNotice { get; set; }
        public string CancellationPolicy { get; set; }
        public string HowUse { get; set; }
        public int CityId { get; set; }
        public int SupplierId { get; set; }
        public bool Isdeleted { get; set; }

        public virtual City City { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<AboutProgramOption> AboutProgramOptions { get; set; }
        public virtual ICollection<Collect> Collects { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}
