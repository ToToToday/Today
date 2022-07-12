
using System.Collections.Generic;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;
using System.Linq;

namespace Today.Web.Services.locationService
{
    public class LocationService: ILocationService
    {
        private readonly IGenericRepository _repo;
        public LocationService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public List<LocationVM> GetLocations()
        {
            var locations = (from location in _repo.GetAll<Location>() where location.LocationId >= 1 select location)
                .Select(lo => new LocationVM { Id = lo.LocationId, latitude = lo.Latitude, longitude = lo.Longitude }).ToList();
            return locations;
        }
    }
}
