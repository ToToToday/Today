using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class RaidersManage
    {
        public int RaidersManageId { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsUpdate { get; set; }
        public int Status { get; set; }
        public bool Isdeleted { get; set; }
        public int RaidersId { get; set; }
        public int StaffId { get; set; }
    }
}
