using Dapper;
using System.Collections.Generic;
using System.Data;
using Today.Model.Models;


namespace TodayMVC.Admin.Repositories.DapperOrderRepositories
{
    public class DapperOrderRepository : DapperBaseRepository, IDapperOrderRepository
    {
        public DapperOrderRepository(IDbConnection conn) : base(conn)
        { }

        public int Create(OrderDetail entity)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(OrderDetail entity)
        {
            throw new System.NotImplementedException();
        }

        public OrderDetail GetOne(OrderDetail entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<OrderDetail> SelectAll()
        {

            var sql = @"SELECT
                         od.OrderDetailsId,
                         od.Quantity,
                         od.UnitPrice,
                         o.OrderId split_on,
                         o.OrderDate,
                         o.Status,
                         m.MemberId split_on,
                         m.MemberName,
                         s.SpecificationId split_on,
                         s.Itemtext,    
                         pg.ProgramId split_on,
                         pg.Title,
                         p.ProductId split_on,
                         p.ProductName
                        FROM OrderDetail od
                        INNER JOIN [Order] o ON o.OrderId = od.OrderId
                        INNER JOIN Member m ON o.MemberId = m.MemberId
                        INNER JOIN ProgramSpecification s ON od.SpecificationId = s.SpecificationId
                        INNER JOIN Program pg ON s.ProgramId = pg.ProgramId
                        INNER JOIN Product p ON pg.ProductId = p.ProductId";

            var result = _conn.Query<OrderDetail, Order, Member, ProgramSpecification, Today.Model.Models.Program, Product, OrderDetail>(sql, (od, o, m, ps, pg, pd) =>
            {
                od.Order = o;
                o.Member = m;
                od.Specification = ps;
                pg.Product = pd;
                ps.Program = pg;

                return od;
            }, splitOn: "split_on");

            
            return result;
            
        }

        public int Update(OrderDetail entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
