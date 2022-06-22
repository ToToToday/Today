using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Reply
    {
        public int ReplyId { get; set; }
        public int MessageId { get; set; }
        public string ReplayText { get; set; }
        public DateTime SendDate { get; set; }

        public virtual Message Message { get; set; }
    }
}
