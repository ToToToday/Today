using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Today.Model;
using TodayMVC.Admin.Services.OrderServices;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            try
            {
                var order = _orderService.GetAllOrder();
                return Ok(new APIResult(APIStatus.Success, string.Empty, order));
            }
            catch (Exception ex)
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, null));
            }
            
        }
    }
}
