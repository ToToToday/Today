using System.Collections.Generic;

namespace Today.Web.DTO
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
        public List<ProductInfo> productList { get; set; }

        public class ProductInfo
        {
            public int Id { get; set; }
            public string ProductPhoto { get; set; }
            public string ProductName { get; set; }
            public string CityName { get; set; }
            public List<string> Tags { get; set; }
            public decimal OriginalPrice { get; set; }
            public decimal Price { get; set; }
        }
    }
}
