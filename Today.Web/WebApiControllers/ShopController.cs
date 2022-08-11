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

        [HttpPost]
        public IActionResult AddCart([FromBody] ShopCartRequestVM request)
        {
            request.MemberId = int.Parse(User.Identity.Name);
            _shopCartService.CreateShopCard(request);

            try 
            {
                return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            }
            catch (Exception ex) 
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, false));
            }



        }

        [HttpDelete]
        public IActionResult DeleteCard([FromBody]DeleteCardVM request)
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
