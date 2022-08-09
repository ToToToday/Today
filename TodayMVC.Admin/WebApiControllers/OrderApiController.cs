using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        public IActionResult AllOrder()
        {
            return Ok();
        }
    }
}
