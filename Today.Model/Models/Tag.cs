using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Tag
    {
        public Tag()
        {
            ProductTags = new HashSet<ProductTag>();
        }

        public int TagId { get; set; }
        public string TagText { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
