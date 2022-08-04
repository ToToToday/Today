using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Today.Web.DTOModels.CreateOrderDTO;
using Today.Web.Models.ShopCartAPI;
using Today.Web.Services.OrderService;
using Today.Web.ViewModels;

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
                requstDTO.MemeberID =  int.Parse(User.Identity.Name);
                var Id =  _orderService.CreateOrder(requstDTO);
                return Ok(new APIResult(APIStatus.Success, string.Empty, new { Id = Id}));
            }
            catch (Exception ex)
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, null));
            }
        }
        [HttpPost]
        public IActionResult directCreateOrder([FromBody] ShopCartRequestVM request)
        {
            try
            {
                //requstDTO.MemeberID = int.Parse(User.Identity.Name);
                request.MemberId = int.Parse(User.Identity.Name);
                var ID =  _orderService.directCreateOrder(request); 
                //return RedirectToAction("Checkout", "Member", new { id  = ID });
                return Ok(new APIResult(APIStatus.Success, string.Empty, new { id = ID }));
            }
            catch (Exception ex)
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, null));
            }
        }
    }
}
