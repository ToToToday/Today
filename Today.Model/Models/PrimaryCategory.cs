using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class PrimaryCategory
    {
        public PrimaryCategory()
        {
            MinorCategories = new HashSet<MinorCategory>();
        }

        public int PrimaryCategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<MinorCategory> MinorCategories { get; set; }
    }
}
