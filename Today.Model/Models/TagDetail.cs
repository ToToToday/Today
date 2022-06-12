using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class TagDetail
    {
        public TagDetail()
        {
            Tags = new HashSet<Tag>();
        }

        public int TagDetailsId { get; set; }
        public string TagInfo { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
