using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Today.Web.ViewModels;
using Today.Web.CommonEnum;
using Today.Web.Helper;
using static Today.Web.CommonEnum.AllEnum;
using Today.Web.Services.MemberCommentService;
using Today.Model.Repositories;
using System;
using System.Linq;

namespace Today.Web.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APICommentController : ControllerBase
    {
        private readonly IMemberCommentService _membercommentservic;
        public APICommentController(IMemberCommentService membercommentservic)
        {
            _membercommentservic = membercommentservic;
        }
        [HttpPost]
        public IActionResult CreateMemberComment(Comment a)
        {
            var result = _membercommentservic.Create(a);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateMemberComment(Comment a)
        {
            var result = _membercommentservic.Update(a);
            return Ok(result);
        }
    }
}
