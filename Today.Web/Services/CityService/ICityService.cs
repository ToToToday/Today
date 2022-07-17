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
        
        CityResponseDTO GetCity(CityRequestDTO request);
        List<CityDTO.City> GetAllCity();
        List<RaiderCard> GetRaiderCard();
        List<CommentCard> GetAllComment();
        RaiderResponseDTO GetRaiders(RaiderRequestDTO request);
       

       
    }
}
