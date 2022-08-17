using System.Collections.Generic;
using Today.Model.Models;
using Today.Web.DTOModels.locationDTO;


namespace Today.Web.Services.locationService
{
    public interface ILocationService
    {
        LocationDTO GetLocation();
        LocationDTO GetCityLocation(int id);
        LocationDTO GetParentCard();
        LocationDTO GetOffIslandCard();

    }
}
