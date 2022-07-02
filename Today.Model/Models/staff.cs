using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class staff
    {
        public staff()
        {
            CityRaiders = new HashSet<CityRaider>();
            CouponManages = new HashSet<CouponManage>();
        }

        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string PassWord { get; set; }

        public virtual ICollection<CityRaider> CityRaiders { get; set; }
        public virtual ICollection<CouponManage> CouponManages { get; set; }
    }
}
