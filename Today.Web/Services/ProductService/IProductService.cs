using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Web.DTOModels.ProductDTO;

namespace Today.Web.Services.ProductService
{
    public interface IProductService
    {
        //List<FilterVM> Filter();
        ProductDTO GetProduct();
        //List<ProductDTO> GetProduct();
    }
}
