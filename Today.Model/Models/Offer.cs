using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Offer
    {
        public Offer()
        {
            OffersDetails = new HashSet<OffersDetail>();
        }

        public int OffersId { get; set; }
        public int? AdId { get; set; }
        public int? PromotionWayId { get; set; }
        public string Content { get; set; }

        public virtual Ad Ad { get; set; }
        public virtual PromotionWay PromotionWay { get; set; }
        public virtual ICollection<OffersDetail> OffersDetails { get; set; }
    }
}
