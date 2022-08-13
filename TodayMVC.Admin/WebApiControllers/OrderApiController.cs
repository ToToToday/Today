using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Today.Model;
using Today.Model.Models;
using TodayMVC.Admin.Repositories.DapperOrderRepositories;
using TodayMVC.Admin.Services.OrderServices;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IDapperOrderRepository _iDapperOrderRepository;
        public OrderApiController(IDapperOrderRepository iDapperOrderRepository)
        {
            _iDapperOrderRepository = iDapperOrderRepository;
        }
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            
            var order =_iDapperOrderRepository.SelectAll();
            try
            {
                return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            }
            catch (Exception ex)
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, null));
            }
            
        }
    }
}
