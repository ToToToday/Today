using System.Threading.Tasks;
using TodayMVC.Admin.DTOModels.UploadDTO;

namespace TodayMVC.Admin.Services.CloudinaryService
{
    public interface ICloudinaryService2
    {
        Task<UploadImgOutputDTO> UploadAsync(UploadImgInputDTO input);
    }
}
