using System.Collections.Generic;

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
        //public List<CategoryInfo> categoryList { get; set; }
        public List<ProductInfo> productList { get; set; }
        public List<CategoryInfo> cateoryList { get; set; }

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
    }

}
