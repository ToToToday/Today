using System.Collections.Generic;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ProductInfoDTO.ProductInfoDTO;

namespace Today.Web.Services.ProductInfoService
{
    public interface IProductInfoService
    {
        ProductInfoResponseDTO GetProduct(ProductInfoRequstDTO requst);
        //List<ProductPagesVM> GetProductPages();
    }
}
