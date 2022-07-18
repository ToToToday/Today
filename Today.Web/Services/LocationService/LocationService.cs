
using System.Collections.Generic;
using Today.Model.Models;
using Today.Model.Repositories;
using System.Linq;
using Today.Web.DTOModels.locationDTO;


namespace Today.Web.Services.locationService
{
    public class LocationService: ILocationService
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
                 locationID = lo.LocationId ,
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
                ProductName = p.ProductName
            }).ToList();
            return result;
        }
        public List<LocationDTO.ProductPictureDTO> GetPhoto()
        {
            var photo = _repo.GetAll<ProductPhoto>();
            if (photo == null) return null;

            var result = photo.Where(fr => fr.Sort==1 ).Select(pho => new LocationDTO.ProductPictureDTO
            {
                PhotoId = pho.PhotoId,
                Path = pho.Path ,
                ProductId = pho.ProductId,
                Sort = pho.Sort
            }).ToList();
            return result;
        }

    }
}
