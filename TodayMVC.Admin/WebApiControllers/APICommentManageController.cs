using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
            var dataSource = _membercommentdapper.SelectAllComment();


            var result = dataSource.GroupBy(row => row.P.ProductName)
                .Select(g =>
                new
                {
                    ProductName = g.Key,
                    Comments = g.Select(row => row.C),
                    Title=g.Select(row => row.L),
                    Path=g.Select(row => row.P),
                    MemberName=g.Select(row => row.M)
                }
            );
            try
            {
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    
}
