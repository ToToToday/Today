using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int? TodayMessageId { get; set; }
        public int? StoreMessageId { get; set; }

        public virtual StoreMessage StoreMessage { get; set; }
        public virtual TodayMessage TodayMessage { get; set; }
    }
}
