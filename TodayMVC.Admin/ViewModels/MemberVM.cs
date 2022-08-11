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
            public bool Gender { get; set; }
            public string CityName { get; set; }
        }
    }
}
