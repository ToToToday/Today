using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Member
    {
        public Member()
        {
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int CityId { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public string IdentityCard { get; set; }
        public bool? Gender { get; set; }
        public string Password { get; set; }
        public int? MessageId { get; set; }
        public int? CouponId { get; set; }

        public virtual City City { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual Message Message { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
