using Microsoft.AspNetCore.Http;

namespace TodayMVC.Admin.DTOModels.UploadDTO
{
    public class UploadImgInputDTO
    {
        public IFormFile File { get; set; }
        public string Path { get; set; }
    }
    public class UploadImgOutputDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
}
