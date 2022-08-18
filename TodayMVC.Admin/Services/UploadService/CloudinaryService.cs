using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using TodayMVC.Admin.DTOModels.UploadDTO;

namespace TodayMVC.Admin.Services.UploadService
{
    public class CloudinaryService
    {
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;
        //這不需要特意去註冊DI，MVC天生就能注入
        public CloudinaryService(IConfiguration config)
        {
            _config = config;

            Account account = new Account(
                _config["Cloudinary:cloudname"],
                _config["Cloudinary:apikey"],
                _config["Cloudinary:apisecret"]
            );

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }
        public async Task<UploadImgOutputDTO> UploadAsync(UploadImgInputDTO input)
        {
            var result = new UploadImgOutputDTO
            {
                IsSuccess = false,
            };

            //防呆
            var file = input.File;
            if (file == null || file.Length == 0)
            {
                result.Message = "上傳檔案長度為0,失敗!";
                return result;
            }
            //還可以獲取文件的信息，可做一些條件限制(檔案大小上限...等)
            // file.Length / 1024.0;  // 文件大小 KB
            // file.FileName; // 客户端上傳的文件名
            // file.ContentType; // 獲取文件 ContentType 或解析 MIME 類型


            //取得作業系統的完整檔案路徑
            string filePath = Path.Combine(
                 Directory.GetCurrentDirectory(),
                "wwwroot",
                Guid.NewGuid().ToString()   //避免大量使用者同時上傳，檔名重疊覆蓋，使用全域ID，反正待會會刪掉
                    );

            try
            {
                //using (var stream = File.Create(filePath)) //網路上看到的寫法
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    //以非同步方式將上傳檔案內容Copy複製寫入FileStream檔案資料流
                    await file.CopyToAsync(stream);
                }

                //相對路徑
                //string virtualPath = Url.Content("UploadFiles/" + upload.FileName);

                //絕對路徑URL
                //string url = $"{Request.Scheme}://{Request.Host.Value}/{virtualPath}";

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath), //檔案來源  可以直接放網址的圖， 但 本地檔案 得經由上面過程?
                    PublicId = $"{input.Path}/{file.FileName}"//【圖床存放路徑】 問題在於若未來圖片不需要了，怎麼刪??
                                                              //path.Combine
                };

                var uploadResult = _cloudinary.Upload(uploadParams);    //取得回傳結果物件

                //【圖床存放路徑 所導致的圖片位址】!! //
                result.Url = uploadResult.SecureUrl.ToString();
                //resultImgUrl = uploadResult.Url.ToString();
                //resultImgUrl = _cloudinary.GetResource(uploadParams.PublicId).SecureUrl;

                //刪wwwroot的檔案  參考https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders
                File.Delete(filePath);

                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                return result;
            }
        }
    }
}
