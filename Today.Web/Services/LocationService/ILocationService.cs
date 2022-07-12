using System.Collections.Generic;
using Today.Web.ViewModels;

namespace Today.Web.Services.locationService
{
    public interface ILocationService
    {
        List<LocationVM> GetLocations();
    }
}
