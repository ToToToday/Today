using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class HowUse
    {
        public int HowUseId { get; set; }
        public int HowUseDetailsId { get; set; }
        public int ProductId { get; set; }

        public virtual HowUseDetail HowUseDetails { get; set; }
        public virtual Product Product { get; set; }
    }
}
