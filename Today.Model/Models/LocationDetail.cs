using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class LocationDetail
    {
        public LocationDetail()
        {
            EventLocations = new HashSet<EventLocation>();
        }

        public int LocationDetailsId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<EventLocation> EventLocations { get; set; }
    }
}
