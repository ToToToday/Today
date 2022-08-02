using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Today.Web.DTOModels.CreateOrderDTO;
using Today.Web.Models.ShopCartAPI;
using Today.Web.Services.OrderService;

namespace Today.Web.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;

        }
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequstDTO requstDTO)
        {
            try
            {
                //requstDTO.MemeberID = int.Parse(User.Identity.Name);
                requstDTO.MemeberID = 2;
                _orderService.CreateOrder(requstDTO);
                return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            }
            catch (Exception ex)
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, null));
            }
        }
        [HttpPost]
        public IActionResult CreateOrderDetail()
        {
            return Ok();
        }
    }
}
