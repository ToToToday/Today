using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            CouponManages = new HashSet<CouponManage>();
        }

        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Context { get; set; }
        public string DiscountCode { get; set; }
        public decimal CouponDiscount { get; set; }
        public int? FullConsumption { get; set; }
        public int? Rebate { get; set; }
        public string UseInfo { get; set; }

        public virtual ICollection<CouponManage> CouponManages { get; set; }
    }
}
