using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Today.Model.Models;
using Today.Model.Repositories;

namespace Today.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class apiProductinfoController : ControllerBase
    {
        private readonly IGenericRepository _productInfoService;

        public apiProductinfoController(IGenericRepository productInfoService)
        {
            _productInfoService = productInfoService;
        }
        [HttpPost]
        public IActionResult CreateProductinfo(ViewModels.ProductInfoVM a)
        {
            ;
            var sohhpingcard = new ShoppingCart
            {
                ShoppingCartId = 1,
                MemberId = 1,
                SpecificationId = 2,
                //DepartureDate = "2022-08-22",
                Quantity = 123,
                ScreeningId = 1,
                JoinTime =  DateTime.Now, 

    };
            _productInfoService.Create(sohhpingcard);
            _productInfoService.SavaChanges();
            return Ok("新增");
        }
    }
}
