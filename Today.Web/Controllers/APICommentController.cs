using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Today.Model.Models;

namespace Today.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APICommentController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateMemberComment(Comment a)
        {
            return Ok("新增");
        }
    }
}
