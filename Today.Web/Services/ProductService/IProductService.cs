using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Web.DTOModels.ProductDTO;
using static Today.Web.DTOModels.ProductDTO.ProductDTO;

namespace Today.Web.Services.ProductService
{
    public interface IProductService
    {
        ProductDTO GetProduct();
        ProductDTO GetAllProductCard();
        abstract List<int> MaybeCategoryList(List<CategoryInfo> source, string target);
    }
}
