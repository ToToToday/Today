using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public IEnumerable<OrderDetail> GetOne(OrderDetail entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<OrderDetail> SelectAll()
        {

            var sql = @"SELECT o.OrderId,m.MemberName,p.ProductName,o.OrderDate,od.Quantity,od.UnitPrice,o.Status FROM[Order] o INNER JOIN OrderDetail od ON o.OrderId = od.OrderId INNER JOIN Member m ON o.MemberId = m.MemberId INNER JOIN ProgramSpecification s ON od.SpecificationId = s.SpecificationId INNER JOIN Program pg ON s.ProgramId = pg.ProgramId INNER JOIN Product p ON pg.ProductId = p.ProductId";

            //var orderData = _conn.Query<Order, OrderDetail, Member, Product, OrderTb>(sql, (order, od, m, p) =>

            //{
            //    return new OrderTb
            //    {
            //        OrderId = order.OrderId,
            //        ProductName = p.ProductName,
            //        OrderDate = order.OrderDate,
            //        Quantity = od.Quantity,
            //        Price = od.UnitPrice * od.Quantity,
            //        Statu = order.Status
            //    };
            //},
            //splitOn: "Id").ToList();


            //return orderData;
            return _conn.Query<OrderDetail>(sql);
            //var orderDictionary = new Dictionary<int, Order>();
            //var order = _conn.Query<Order, OrderDetail, Member, Product, Order>(sql, (order, od, m, p) =>
            //{
            //    Order orderEntry;
            //    if (!orderDictionary.TryGetValue(order.OrderId, out orderEntry))
            //    {
            //        orderEntry = order;
            //        orderEntry.OrderDetails = new List<OrderDetail>();
            //        orderDictionary.Add(orderEntry.OrderId, orderEntry);
            //    }
            //    orderEntry.OrderDetails.Add(od);
            //    return orderEntry;
            //},
            //splitOn: "Id")
            //.Distinct().ToList();
            //return order;


            ////var uniqueOrderDict = new Dictionary<string, Order>();
            //var orderList = _conn.Query<Order, OrderDetail, Program, ProgramSpecification, Member, Product, Order>(sql, (o, od, pg, s, m, p) =>
            //{
            //    o.OrderDetail = od;
            //    return o


            //});

            //return _conn.Query<Order, OrderDetail, Today.Model.Models.Program, ProgramSpecification, Member, Product>
            //    (@"SELECT 
            //         o.OrderId,
            //         m.MemberName,
            //         p.ProductName,
            //         o.OrderDate,
            //         od.Quantity,
            //         od.UnitPrice * od.Quantity AS Total,
            //         o.Status
            //        FROM [Order] o
            //        INNER JOIN OrderDetail od ON o.OrderId=od.OrderId
            //        INNER JOIN Member m ON o.MemberId=m.MemberId
            //        INNER JOIN ProgramSpecification s ON od.SpecificationId=s.SpecificationId
            //        INNER JOIN Program pg ON s.ProgramId=pg.ProgramId
            //        INNER JOIN Product p ON pg.ProductId=p.ProductId", 
        }

        public int Update(OrderDetail entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
