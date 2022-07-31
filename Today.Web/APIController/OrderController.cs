using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Today.Web.DTOModels.OrderDTO.OrderDTO;

namespace Today.Web.APIController
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateOrder(ProductInfoRequstDTO requstDTO)
        {

            
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateOrderDetail()
        {
            return Ok();
        }
    }
}
