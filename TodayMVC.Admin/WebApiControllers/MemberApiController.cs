using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TodayMVC.Admin.Services.MemberService;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberApiController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberApiController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpGet]
        public IActionResult GetMemberList()
        {
            var dataSource = _memberService.GetAllMemberList();

            //try
            //{

            //}
            //catch (Exception ex)
            //{

            //}
            return Ok();
        }
    }
}
