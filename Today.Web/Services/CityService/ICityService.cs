using System.Collections.Generic;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.CityDTO;
using static Today.Web.DTOModels.RaiderDTO;

namespace Today.Web.Services.CityService
{
    public interface ICityService
    {
        CityResponseDTO GetCity(CityRequestDTO request);

        RaiderResponseDTO GetRaiders(RaiderRequestDTO request);
    }
}
