using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class PromotionWay
    {
        public PromotionWay()
        {
            Offers = new HashSet<Offer>();
        }

        public int PromotionWayId { get; set; }
        public string PromotionWayName { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
