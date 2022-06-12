using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class MinorCategory
    {
        public MinorCategory()
        {
            Products = new HashSet<Product>();
        }

        public int MinorCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int PrimaryCategoryId { get; set; }

        public virtual PrimaryCategory PrimaryCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
