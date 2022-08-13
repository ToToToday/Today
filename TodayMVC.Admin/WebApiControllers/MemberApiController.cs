using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TodayMVC.Admin.Repositories.DapperMemberRepositories;
using TodayMVC.Admin.Services.MemberService;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberApiController : ControllerBase
    {
        //private readonly IMemberService _memberService;
        //public MemberApiController(IMemberService memberService)
        //{
        //    _memberService = memberService;
        //}
        private readonly IDapperMemberRepository _dapperRepo;
        public MemberApiController(IDapperMemberRepository dapperRepo)
        {
            _dapperRepo = dapperRepo;
        }
        [HttpGet]
        public IActionResult GetMemberList()
        {
            var dataSource = _dapperRepo.SelectAll();

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
