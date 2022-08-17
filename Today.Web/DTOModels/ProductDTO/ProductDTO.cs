using System;
using System.Collections.Generic;
using System.Linq;

namespace Today.Web.DTOModels.ProductDTO
{
    [Serializable]
    public class ProductDTO
    {
        public class ProductRequestDTO
        {
            public string SearchWord { get; set; }
        }
        public class ProductResponseDTO
        {
            public bool HasCityId { get; set; }
            public int Id { get; set; }
        }
        #region 所需資料欄位
        public IQueryable<ProductInfo> QueryProduct { get; set; }
        public List<CategoryInfo> CategoryList { get; set; }
        public List<ProductInfo> ProductList { get; set; }
        #endregion

        #region 首頁所需欄位
        public List<CityInfo> CityList { get; set; }

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
        public IQueryable<int> FavoriteList { get; set; }
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
            public int CityId { get; set; }
            public string CityName { get; set; }
            public bool Favorite { get; set; }
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
            public bool Favorite { get; set; }
            public decimal? Price { get; set; }
        }

        public class CityInfo
        {
            public int Id { get; set; }
            public string CityName { get; set; }
            public string CityImage { get; set; }
        }
    }

}
