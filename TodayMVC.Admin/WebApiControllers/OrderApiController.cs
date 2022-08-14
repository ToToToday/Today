using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Today.Model;
using TodayMVC.Admin.Repositories.DapperOrderRepositories;
using TodayMVC.Admin.Services.OrderServices;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public string GetAllOrder()
        {
            
            var orderData = _orderService.OrderList();
            return JsonConvert.SerializeObject(orderData);
        }
    }
}
