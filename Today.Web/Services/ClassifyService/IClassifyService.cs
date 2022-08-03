using System.Collections.Generic;
using System.Threading.Tasks;
using Today.Web.DTOModels.ClassifyDTO;
using Today.Web.ViewModels;

namespace Today.Web.Services.ClassifyService
{
    public interface IClassifyService
    {
        ClassifyDTO.ClassifyDTOinfo GetClassifyPages(ClassifyDTO.ClassifyDTORequest Id);
        //List<ClassifyVM> GetClassifydestination();
    }
}
