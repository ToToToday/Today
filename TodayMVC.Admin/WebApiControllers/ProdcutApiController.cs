using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TodayMVC.Admin.DTOModels.ProductDTO;
using TodayMVC.Admin.Services.ProductServices;
using System.Text.Json;
using Newtonsoft.Json;

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
        public IActionResult Create([FromBody]CreateProductDTO createProductDTO)
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
        [HttpGet]
        public string get()
        {
            try
            {
                var result =  _createProductServices.get();
                return JsonConvert.SerializeObject(result);
                //return JsonConvert.SerializeObject(result);
                
            }
            catch(Exception ex)
            {
                return ex.Message;

            }
        }
    }
}
