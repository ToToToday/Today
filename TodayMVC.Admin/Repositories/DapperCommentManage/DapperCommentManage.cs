using Dapper;
using System.Collections.Generic;
using System.Data;
using Today.Model.Models;

namespace TodayMVC.Admin.Repositories.DapperCommentManage
{
    public class DapperCommentManage : DapperBaseRepository, IDapperCommentManage
    {
        public DapperCommentManage(IDbConnection conn):base(conn)
        {

        }
        public int Create(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public Product GetOne(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> SelectAll()
        {
            var productComment = @"select 
                                 p.ProductName,
                                    l.LocationId split_on,
                                    l.Title,
                                    pp.PhotoId split_on,
                                    pp.Path,
                                    c.CommentId split_on,
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
            //class
            var result = _conn.Query<Product, Location, ProductPhoto, Comment, Member, Product>(
                productComment, (p, l, pp, c, m) =>
                {
                    c.Member = m;
                    c.Product = p;
                    l.Product = p;
                    pp.Product = p;
                    return p;
                }, splitOn: "split_on"
            );
            return result;
        }

        public int Update(Product entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
