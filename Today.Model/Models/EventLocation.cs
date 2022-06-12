using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class EventLocation
    {
        public int EventLocationId { get; set; }
        public int LocationDetailsId { get; set; }
        public int ProductId { get; set; }

        public virtual LocationDetail LocationDetails { get; set; }
        public virtual Product Product { get; set; }
    }
}
