using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class CityRaider
    {
        public int RaidersId { get; set; }
        public int CityId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Video { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Status { get; set; }
        public bool Isdeleted { get; set; }
        public int StaffId { get; set; }
        public string Image { get; set; }

        public virtual City City { get; set; }
        public virtual staff Staff { get; set; }
    }
}
