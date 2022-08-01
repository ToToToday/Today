using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Web.DTOModels.ShopCartDTO;
using Today.Web.Models.ShopCartAPI;
using Today.Web.Services.ShopCartService;
using Today.Web.ViewModels;

namespace Today.Web.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopCartService _shopCartService;

        public ShopController(IShopCartService shopCartService)
        {
            _shopCartService = shopCartService;
        }

        [HttpPost("AddCart")]
        public IActionResult AddCart([FromBody] ShopCartRequestVM request)
        {       
            //    new CreateShopCartInputDTO
            //{
            //    MemberId = request.MemberId,
            //    DepartureDate = DateTime.Parse(request.DepartureDate),
            //    SpecificationId = request.SpecificationList,
            //    Quantity = request.Quantity,
            //    ScreeningId = request.ScreeningId,

            //};
            _shopCartService.CreateShopCard(request);

            try 
            {
                return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            }
            catch (Exception ex) 
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, false));
            }


            //if (result.IsSuccess == true)
            //{
            //    return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            //}
            //else
            //{
            //    return Ok(new APIResult(APIStatus.Fail, result.Message, false));
            //}


        }

        [HttpDelete]
        public IActionResult DeleteCard(DeleteCardVM request)
        {

            _shopCartService.DeleteShopCard(request);

            try
            {
                return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            }
            catch (Exception ex)
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, false));
            }
        }
    }
}
