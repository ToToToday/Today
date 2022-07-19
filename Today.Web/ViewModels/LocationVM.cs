using System.Collections.Generic;


namespace Today.Web.ViewModels
{
    public class LocationVM
    {
        public List<ProductPhoto> GetProductPhoto{ get; set; }
        public List<GetProduct> GetProdocutName { get; set; }
        public List<ProductLocation> GetLocation { get; set; }

        public class ProductLocation
        {
            public int Id { get; set; }
            public string longitude { get; set; }   
            public string latitude { get; set; }
            public string Title { get; set; }

        }
        public class GetProduct
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
        }
        public class ProductPhoto
        {
            public int PhotoId { get; set; }
            public int ProductId { get; set; }
            public string PhotoPath { get; set; }
            public int Sort { get; set; }
        }
        public class CityVM2
        {   
            public int CityId { get; set; }
            public string CityName { get; set; }
        }
    }

}
