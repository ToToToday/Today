using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Member
    {
        public Member()
        {
            Collects = new HashSet<Collect>();
            Comments = new HashSet<Comment>();
            CouponManages = new HashSet<CouponManage>();
            LoginWays = new HashSet<LoginWay>();
            Messages = new HashSet<Message>();
            Orders = new HashSet<Order>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int? CityId { get; set; }
        public string Image { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public string IdentityCard { get; set; }
        public bool? Gender { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Collect> Collects { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CouponManage> CouponManages { get; set; }
        public virtual ICollection<LoginWay> LoginWays { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
