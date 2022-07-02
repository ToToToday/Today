using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int Type { get; set; }

        public virtual Product Product { get; set; }
    }
}
