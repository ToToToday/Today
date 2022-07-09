using System.Collections.Generic;
using Today.Web.ViewModels;

namespace Today.Web.Services.CityService
{
    public interface ICityService
    {
        List<CityVM> GetCityPages(int id);
        List<RaiderVM> GetRaiders(int id);
    }
}
