using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class MemberInfoId
    {
        public MemberInfoId()
        {
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int? CityId { get; set; }
        public DateTime Brithday { get; set; }
        public string Pchone { get; set; }
        public string Email { get; set; }
        public int LoginWayId { get; set; }

        public virtual City City { get; set; }
        public virtual LoginWay LoginWay { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
