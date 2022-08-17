using System;
using System.Collections.Generic;
using System.Linq;

namespace Today.Web.DTOModels.locationDTO
{
    public class LocationDTO
    {   
    
        public List<ProductLocationinfo> ProductLocationList { get; set; }
        public List<CityLocationInfo> CityLocationList { get; set; }
        public List<GetParentCard> GetParentCardList { get; set; }

        public List<OffCity> OffIslandList { get; set; }
        public class ProductLocationinfo
        {
            public int ProductId { get; set; }
            public int LocationId { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string ProductName { get; set; }
            public int CityId { get; set; }
            public bool IsIsland { get; set; }
            public string CityName { get; set; }
            public decimal? Price { get; set; }
            public int PhotoId { get; set; }
            public string Path { get; set; }
            public float RatingStar { get; set; }
            public int Sort { get; set; }
            public PriceInfo Prices { get; set; }
            public string Itemtext { get; set; }
            public int CategoryId { get; set; }

        }
        public class GetParentCard
        {
            public int ProductId { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public int CityId { get; set; }
            public string CityName { get; set; }
            public List<string> Tags { get; set; }
            public RatingInfo Rating { get; set; }
            public int TotalOrder { get; set; }
            public PriceInfo Prices { get; set; }
            public bool Favorite { get; set; }
        }

        public class CityLocationInfo
        {
            public int ProductId { get; set; }
            public int CityId { get; set; }
            public string City_Latitude { get; set; }
            public string City_Longtitude { get; set; }
        }

        public class OffCity
        {
            public int CityId { get; set; }
            public string CityName { get; set; }
            public string CityImage { get; set; }
        }
        public class PriceInfo
        {
            public decimal? OriginalPrice { get; set; }
            public decimal? Price { get; set; }
        }
        public class RatingInfo
        {
            public float RatingStar { get; set; }
            public int TotalGiveComment { get; set; }
        }

    }
}
