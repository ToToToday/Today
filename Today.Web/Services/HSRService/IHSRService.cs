using System.Collections.Generic;
using System.Threading.Tasks;
using Today.Web.ViewModels;

namespace Today.Web.Services.OrderService
{
    public interface IHSRService
    {
        List<HSRVM> GetHSRpages();
    }
}
