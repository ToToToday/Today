using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
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
        public string GetMemberList()
        {
            var dataResult = _memberService.GetAllMemberList().MemberList;

            return JsonConvert.SerializeObject(dataResult);
        }
    }
}
