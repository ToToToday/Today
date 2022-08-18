using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using Today.Model;
using Today.Model.Models;
using TodayMVC.Admin.Repositories.DapperCommentManage;
using TodayMVC.Admin.ViewModels;

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
        public string GetAllComment()
        {
            var dataSource = _membercommentdapper.SelectAllComment();


            var result = dataSource.GroupBy(row => row.P.ProductName)
                .Select(g =>
                new
                {
                    ProductName = g.Key,
                    Comments = g.Select(row => row.C),
                    Title=g.Take(1).Select(row => row.L),
                    Path=g.Take(1).Select(row => row.PP),
                    MemberName=g.Select(row => row.M)
                }
            );
            try
            {
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public int DeleteComment([FromBody]DeleteCommentVM id)
        {
            //var dataSource = _membercommentdapper.SelectAllComment();
            //var linq = dataSource.Where(row => row.C.CommentId == a.CommentId);
            //var result = _membercommentdapper.Delete(linq);
            //return result;
            var ci = new Comment { CommentId = id.CommentId };
            var dataSource = _membercommentdapper.Delete(ci);
            return dataSource;
        }
    }
    
}
