using System;
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
            public int RatingStar{ get; set; }
            public DateTime CommentDate { get; set; }
            public string PartnerType { get; set; }
            public string ProductTitle { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
        }
    }
}
