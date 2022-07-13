using System.Collections.Generic;
using Today.Web.ViewModels;
using static Today.Web.DTOModel.ProductInfoDTO;

namespace Today.Web.Services.ProductPagesService
{
    public interface IProductPagesService
    {
        ProductInfoResponseDTO GetProduct(ProductInfoRequstDTO requst);
        //List<ProductPagesVM> GetProductPages();
    }
}
