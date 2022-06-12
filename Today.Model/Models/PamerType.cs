using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class PamerType
    {
        public PamerType()
        {
            Comments = new HashSet<Comment>();
        }

        public int PamerTypeId { get; set; }
        public string PamerType1 { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
