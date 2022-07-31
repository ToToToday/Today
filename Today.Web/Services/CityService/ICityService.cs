using System.Collections.Generic;
using Today.Model.Models;
using Today.Web.DTOModels.CityDTO;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.CityDTO.CityDTO;
using static Today.Web.DTOModels.CityDTO.RaiderDTO;

namespace Today.Web.Services.CityService
{
    public interface ICityService
    {
        
        CityResponseDTO GetCity(CityRequestDTO request);
        List<CityDTO.City> GetAllCity(CityRequestDTO request);
        List<RaiderCard> GetRaiderCard(CityRequestDTO request);
        List<CommentCard> GetAllComment(CityRequestDTO request);
        RaiderResponseDTO GetRaiders(RaiderRequestDTO request);
        List<ProductCard> GetNewActiviy(CityRequestDTO request);
        List<ProductCard> GetAboutProduct(CityRequestDTO request);
        List<ProductCard> GetTopTen(CityRequestDTO request);

    }
}
