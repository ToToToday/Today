using Dapper;
using System.Collections.Generic;
using System.Data;
using Today.Model.Models;

namespace TodayMVC.Admin.Repositories.DapperSalesRepositories
{
    public class DapperSalesRepository : DapperBaseRepository, IDapperSalesRepository
    {
        public DapperSalesRepository(IDbConnection conn) : base(conn)
        { }

        

        public IEnumerable<OrderDetail> SelectAll()
        {
            var sql = @"
                        SELECT
	                        od.Quantity,
	                        od.UnitPrice,
                            o.OrderId split_on,
	                        o.OrderDate,
                            s.SpecificationId split_on,
                            pg.ProgramId split_on,
                            p.ProductId split_on
                        FROM OrderDetail od
                        INNER JOIN [Order] o ON od.OrderId = o.OrderId
                        INNER JOIN ProgramSpecification s ON od.SpecificationId = s.SpecificationId
                        INNER JOIN Program pg ON s.ProgramId = pg.ProgramId
                        INNER JOIN Product p ON pg.ProductId = p.ProductId
                        WHERE o.Status = 1";

            return _conn.Query<OrderDetail, Order, ProgramSpecification, Today.Model.Models.Program, Product, OrderDetail>
                (
                    sql, (od, o, s, pg, p) =>
                    {
                        od.Order = o;
                        s.Program = pg;
                        pg.Product = p;
                        return od;
                    }, splitOn: "split_on"
                );

        }

        public int Create(OrderDetail entity)
        {
            throw new System.NotImplementedException();
        }

        public int Update(OrderDetail entity)
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
    }
}
