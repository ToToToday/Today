using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int MemberId { get; set; }
        public int ParnerTypeId { get; set; }
        public DateTime CommentDate { get; set; }
        public int RatingStar { get; set; }
        public string CommentTitle { get; set; }
        public string Comments { get; set; }
        public int OrderId { get; set; }

        public virtual MemberInfoId Member { get; set; }
        public virtual Order Order { get; set; }
        public virtual PamerType ParnerType { get; set; }
        public virtual Product Product { get; set; }
    }
}
