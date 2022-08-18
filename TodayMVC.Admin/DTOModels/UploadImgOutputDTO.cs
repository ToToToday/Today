using Microsoft.AspNetCore.Http;

namespace TodayMVC.Admin.DTOModels
{
    public class UploadImgDTO
    {
        public class UploadImgInputDTO
        {
            public IFormFile File { get; set; }
            public string Path { get; set; }
        }

        public class UploadImgOutputDTO
        {
            public string Url { get; set; }
        }
    }
}
