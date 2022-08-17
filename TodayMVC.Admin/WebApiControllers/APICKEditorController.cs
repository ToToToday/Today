using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodayMVC.Admin.DTOModels.UploadDTO;
using TodayMVC.Admin.Services.UploadService;

namespace TodayMVC.Admin.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APICKEditorController : ControllerBase
    {
        private readonly CloudinaryService _cloudinaryService;
        public APICKEditorController(CloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        [HttpPost]
        public async Task<IActionResult> CKEditorMediaAsync(IFormFile upload)//命名一定要upload
        {
            //return CKEditor_Success_Return("https://localhost:5001/去檢查img的src屬性");
            //return CKEditor_Fail_Return("我自訂的錯誤訊息，回傳後應會在CKEditor彈出alert");

            var input = new UploadImgInputDTO
            {
                File = upload,
                Path = "ck",
            };
            var outputDto = await _cloudinaryService.UploadAsync(input);
            //由於CKEditor 只需要回傳上傳後的網址，沒有其他後續寫入DB的動作，故就不再另外包service

            //缺點：圖片一插入就上傳，但 富文本文案不一定會儲存...長期下來會有許多冗圖?

            if (!outputDto.IsSuccess)
            {
                return CKEditor_Fail_Return(outputDto.Message);
            }

            return CKEditor_Success_Return(outputDto.Url);
        }


        [NonAction]
        private IActionResult CKEditor_Success_Return(string imgUrl)
        {
            return Ok(
                new
                {
                    url = imgUrl
                }
            );
        }
        [NonAction]
        private IActionResult CKEditor_Fail_Return(string errMsg)
        {
            return Ok(
                new
                {
                    error = new
                    {
                        message = errMsg,
                    },
                }
            );
        }
    }
}
