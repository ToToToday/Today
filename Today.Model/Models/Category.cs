using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParentCategory = new HashSet<Category>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; }
    }
}
