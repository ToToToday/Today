using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Today.Web.DTOModels.DateRequest;
using Today.Web.Services.ClassifyService;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ClassifyDTO.ClassifyDTO;

namespace Today.Web.apiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class apiDateController : ControllerBase
    {
        private readonly IClassifyService _classifyService;
        public apiDateController(IClassifyService classifyService)
        {
            _classifyService = classifyService;
        }


        [HttpPost]
        public IActionResult Date([FromBody] DateRequestModel dat  )
        {
            var date = new ClassifyDTORequest
            {
                RealDate = dat.DateRange

            };
            
            var dateList = _classifyService.GetClassifyPages(date);
            return Ok(dateList);

        }
    }
}
