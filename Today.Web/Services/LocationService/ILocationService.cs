using System.Collections.Generic;

using Today.Web.DTOModels.locationDTO;


namespace Today.Web.Services.locationService
{
    public interface ILocationService
    {
        List<LocationDTO.ProductLocationDTO> GetLocations();
        List<LocationDTO.ProductNameDTO> GetProducts();
        List<LocationDTO.ProductPictureDTO> GetPhoto();
    }
}
