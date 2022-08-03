
using System.Collections.Generic;
using Today.Model.Models;
using Today.Model.Repositories;
using System.Linq;
using Today.Web.DTOModels.locationDTO;
using static Today.Web.DTOModels.locationDTO.LocationDTO;

namespace Today.Web.Services.locationService
{
    public class LocationService:ILocationService
    {
        private readonly IGenericRepository _repo;
        public LocationService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public LocationDTO GetLocation()
        {
            var productLocation = _repo.GetAll<Location>();
            var products = _repo.GetAll<Product>();
            var photo = _repo.GetAll<ProductPhoto>();
            var result = new LocationDTO() { ProductLocationList = new List<ProductLocationDTO>() };     
            var Geting = (from pro in products
                          join loca in productLocation on pro.ProductId equals loca.ProductId
                          join pho in photo on loca.ProductId equals pho.ProductId
                          where pho.Sort == 1
                          select new {ProductId = pro.ProductId , LocationId =loca.LocationId, ProductName = pro.ProductName, Longitude = loca.Longitude, Latitude = loca.Latitude, Path = pho.Path  }
                           ).ToList();

            foreach (var item in Geting)
            {
                result.ProductLocationList.Add(new ProductLocationDTO() { ProductId = item.ProductId, LocationId = item.LocationId ,  ProductName = item.ProductName, Longitude = item.Longitude, Latitude = item.Latitude, Path = item.Path });
            }

            return result;
        }
        //public LocationDTO GetCategory()
        //{
        //    var category = _repo.GetAll<Category>();
            
        //}

    }
}
