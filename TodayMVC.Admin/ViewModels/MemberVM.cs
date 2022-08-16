using System.Collections.Generic;

namespace TodayMVC.Admin.ViewModels
{
    public class MemberVM
    {
        public List<MemberInfo> MemberList { get; set; }
        public class MemberInfo
        {
            public int MemberId { get; set; }
            public string MemberName { get; set; }
            public string CityName { get; set; }
            public int? Age { get; set; }
            public string Phone { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }

        }
    }
}
