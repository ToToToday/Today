using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ProductPhoto
    {
        public int PhotoId { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }
        public int Sort { get; set; }

        public virtual Product Product { get; set; }
    }
}
