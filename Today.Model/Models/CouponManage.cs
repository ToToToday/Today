using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class CouponManage
    {
        public int CouponManageId { get; set; }
        public int CouponId { get; set; }
        public int StaffId { get; set; }
        public DateTime SendTime { get; set; }
        public int MemberId { get; set; }
        public int CouponStatus { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Member Member { get; set; }
        public virtual staff Staff { get; set; }
    }
}
