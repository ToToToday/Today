using System;
using System.Collections.Generic;
using System.Linq;

namespace Today.Web.DTOModels.locationDTO
{
    public class LocationDTO
    {   
        public class CategoryInfoRequestDTO
        {
            public int CatergoryId { get; set; }
            
        }
        public class CategoryInfoResponseDTO
        {
            public ClassifyCardInfo CategoryInfo { get; set; }
        }
        public List<ProductLocationDTO> ProductLocationList { get; set; }

        public class ProductLocationDTO
        {
            public int ProductId { get; set; }
            public int LocationId { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string ProductName { get; set; }
            public int CityId { get; set; }
            public int PhotoId { get; set; }
            public string Path { get; set; }
            public int Sort { get; set; }
        }

        public List<ClassifyCardInfo> ClassifyCardList { get; set; }
        public List<CategoryDestinations> CategoryList { get; set; }
        public class ClassifyCardInfo
        {
            public string ProductName { get; set; }
            public string Path { get; set; }
            public List<string> TagText { get; set; }
            public string CityName { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Evaluation { get; set; }
        }
        public class CategoryDestinations
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public List<CategoryDestinations> ChildCategory { get; set; }
        }
    }
}
