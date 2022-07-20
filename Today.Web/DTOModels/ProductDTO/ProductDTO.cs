﻿using System.Collections.Generic;
using System.Linq;

namespace Today.Web.DTOModels.ProductDTO
{
    public class ProductDTO
    {
        //public class ProductRequestDTO
        //{
        //    public int ProductId { get; set; }
        //}
        //public class ProductResponseDTO
        //{
        //    public ProductInfo ProductInfo { get; set; }
        //}
        public List<ProductInfo> ProductList { get; set; }
        public List<CategoryInfo> CategoryList { get; set; }

        #region 測試
        //public List<City> RecommendedCity { get; set; }
        public List<RecentlyInfo> RecentlyViewed { get; set; }
        public List<ProductInfo> TopProduct { get; set; }
        public List<ProductInfo> Featured { get; set; }
        public List<ProductInfo> Paradise { get; set; }
        public List<ProductInfo> AttractionTickets { get; set; }
        public List<ProductInfo> Exhibition { get; set; }
        public List<ProductInfo> Hotel { get; set; }
        public List<ProductInfo> Taoyuan { get; set; }
        public List<ProductInfo> TimeLimit { get; set; }
        public List<ProductInfo> Evaluation { get; set; }
        #endregion

        public class CategoryInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<CategoryInfo> ChildCategoryList { get; set; }
        }
        public class ProductInfo
        {
            public int Id { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public string ChildCategoryName { get; set; }
            public string CityName { get; set; }
            public List<string> Tags { get; set; }
            public RatingInfo Rating { get; set; }
            public int TotalOrder { get; set; }
            public PriceInfo Prices { get; set; }

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

        public class RecentlyInfo
        {
            public int Id { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public decimal? Price { get; set; }
        }
    }

}
