using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class CouponDetail
    {
        public int CouponDetailsId { get; set; }
        public int CouponDiscount { get; set; }
        public int CouponId { get; set; }

        public virtual Coupon Coupon { get; set; }
    }
}
