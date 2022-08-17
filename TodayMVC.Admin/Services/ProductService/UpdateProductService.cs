using System.Linq;
using TodayMVC.Admin.Repositories.DapperProductRepositories;
using static TodayMVC.Admin.DTOModels.ProductDTO.UpdateProductDTO;

namespace TodayMVC.Admin.Services.ProductService
{
    public class UpdateProductService
    {
        private readonly IDapperProductRepository _dapperRepo;
        public UpdateProductService(IDapperProductRepository dapperRepo)
        {
            _dapperRepo = dapperRepo;
        }
        //public ProductInfoResponseDTO GetAllProduct(ProductInfoRequstDTO requst)
        //{
        //    if (requst.ProductId <= 0) return null;

        //    var source = _dapperRepo.SelectAll().FirstOrDefault(p => p.ProductId == requst.ProductId);

        //    if(source == null) return null;

        //    var citySource = _dapperRepo.SelectAll().First(c => c.CityId == source.CityId);
        //    var locationSource = _dapperRepo.SelectAll().First(l => l.ProductId == source.ProductId);
        //    var productPhotoSource = _dapperRepo.SelectAll().Where(pp => pp.ProductId == source.ProductId);
        //    var programSource = _dapperRepo.SelectAll().Where(p => p.ProductId == source.ProductId);


        //    var result = new ProductInfoResponseDTO()
        //    {
        //        ProductInfo = new Product()
        //        {
        //            ProductName = source.ProductName,
        //            CityName = citySource
        //        }
        //    };

        //}
    }
}
