using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels.CreateOrderDTO;
using Today.Web.ViewModels;

namespace Today.Web.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository _repo;

        public OrderService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public int CreateOrder(CreateOrderRequstDTO requst)
        {
            var order = new Order
            {
                MemberId = (int)requst.MemeberID,
                OrderDate = DateTime.UtcNow.AddHours(8),
                PaymentId = 1,
                Status = 1,
                StatusUpdate = 1,
            };
            _repo.Create(order);
            _repo.SavaChanges();

            var Memberorder = _repo.GetAll<Order>().Where(o => o.MemberId == requst.MemeberID ).OrderByDescending(o => o.OrderDate).First();

            var cartList = _repo.GetAll<ShoppingCart>().Where(cart => cart.MemberId == requst.MemeberID);
            var programSpecList = _repo.GetAll<ProgramSpecification>();

            foreach (var cartId in requst.ShoppingCradIdList)
            {
                var currentCart = cartList.First(cart => cart.ShoppingCartId == cartId);
                var currentSpec = programSpecList.First(spec => spec.SpecificationId == currentCart.SpecificationId);
                var orderDtail = new OrderDetail
                {
                    OrderId = Memberorder.OrderId,
                    SpecificationId = currentCart.SpecificationId,
                    Quantity = currentCart.Quantity,
                    DepartureDate = currentCart.DepartureDate,
                    UnitPrice = currentSpec.UnitPrice,
                    Itemtext = currentSpec.Itemtext,
                    //Discount = 0,
                    //LeaseTime= ,
                    //TicketsId = 0,
                };
                _repo.Create(orderDtail);
                _repo.Delete(currentCart);
            }
            _repo.SavaChanges();

            return Memberorder.OrderId;
        }
    
        public int directCreateOrder(ShopCartRequestVM request)
        {

            var order = new Order
            {
                MemberId = (int)request.MemberId,
                OrderDate = DateTime.UtcNow.AddHours(8),
                PaymentId = 1,
                Status = 1,
                StatusUpdate = 1,
            };
            _repo.Create(order);
            _repo.SavaChanges();


            var Memberorder = _repo.GetAll<Order>().Where(o => o.MemberId == request.MemberId).OrderByDescending(o => o.OrderDate).First();
            var programSpecList = _repo.GetAll<ProgramSpecification>();

            foreach (var item in request.SpecificationList)
            {
                var currentSpec = programSpecList.First(spec => spec.SpecificationId == item.SpecificationId);
                var orderDtail = new OrderDetail
                {
                    OrderId = Memberorder.OrderId,
                    SpecificationId = item.SpecificationId,
                    Quantity = item.Quantity,
                    DepartureDate = DateTime.Parse(request.DepartureDate),
                    UnitPrice = currentSpec.UnitPrice,
                    Itemtext = currentSpec.Itemtext,
                    //Discount = 0,
                    //LeaseTime= ,
                    //TicketsId = 0,
                };
                _repo.Create(orderDtail);
            }
            _repo.SavaChanges();

            return Memberorder.OrderId;
        }

    }
}
