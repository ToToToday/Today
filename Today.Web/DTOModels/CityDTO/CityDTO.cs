using System;
using System.Collections.Generic;
using Today.Model.Models;
using static Today.Web.DTOModels.ProductDTO.ProductDTO;

namespace Today.Web.DTOModels.CityDTO
{
    public class CityDTO
    {
        public class CityRequestDTO
        {
            public int CityId { get; set; }
            public int MemberId { get; set; }
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
            public int RaiderId { get; set; }
            public string photo { get; set; }
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
            public int ProductId { get; set; }
        }

        public List<ProductInfo> TopProductList { get; set; }
        public List<ProductInfo> AboutProductList { get; set; }
        public List<ProductInfo> NewProductList { get; set; }
    }
}
