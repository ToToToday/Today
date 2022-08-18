using System;
using System.Collections.Generic;
using System.Linq;

namespace Today.Web.ViewModels
{
    public class LocationVM : ClassifyVM
    {
        public List<ProductLocation> ProductLocationList { get; set; }
        public List<CityLocation> CityLocationList { get; set; }
        public List<GetParentCard> GetParentCardList { get; set; }
        public List<FilterCity> FilterList { get; set; }
        
        public class ProductLocation
        {
            public int ProductId { get; set; }
            public int LocationId { get; set; }
            public int PhotoId { get; set; }
            public int CityId { get; set; }
            public bool IsIsland { get; set; }
            public decimal? Price { get; set; }
            public string ProductName { get; set; }
            public string Path { get; set; }
            public float RatingStar { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Itemtext { get; set; }
            public int CategoryId { get; set; }

        }
        public class FilterCity
        {
            public int CityId { get; set; }
            public string CityName { get; set; }
            public string CityImage { get; set; }
        }
        public class CityLocation
        {
            public int CityId { get; set; }
            public string City_Latitude { get; set; }
            public string City_Longtitude { get; set; }
        }
        public class GetParentCard
        {
            public int Id { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public string CityName { get; set; }
            public List<string> Tags { get; set; }
            public float Rating { get; set; }
            public int TotalGiveComment { get; set; }
            public int TotalOrder { get; set; }
            public decimal? OriginalPrice { get; set; }
            public decimal? Price { get; set; }
            public bool Favorite { get; set; }
        }
    }

}
