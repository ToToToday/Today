
using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Repositories;
using TodayMVC.Admin.AdminEnum;
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
            var getOrder = orderData.OrderByDescending(O => O.OrderDetailsId).Select(x => new OrderInfo
            {
                OrderId = x.OrderDetailsId,
                OrderDate = String.Format("{0:yyyy-MM-dd HH:mm}", x.Order.OrderDate),
                MemberName = x.Order.Member.MemberName,
                ProductName = (x.Specification.Program.Product.ProductName) == null ? string.Empty : (x.Specification.Program.Product.ProductName).Substring(0, Math.Min(30, (x.Specification.Program.Product.ProductName).Length)),
                ItemText = x.Specification.Itemtext,
                Quantity = x.Quantity,
                TotalPrice = (int)x.UnitPrice * x.Quantity,
                Status = (x.Order.Status).ToDescription<AllEnum.OrderStatu>(),
                ProgramName = (x.Specification.Program.Title) == null ? string.Empty : (x.Specification.Program.Title).Substring(0, Math.Min(30, (x.Specification.Program.Title).Length)),
            }).ToList();
            return getOrder;
        }


    }
}
