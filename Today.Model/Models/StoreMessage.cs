using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class StoreMessage
    {
        public StoreMessage()
        {
            Messages = new HashSet<Message>();
        }

        public int StoreMessageId { get; set; }
        public string MessageContext { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
