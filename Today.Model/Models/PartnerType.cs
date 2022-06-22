using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class PartnerType
    {
        public PartnerType()
        {
            Comments = new HashSet<Comment>();
        }

        public int PartnerTypeId { get; set; }
        public string PartnerType1 { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
