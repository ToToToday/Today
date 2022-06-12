using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Ad
    {
        public Ad()
        {
            Offers = new HashSet<Offer>();
        }

        public int AdId { get; set; }
        public string AdName { get; set; }
        public DateTime Peroid { get; set; }
        public string Notice { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
