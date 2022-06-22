﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class LoginWay
    {
        public int MemberId { get; set; }
        public string LongWayName { get; set; }
        public string UniqueId { get; set; }

        public virtual Member Member { get; set; }
    }
}
