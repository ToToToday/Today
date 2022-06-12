using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class City
    {
        public City()
        {
            CityRaiders = new HashSet<CityRaider>();
            MemberInfoIds = new HashSet<MemberInfoId>();
            Products = new HashSet<Product>();
            Suppliers = new HashSet<Supplier>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityImg { get; set; }
        public string CityIntrod { get; set; }
        public bool IsIsland { get; set; }

        public virtual ICollection<CityRaider> CityRaiders { get; set; }
        public virtual ICollection<MemberInfoId> MemberInfoIds { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
