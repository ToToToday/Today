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

        [HttpPost]
        public IActionResult AddCart([FromBody] ShopCartRequestVM request)
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
