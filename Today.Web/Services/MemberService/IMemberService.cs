using System.Collections.Generic;
using Today.Web.DTOModels;
using Today.Web.DTOModels.MemberDTO;
using static Today.Web.DTOModels.MemberDTO.MemberDTO;

namespace Today.Web.Services.MemberService
{
    public interface IMemberService
    {
        List<CityRegion> AllCityList();
        MemberResponseDTO GetMember(MemberRequestDTO MemberId);
        ResponseDTO UpdateMember(MemberResponseDTO input);
        string GetMemberName(MemberRequestDTO MemberId);
        int GetLongWayName();
    }
}
