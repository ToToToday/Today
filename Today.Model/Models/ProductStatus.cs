using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ProductStatus
    {
        public int ProductStatusId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public int RemainingStock { get; set; }
        public int BookQuantity { get; set; }

        public virtual Product Product { get; set; }
    }
}
