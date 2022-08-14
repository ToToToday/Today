
using System.Collections.Generic;
using System.Linq;
using Today.Model.Repositories;
using TodayMVC.Admin.Repositories.DapperOrderRepositories;
using static TodayMVC.Admin.ViewModels.OrderVM;

namespace TodayMVC.Admin.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IDapperOrderRepository _orderRepo;
        public OrderService(IDapperOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
            
        }
        public List<OrderInfo> OrderList()
        {
            var orderData = _orderRepo.SelectAll();
            var getOrder = orderData.Select(x => new OrderInfo
            {
                OrderId = x.Order.OrderId,
                OrderDate = x.Order.OrderDate,
                MemberName = x.Order.Member.MemberName,
                ProgramName = x.Specification.Program.Title,
                ItemText = x.Specification.Itemtext,
                Quantity = x.Quantity,
                TotalPrice = (int)x.UnitPrice * x.Quantity,
            }).ToList();
            return getOrder;
        }


    }
}
