using System;
using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.ViewModels
{
   
    public class CityVM : LocationVM
    {
        public int CityID { get; set; } 
        public string CityName { get; set; } 
        public List<ProductCardVM> TopActiviyList { get; set; }
        public List<ProductCardVM> AboutActiviyList { get; set; }
        public List<ProductCardVM> NewActiviyList { get; set; }
        public List<CityRaiderList> RaiderList { get; set; }
        public List<CityCommentList> CommentList { get; set; }
        public List<CityCardList> CityCardsList { get; set; }
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
            public int Id { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public bool Favorite { get; set; }
            public string CityName { get; set; }
            public List<string> Tags { get; set; }
            public double Rating { get; set; }
            public int TotalGiveComment { get; set; }
            public int TotalOrder { get; set; }
            public decimal? OriginalPrice { get; set; }
            public decimal? Price { get; set; }
        }
        public class CityRaiderList
        {
            public int RaiderId { get; set; }
            public string photo { get; set; }
            public int CityId { get; set; }
            public string Title { get; set; }
            public string SubTitle { get; set; }
        }

        public class CityCommentList
        {
            public int CityId { get; set; }
            public string Name { get; set; }
            public int RatingStar { get; set; }
            public string CommentDate { get; set; }
            public string UseDate { get; set; }
            public string PartnerType { get; set; }
            public string ProductName { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public int ProductId { get; set; }
        }

        public class CityCardList
        {
            public int Id { get; set; }
            public string CityName { get;  set; }
            public string CityImage { get; set; }
        }
    }

   
}
