using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class MinorCategory
    {
        public int MinorCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int PrimaryCategoryId { get; set; }

        public virtual PrimaryCategory PrimaryCategory { get; set; }
    }
}
