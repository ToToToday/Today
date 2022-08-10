using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using TodayMVC.Admin.DTOModels.OrderDTO;
using static TodayMVC.Admin.DTOModels.OrderDTO.OrderDTO;

namespace TodayMVC.Admin.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository _repo;
        public OrderService(IGenericRepository repo)
        {
            _repo = repo;
            
        }
        public List<OrderInfo> GetAllOrder()
        {
            var memberData = _repo.GetAll<Member>();
            var orderData = _repo.GetAll<Order>();
            var orderDetailData = _repo.GetAll<OrderDetail>();
            var SpecificationData = _repo.GetAll<ProgramSpecification>();
            var productData = _repo.GetAll<Product>();
            var programData = _repo.GetAll<Today.Model.Models.Program>();
            var orderMenu = from o in orderData
                            join m in memberData on
                            o.MemberId equals m.MemberId
                            join od in orderDetailData on
                            o.OrderId equals od.OrderId
                            join s in SpecificationData on
                            od.SpecificationId equals s.SpecificationId
                            join pg in programData on
                            s.ProgramId equals pg.ProgramId
                            join p in productData on
                            pg.ProductId equals p.ProductId
                            select new { o.OrderId, m.MemberName, p.ProductName, od.Quantity, od.UnitPrice, o.OrderDate, o.Status };
            return orderMenu.Select(oa => new OrderInfo
            {
                OrderID = oa.OrderId,
                MemberName = oa.MemberName,
                ProductName = oa.ProductName,
                OrderDate = oa.OrderDate,
                OrderStatus = oa.Status,
                Quantity = oa.Quantity,
                TotalPrice = oa.UnitPrice * oa.Quantity,

            }).ToList();
             
        }
        
    }
}
