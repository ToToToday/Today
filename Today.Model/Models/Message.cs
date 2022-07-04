using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Message
    {
        public Message()
        {
            InverseReply = new HashSet<Message>();
        }

        public int MessageId { get; set; }
        public string MessageContext { get; set; }
        public DateTime SendDate { get; set; }
        public int Recipient { get; set; }
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public int? ReplyId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Order Order { get; set; }
        public virtual Message Reply { get; set; }
        public virtual ICollection<Message> InverseReply { get; set; }
    }
}
