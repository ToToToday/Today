using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class OffersDetail
    {
        public int OffersDetailsId { get; set; }
        public int OffersId { get; set; }
        public int ProductId { get; set; }
        public int? DiscountAngin { get; set; }

        public virtual Offer Offers { get; set; }
        public virtual Product Product { get; set; }
    }
}
