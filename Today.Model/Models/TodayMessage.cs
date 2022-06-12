using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class TodayMessage
    {
        public TodayMessage()
        {
            Messages = new HashSet<Message>();
        }

        public int TodayMessageId { get; set; }
        public string MessageContext { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
