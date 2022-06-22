using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            Members = new HashSet<Member>();
        }

        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Context { get; set; }
        public string DiscountCode { get; set; }
        public decimal CouponDiscount { get; set; }
        public string ConditionsOfUse { get; set; }
        public string CouponStatus { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
