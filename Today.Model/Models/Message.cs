using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Message
    {
        public Message()
        {
            Members = new HashSet<Member>();
            Replies = new HashSet<Reply>();
        }

        public int MessageId { get; set; }
        public string MessageContext { get; set; }
        public DateTime SendDate { get; set; }
        public string Recipient { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
