using Microsoft.AspNetCore.Mvc;

using Today.Web.DTOModels.DateRequest;
using Today.Web.Services.ClassifyService;
using static Today.Web.DTOModels.ClassifyDTO.ClassifyDTO;

namespace Today.Web.apiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ApiDateController : ControllerBase
    {
        private readonly IClassifyService _classifyService;
        public ApiDateController(IClassifyService classifyService)
        {
            _classifyService = classifyService;
        }


        [HttpPost]
        public IActionResult Date([FromBody] DateRequestModel dat)
        {
            var date = new ClassifyRequestDTO
            {
                RealDate = dat.DateRange
            };

            var dateList = _classifyService.GetClassifyPages(date);
            return Ok(dateList);

        }

    }
}
