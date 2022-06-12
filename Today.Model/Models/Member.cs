using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Member
    {
        public int MemberId { get; set; }
        public int MessageId { get; set; }
        public int CollectId { get; set; }
        public int CouponId { get; set; }

        public virtual Collect Collect { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual MemberInfoId MemberNavigation { get; set; }
        public virtual Message Message { get; set; }
    }
}
