using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.Linq;
using static Today.Web.DTOModels.ProductDTO.ProductDTO;

namespace Today.Web.ViewModels
{
    public class ProductVM
    {
        //public List<CategoryShow> Category { get; set; }

        #region 首頁
        public List<City> PopularCity { get; set; }
        public List<RecentlyCardInfo> RecentlyViewed { get; set; }
        public List<ProductCardInfo> TopProduct { get; set; }
        public List<ProductCardInfo> Featured { get; set; }
        public List<ProductCardInfo> Paradise { get; set; }
        public List<ProductCardInfo> AttractionTickets {get; set; }
        public List<ProductCardInfo> Exhibition { get; set; }
        public List<ProductCardInfo> Hotel { get; set; }
        public List<ProductCardInfo> Taoyuan { get; set; }
        public List<ProductCardInfo> TimeLimit { get; set; }
        public List<ProductCardInfo> Evaluation { get; set; }
        #endregion

        #region Collection
        public List<ProductCardInfo> CollectionList { get; set; }
        #endregion

        //public class CategoryShow
        //{
        //    public string ParentCategory { get; set; }
        //    public List<string> ChildCategory { get; set; }
        //}

        #region 卡牌所需內容
        public class RecentlyCardInfo
        {
            public int Id { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public bool? Favorite { get; set; }

            public decimal? Price { get; set; }
        }
        public class ProductCardInfo
        {
            public int Id { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public string CityName { get; set; }
            public bool? Favorite { get; set; }
            public List<string> Tags { get; set; }
            public double Rating { get; set; }
            public int TotalGiveComment { get; set; }
            public int TotalOrder { get; set; }
            public decimal? OriginalPrice { get; set; }
            public decimal? Price { get; set; }
        }
        public class City
        {
            public int Id { get; set; }
            public string CityName { get; set; }
            public string CityImage { get; set; }
        }
        #endregion
    }

}
