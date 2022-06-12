using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            CouponDetails = new HashSet<CouponDetail>();
        }

        public int CouponId { get; set; }
        public string CouponName { get; set; }

        public virtual ICollection<CouponDetail> CouponDetails { get; set; }
    }
}
