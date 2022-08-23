using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodayMVC.Admin.DTOModels.UploadDTO;
using TodayMVC.Admin.Services.CloudinaryService;

namespace TodayMVC.Admin.Controllers
{
    public class CloudinaryController : Controller
    {
        
        private readonly ICloudinaryService2 _cloudinaryService;

        public CloudinaryController(ICloudinaryService2 cloudinaryService)
        {
            
            _cloudinaryService = cloudinaryService;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Profile([FromForm] IFormFile File, string About)
        {
            var outputDTO = new UploadImgOutputDTO();
            if (File != null)
            {
                var inputDto = new UploadImgInputDTO()
                {
                    File = File
                };
                outputDTO = await _cloudinaryService.UploadAsync(inputDto);

            }
           
            return View(outputDTO);


        }
        [HttpGet]
        public IActionResult Imgtry()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> imgtry([FromForm] IFormFile File, string About)
        {
            var outputDTO = new UploadImgOutputDTO();
            if (File != null)
            {
                var inputDto = new UploadImgInputDTO()
                {
                    File = File
                };
                outputDTO = await _cloudinaryService.UploadAsync(inputDto);

            }

            return View(outputDTO);


        }
    }
}
