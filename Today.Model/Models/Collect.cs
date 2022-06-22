﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Collect
    {
        public int MemberId { get; set; }
        public int ProductId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}
