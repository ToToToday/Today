using System.Collections.Generic;
using System.Threading.Tasks;
using Today.Web.DTOModels.ClassifyDTO;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ClassifyDTO.ClassifyDTO;

namespace Today.Web.Services.ClassifyService
{
    public interface IClassifyService
    {
        ClassifyDTO GetClassifyPages(ClassifyRequestDTO categoryId);

        ClassifyDTO GetClassifyMatchedProducts(FilterDTO input);

    }
}
