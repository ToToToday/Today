using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Today.Model.Models;
using TodayMVC.Admin.Repositories.DapperCommentManage;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APICommentManageController : ControllerBase
    {
        private readonly IDapperCommentManage _membercommentdapper;
        public APICommentManageController(IDapperCommentManage membercommentdapper)
        {
            _membercommentdapper = membercommentdapper;
        }
        [HttpGet]
        public IActionResult GetAllComment()
        {
            var dataSource = _membercommentdapper.SelectAll();

            try
            {
                return Ok("成功");
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
    
}
