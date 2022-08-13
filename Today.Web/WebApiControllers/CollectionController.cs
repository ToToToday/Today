using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Today.Web.Models.ShopCartAPI;
using Today.Web.Services.CollectService;
using Today.Web.ViewModels;

namespace Today.Web.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;
        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public string CheckMemberLoginStatus()
        {
            bool logingStatus = true;

            if (User.Identity.Name == null)
            {
                logingStatus = false;
            }

            var result = JsonConvert.SerializeObject(new { Status = logingStatus });

            return result;
        }

        [HttpPost]
        public IActionResult AddCollect([FromBody] CollectionVM request)
        {
            request.MemberId = int.Parse(User.Identity.Name);
            request.Time = DateTime.UtcNow.AddHours(8);

            _collectionService.CreateCollect(request);

            try
            {
                return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            }
            catch(Exception ex)
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, false));
            }
        }

        [HttpPost]
        public IActionResult RemoveCollect([FromBody] CollectionVM request)
        {
            request.MemberId = int.Parse(User.Identity.Name);
            _collectionService.RemoveCollect(request);

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
