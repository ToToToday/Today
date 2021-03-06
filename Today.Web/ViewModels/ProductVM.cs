using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.Linq;
using static Today.Web.DTOModels.ProductDTO.ProductDTO;

namespace Today.Web.ViewModels
{
    public class ProductVM
    {
        //public int CityName { get; set; }
        //public List<CategoryShow> Category { get; set; }
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

        //public class CategoryShow
        //{
        //    public string ParentCategory { get; set; }
        //    public List<string> ChildCategory { get; set; }
        //}

        public class RecentlyCardInfo
        {
            public int Id { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public decimal? Price { get; set; }
        }
        public class ProductCardInfo
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
        }
        public class City
        {
            public int Id { get; set; }
            public string CityName { get; set; }
            public string CityImage { get; set; }
        }
    }

}
