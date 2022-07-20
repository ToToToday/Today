using System;
using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.DTOModels.CityDTO
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

        public List<City> AllCityList { get; set; }


        public class City
        {
            public int Id { get; set; }
            public string CityName { get; set; }
            public string CityIntrod { get; set; }
            public string CityImage { get; set; }
        }
        public List<RaiderCard> RaiderCarList { get; set; }

        public class RaiderCard
        {
            public int CityId { get; set; }
            public string Title { get; set; }
            public string SubTitle { get; set; }


        }
        public List<CommentCard> CommentCardList { get; set; }

        public class CommentCard
        {
            public int CityId { get; set; }
            public string Name { get; set; }
            public int RatingStar { get; set; }
            public DateTime CommentDate { get; set; }
            public DateTime UseDate { get; set; }
            public string PartnerType { get; set; }
            public string ProductName { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
        }

        public List<ProductCard> TopProductList { get; set; }
        public List<ProductCard> AboutProductList { get; set; }
        public List<ProductCard> NewProductList { get; set; }
        public class ProductCard
        {
            public int Id { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public string ChildCategoryName { get; set; }
            public int CityId { get; set; }
            public string CityName { get; set; }
            public List<string> Tags { get; set; }
            public float Rating { get; set; }
            public int TotalComment { get; set; }
            public int Quantity { get; set; }
            public decimal? OriginalPrice { get; set; }
            public decimal? Price { get; set; }
            
        }
    }
}
