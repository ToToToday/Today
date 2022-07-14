using System.Collections.Generic;
using Today.Model.Models;
using Today.Web.DTOModels;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.CityDTO;
using static Today.Web.DTOModels.RaiderDTO;

namespace Today.Web.Services.CityService
{
    public interface ICityService
    {
        //List<CityDTO> GetCity(int id);
        CityResponseDTO GetCity(CityRequestDTO request);
        CityDTO GetAllCity();
        CityDTO GetRaiderCard();
        RaiderResponseDTO GetRaiders(RaiderRequestDTO request);
    }
}
