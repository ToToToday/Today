using TodayMVC.Admin.DTOModels.ProductDTO;

namespace TodayMVC.Admin.Services.ProductServices
{
    public interface ICreateProductServices
    {
        public void CreateProduct(CreateProductDTO product);
    }
}