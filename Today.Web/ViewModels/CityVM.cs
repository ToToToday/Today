using System;
using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.ViewModels
{
   
    public class CityVM
    {
        public int CityID { get; set; } 
        public string CityName { get; set; } 
        public List<ProductCardVM> TopActiviyList { get; set; }
        public List<ProductCardVM> AboultActiviyList { get; set; }
        public List<ProductCardVM> NewActiviyList { get; set; }
        public List<CityRaiderList> RaiderList { get; set; }
        public List<CityCommentList> commentList { get; set; }
        public List<CityCardList> cityCardList { get; set; }
        public CityInfo CurrentCityInfo { get; set; }
       
        public class CityInfo
        {
            public int Id { get; set; }
            public string CityName { get; set; }
            public string CityIntrod { get; set; }
            public string CityImage { get; set; }
        }

        public class ProductCardVM
        {
        }
        public class CityRaiderList
        {
            public int CityId { get; set; }
            public string Title { get; set; }
            public string SubTitle { get; set; }
        }

        public class CityCommentList
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

        public class CityCardList
        {
            public int Id { get; set; }
            public string CityName { get;  set; }
            public string CityImage { get; set; }
        }
    }

   
}
