using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Category
    {
        public int ProductId { get; set; }
        public int MinorCategoryId { get; set; }

        public virtual MinorCategory MinorCategory { get; set; }
        public virtual Product Product { get; set; }
    }
}
