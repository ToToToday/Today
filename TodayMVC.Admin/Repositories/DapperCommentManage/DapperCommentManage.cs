using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Today.Model.Models;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.Repositories.DapperCommentManage
{
    public class DapperCommentManage : DapperBaseRepository, IDapperCommentManage
    {
        public DapperCommentManage(IDbConnection conn):base(conn)
        {

        }

        public IEnumerable<CommentVM> SelectAllComment()
        {
            var productComment = @"select 
                                    p.ProductName,
                                    l.LocationId split_on,
                                    l.Title,
                                    pp.PhotoId split_on,
                                    pp.Path,
                                    c.OrderDetailsId split_on,
                                    c.CommentId,
                                    c.RatingStar,
                                    c.CommentDate,
                                    c.PartnerType,
                                    c.CommentTitle,
                                    c.CommentText,
                                    m.MemberId split_on,
                                    m.MemberName
                                from Product p
                                inner join Location l on p.ProductId=l.ProductId
                                inner join ProductPhoto pp on p.ProductId=pp.ProductId
                                inner join Comment c on p.ProductId=c.ProductId
                                inner join Member m on c.MemberId=m.MemberId
                                where pp.Sort=1
                                order by p.ProductId asc,c.CommentDate desc";

            var result = _conn.Query<Product, Location, ProductPhoto, Comment, Member, CommentVM>(
                productComment, (p, l, pp, c, m) =>
                {
                    return new CommentVM
                    {
                        P = p,
                        L = l,
                        PP = pp,
                        C = c,
                        M = m,
                    };
                }, splitOn: "split_on"
            );

            return result;
        }

        public int Create(Comment entity)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(Comment entity)
        {
            var productComment = @"DELETE FROM Comment
                                   WHERE CommentId= @CommentId";
            var result = _conn.Execute(productComment, new { entity.CommentId});
            return result;
        }

        public IEnumerable<Comment> SelectAll()
        {
            throw new System.NotImplementedException();
        }

        public int Update(Comment entity)
        {
            throw new System.NotImplementedException();
        }

        public Comment GetOne(Comment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
