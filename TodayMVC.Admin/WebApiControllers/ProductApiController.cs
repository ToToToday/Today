using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using TodayMVC.Admin.Repositories.DapperProductRepositories;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IDapperProductRepository _dapperProductRepository;

        public ProductApiController(IDapperProductRepository dapperProductRepository)
        {
            _dapperProductRepository = dapperProductRepository;
        }

        [HttpGet]
        public string UpdateProductInfo()
        {
            var productInfo = _dapperProductRepository.SelectAllProduct();

            try
            {
                return JsonConvert.SerializeObject(productInfo);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
