using System.Collections.Generic;
using System.Threading.Tasks;
using Today.Web.DTOModels.ClassifyDTO;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ClassifyDTO.ClassifyDTO;

namespace Today.Web.Services.ClassifyService
{
    public interface IClassifyService
    {
        //CityinputResponseDTO CityInput(CityinputRequestDTO search);



        //ClassifyDTO GetClassifyPages(ClassifyRequestDTO categoryId);
        GetAllFiltersOutputDTO GetClassifyFilters();

        //GetAllSortsOutputDTO GetClassifySorts();

        ClassifyDTO GetClassifyMatchedCards(FilterDTO input);
        //List<ClassifyDTO.ClassifyCardInfo> AddClassifyCardToResult(List<Product> product);
        //ClassifyDTO AddClassifyCardToResult();

    }
}
