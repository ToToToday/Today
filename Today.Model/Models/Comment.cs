using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int OrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public int MemberId { get; set; }
        public int PartnerType { get; set; }
        public DateTime CommentDate { get; set; }
        public int RatingStar { get; set; }
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }

        public virtual Member Member { get; set; }
        public virtual OrderDetail OrderDetails { get; set; }
        public virtual Product Product { get; set; }
    }
}
