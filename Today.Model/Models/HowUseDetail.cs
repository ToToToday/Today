using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class HowUseDetail
    {
        public HowUseDetail()
        {
            HowUses = new HashSet<HowUse>();
        }

        public int HowUseDetailsId { get; set; }
        public string Usage { get; set; }
        public string Caption { get; set; }

        public virtual ICollection<HowUse> HowUses { get; set; }
    }
}
