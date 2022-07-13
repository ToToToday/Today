using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Web.DTO;

namespace Today.Web.Services.ProductService
{
    public interface IProductService
    {
        //List<FilterVM> Filter();
        List<ProductDTO> GetProduct();
    }
}
