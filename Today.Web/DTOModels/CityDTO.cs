using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.DTOModels
{
    public class CityDTO
    {
        public class CityRequestDTO
        {
            public int CityId { get; set; }
        }

        public class CityResponseDTO
        {
            public City CityInfo { get; set; }

        }
        public class City
        {
            public int Id { get; set; }
            public string CityName { get; set; }
            public string CityIntrod { get; set; }
            public string CityImage { get; set; }
        }
        
    }
}
