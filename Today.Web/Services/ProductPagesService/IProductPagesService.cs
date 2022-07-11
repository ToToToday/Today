using System.Collections.Generic;
using Today.Web.ViewModels;

namespace Today.Web.Services.ProductPagesService
{
    public interface IProductPagesService
    {
        List<ProductPagesVM> GetProductPages();
    }
}
