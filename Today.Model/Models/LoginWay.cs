using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class LoginWay
    {
        public int LoginWayId { get; set; }
        public int MemberId { get; set; }
        public int LongWayName { get; set; }
        public string UniqueId { get; set; }

        public virtual Member Member { get; set; }
    }
}
