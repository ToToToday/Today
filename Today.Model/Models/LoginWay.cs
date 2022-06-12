using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class LoginWay
    {
        public LoginWay()
        {
            MemberInfoIds = new HashSet<MemberInfoId>();
        }

        public int LoginWayId { get; set; }
        public int LoginWayName { get; set; }

        public virtual ICollection<MemberInfoId> MemberInfoIds { get; set; }
    }
}
