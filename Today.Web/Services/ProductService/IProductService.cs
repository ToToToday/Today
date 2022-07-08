using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Web.ViewModels;

namespace Today.Web.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<ProductCardVM> GetProductCard();
    }
}
