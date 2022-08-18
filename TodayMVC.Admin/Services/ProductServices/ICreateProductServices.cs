using TodayMVC.Admin.DTOModels.ProductDTO;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.Services.ProductServices
{
    public interface ICreateProductServices
    {
        public void CreateProduct(CreateProductDTO product);
        public GetVM get();
    }
}