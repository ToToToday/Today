using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodayMVC.Admin.Services.SalesServices;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesApiController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesApiController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public string GetSalesMonth()
        {
            var dateResult = _salesService.GetSalesMonth().SalesList;
            return JsonConvert.SerializeObject(dateResult);
        }

        [HttpGet]
        public string GetSalesSevenDays()
        {
            var dateResult = _salesService.GetSalesSevenDays().WeekSalesList;
            return JsonConvert.SerializeObject(dateResult);
        }

    }   
}
