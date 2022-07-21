
using System.Collections.Generic;
using Today.Model.Models;
using Today.Model.Repositories;
using System.Linq;
using Today.Web.DTOModels.locationDTO;


namespace Today.Web.Services.locationService
{
    public class LocationService:ILocationService
    {
        private readonly IGenericRepository _repo;
        public LocationService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public List<LocationDTO.ProductLocationDTO> GetLocations()
        {
            var productLocation = _repo.GetAll<Location>();
            if (productLocation == null) return null; //防呆
            var result = productLocation.Select( lo => new LocationDTO.ProductLocationDTO
            {
                 LocationID = lo.LocationId ,
                 Latitude =lo.Latitude ,
                 Longitude =lo.Longitude ,
            }).ToList();
            return result;
        }
        public List<LocationDTO.ProductNameDTO> GetProducts()
        {
            var products = _repo.GetAll<Product>();
            if(products == null) return null;

            var result = products.Select(p => new LocationDTO.ProductNameDTO
            {
                ProductId = p.ProductId ,
                ProductName = p.ProductName,
                CityId=p.CityId
            }).ToList();
            return result;
        }
        public List<LocationDTO.ProductPictureDTO> GetPhoto()
        {
            var photo = _repo.GetAll<ProductPhoto>();
            if (photo == null) return null;

            var result = photo.Select(pho => new LocationDTO.ProductPictureDTO
            {
                PhotoId = pho.PhotoId,
                Path = pho.Path ,
                ProductId = pho.ProductId,
                Sort = pho.Sort
            }).ToList();
            return result;
        }


        public List<LocationDTO.CityDTO> GetCity()
        {
            var City = _repo.GetAll<City>();
            if (City == null) return null;

            var result = City.Select(c => new LocationDTO.CityDTO
            {
                CityId = c.CityId,
                CityName = c.CityName,
                IsIsland = c.IsIsland
            }).ToList();
            return result;
        }
        public List<LocationDTO.Program> GetPrograms()
        {
            var programs = _repo.GetAll<Today.Model.Models.Program>();
            if (programs == null) return null;
            var result = programs.Select(p => new LocationDTO.Program
            {
                ProductId = p.ProductId,
                ProgramId = p.ProgramId
            }).ToList();
            return result;
        }
        public List<LocationDTO.ProgramSpecification> GetProgramSpecifications()
        {
            var programSpecification = _repo.GetAll<ProgramSpecification>();
            if (programSpecification == null) return null;
            var result = programSpecification.Select(p => new LocationDTO.ProgramSpecification
            {
                ProgramId = p.ProgramId,
                SpecificationId = p.SpecificationId,
                OriginalUnitPrice = p.OriginalUnitPrice,
                UnitPrice = p.UnitPrice

            }).ToList();
            return result;
        }
        public List<LocationDTO.ProgramCantUseDate> GetProgramCantUseDates()
        {
            var programCantUseDate = _repo.GetAll<ProgramCantUseDate>();
            if (programCantUseDate == null) return null;
            var reuslt = programCantUseDate.Select(p => new LocationDTO.ProgramCantUseDate
            {
                ProgramDateId = p.ProgramDateId,
                ProgramId = p.ProgramId,
                Date = p.Date
            }).ToList();
            return reuslt;
        }
        public List<LocationDTO.ProductCard> ProductCards()
        {
            var products = _repo.GetAll<Product>();
            var photo = _repo.GetAll<ProductPhoto>();
            var City = _repo.GetAll<City>();
            var programs = _repo.GetAll<Today.Model.Models.Program>();
            var programSpecification = _repo.GetAll<ProgramSpecification>();
            var programCantUseDate = _repo.GetAll<ProgramCantUseDate>();

            var result = products.Select(p => new LocationDTO.ProductCard
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CityId = p.CityId,
                CityDTOs = GetCity(),
                Programs = GetPrograms(),
                CantUseDates = GetProgramCantUseDates(),
                ProductPictureDTOs = GetPhoto(),
                ProgramSpecifications = GetProgramSpecifications()
            }).ToList();
            return result;
        }
    }
}
