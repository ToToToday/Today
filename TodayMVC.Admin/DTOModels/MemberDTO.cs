using System.Collections.Generic;

namespace TodayMVC.Admin.DTOModels
{
    public class MemberDTO
    {
        public List<MemberInfo> MemberList { get; set; }
        public class MemberInfo
        {
            public int MemberId { get; set; }
            public string MemberName { get; set; }
            public bool? Gender { get; set; }
            public int? CityName { get; set; }
        }
    }
}
