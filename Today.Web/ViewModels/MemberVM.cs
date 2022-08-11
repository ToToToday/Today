using System.Collections.Generic;

namespace Today.Web.ViewModels
{
    //只抓我需要的欄位
    public class MemberVM
    {
        public class MemberInfo
        {
            public int? MemberId { get; set; }
            public string MemberName { get; set; }
            public int? CityId { get; set; }
            public string Image { get; set; }
            public int? Age { get; set; }
            public string Phone { get; set; }
            //public string IdentityCard { get; set; } //(之後可能不要了)
            public bool? Gender { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public List<CityList> AllCity { get; set; }
        }

        public class CityList
        {
            public int CityId { get; set; }
            public string CityName { get; set; }
        }

    }
}
