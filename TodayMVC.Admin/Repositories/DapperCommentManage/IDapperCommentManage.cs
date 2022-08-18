using System.Collections.Generic;
using Today.Model.Models;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.Repositories.DapperCommentManage
{
    public interface IDapperCommentManage:IDapperGenericRepository<Comment>
    {
        public IEnumerable<CommentVM> SelectAllComment();
    }
}
