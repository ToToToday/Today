using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Today.Model;
using TodayMVC.Admin.Repositories.DapperProductRepositories;
using TodayMVC.Admin.Services;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IDapperProductRepository _dapperProductRepository;
        private readonly IUpdateProductService _updateProductService;

        public ProductApiController(IDapperProductRepository dapperProductRepository, IUpdateProductService updateProductService)
        {
            _dapperProductRepository = dapperProductRepository;
            _updateProductService = updateProductService;
        }

        [HttpGet]
        public string ReadProductInfo()
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

        [HttpPost]
        public IActionResult UpdateProductInfo(UpdateProductVM updateProduct)
        {
            try
            {
                _updateProductService.UpdateProduct(updateProduct);

                return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            }
            catch (Exception ex)
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, false));
            }
        }
    }
}
