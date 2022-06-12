using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Tag
    {
        public int TagId { get; set; }
        public int TagDetailsId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual TagDetail TagDetails { get; set; }
    }
}
