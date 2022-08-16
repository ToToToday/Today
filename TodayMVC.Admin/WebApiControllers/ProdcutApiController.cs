using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TodayMVC.Admin.DTOModels.ProductDTO;
using TodayMVC.Admin.Services.ProductServices;
namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProdcutApiController : ControllerBase
    {
        private readonly ICreateProductServices _createProductServices;
        public ProdcutApiController (ICreateProductServices createProductServices)
        {
            _createProductServices = createProductServices; 
        }
        //[Http]
        //public  IActionResult Create(CreateProductDTO product)
        [HttpPost]
        public IActionResult Create(CreateProductDTO createProductDTO)
        {
            try
            {
                _createProductServices.CreateProduct(createProductDTO);
                return Ok("新增成功");
            }
            catch(Exception eX)
            {
                return Ok(eX.Message);
            }
        }
    }
}
