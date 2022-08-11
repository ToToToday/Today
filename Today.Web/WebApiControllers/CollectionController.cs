using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public IActionResult AddCollect([FromBody] CollectionVM request)
        {
            request.MemberId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;
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
