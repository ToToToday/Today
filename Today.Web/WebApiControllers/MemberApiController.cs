using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Today.Web.DTOModels.MemberDTO;
using Today.Web.Services.MemberService;
using static Today.Web.DTOModels.MemberDTO.MemberDTO;
using static Today.Web.ViewModels.MemberVM;

namespace Today.Web.WebApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberApiController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberApiController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("CountSetting")] //提交
        public IActionResult CountSetting([FromBody] MemberInfo requestParam)
        {
            var memberId = int.Parse(User.Identity.Name);
            var inputDto = new MemberResponseDTO
            {
                MemberId = memberId,
                MemberName = requestParam.MemberName,
                CityId = requestParam.CityId,
                Age = requestParam.Age,
                Phone = requestParam.Phone,
                //IdentityCard = requestParam.IdentityCard,
                Gender = requestParam.Gender,
            };

            var entity = _memberService.UpdateMember(inputDto);

            return Ok(entity);
        }
    }
}
