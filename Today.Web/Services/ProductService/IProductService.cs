using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Web.DTOModels.ProductDTO;
using static Today.Web.DTOModels.ProductDTO.ProductDTO;

namespace Today.Web.Services.ProductService
{
    public interface IProductService
    {
        ProductResponseDTO ToCityPage(ProductRequestDTO search);
        ProductDTO AllProduct();
        ProductDTO GetAllProductCard();
        ProductDTO PopularCityCard();
        abstract List<string> MaybeCategoryList(List<CategoryInfo> source, string target);
    }
}
