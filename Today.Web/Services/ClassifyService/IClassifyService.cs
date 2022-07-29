using System.Collections.Generic;
using System.Threading.Tasks;
using Today.Web.DTOModels.ClassifyDTO;
using Today.Web.ViewModels;

namespace Today.Web.Services.ClassifyService
{
    public interface IClassifyService
    {
        ClassifyDTO GetClassifyPages();
        ClassifyDTO GetClassifylogic();
    }
}
