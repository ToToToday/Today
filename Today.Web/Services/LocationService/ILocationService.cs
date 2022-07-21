using System.Collections.Generic;

using Today.Web.DTOModels.locationDTO;


namespace Today.Web.Services.locationService
{
    public interface ILocationService
    {
        List<LocationDTO.ProductLocationDTO> GetLocations();
        List<LocationDTO.ProductNameDTO> GetProducts();
        List<LocationDTO.ProductPictureDTO> GetPhoto();

        List<LocationDTO.ProductCard> ProductCards();
        //List<LocationDTO.CityDTO> GetCity();
        //List<LocationDTO.Program> GetPrograms();
        //List<LocationDTO.ProgramSpecification> GetProgramSpecifications();
        //List<LocationDTO.ProgramCantUseDate> GetProgramCantUseDates();
    }
}
