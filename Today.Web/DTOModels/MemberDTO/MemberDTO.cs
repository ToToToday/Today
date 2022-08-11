using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.DTOModels.MemberDTO
{
    //DTO傳輸用，從資料庫抓出來
    public class MemberDTO
    {
        public class MemberRequestDTO //請求 inpute
        {
            public int MemberId { get; set; }
        }
        public class ResponseDTO
        {
            public bool IsSuccess { get; set; }
            public int Code { get; set; }
            public object Data { get; set; }
            public string Message { get; set; }

        }
        public class MemberResponseDTO //回應 outpute
        {
            public int MemberId { get; set; }
            public string MemberName { get; set; }
            public int? CityId { get; set; }
            public int? Age { get; set; }
            public string Phone { get; set; }
            //public string IdentityCard { get; set; }
            public bool? Gender { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public List<CityRegion> AllCityList { get; set; }
        public class CityRegion
        {
            public int CityId { get; set; }
            public string CityName { get; set; }
        }

        public int LongWayName { get; set; }
        
    }
}
