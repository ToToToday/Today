using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Today.Web.DTOModels.ShopCartDTO;
using Today.Web.Services.ShopCartService;
using Today.Web.ViewModels;
using static Today.Web.ViewModels.ShopCartVM;

namespace Today.Web.Controllers
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

        [HttpPost
        ("{memberId}/{specificationId}/{screeningId}/{productName}/{programTitle}/{path}/{departureDate}/{unitText}/{unitPrice}/{quantity}/{screenTime}")]
        public IActionResult ShopCart
        ([FromRoute] int memberId,
        [FromRoute] int specificationId,
        [FromRoute] int screeningId,
        [FromRoute] string productName,
        [FromRoute] string programTitle,
        [FromRoute] string path,
        [FromRoute] DateTime departureDate,
        [FromRoute] string unitText,
        [FromRoute] string unitPrice,
        [FromRoute] int quantity,
        [FromRoute] string screenTime
        )
        {
            
            try
            {
                //var result = _shopCartService.CreateShopCart();
            }
            catch (Exception ex)
            {

            }

            
            return Ok();

        }
    }
}
