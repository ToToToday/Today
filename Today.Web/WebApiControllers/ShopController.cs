using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Today.Web.DTOModels.ShopCartDTO;
using Today.Web.Models.ShopCartAPI;
using Today.Web.Services.ShopCartService;
using Today.Web.ViewModels;
using static Today.Web.ViewModels.ShopCartVM;

namespace Today.Web.WebApiControllers
{
    [Route("api/[controller]")]
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

            var input = new CreateShopCartInputDTO
            {
                //MemberId = int.Parse(User.Identity.Name),
                ProductName = request.ProductName,
                ProgramTitle = request.ProgramTitle,
                DepartureDate = request.DepartureDate,
                Path = request.Path,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                UnitText = request.UnitText,
                SpecificationId = request.SpecificationId,
                ScreenTime = request.ScreenTime,
                ScreeningId = request.ScreeningId
            };
            var result = _shopCartService.CreateShopCart(input);
            if (result.IsSuccess == true)
            {
                return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            }
            else
            {
                return Ok(new APIResult(APIStatus.Fail, result.Message, false));
            }




        }
    }
}
