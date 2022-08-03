using System;
using System.Collections.Generic;
using System.Linq;

namespace Today.Web.ViewModels
{
    public class LocationVM
    {

        public string SearchString { get; set; }
        public List<ProductLocation> ProductLocationList { get; set; }
        public class ProductLocation
        {
            public int ProductId { get; set; }
            public int LocationId { get; set; }
            public int  PhotoId { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string ProductName { get; set; }
            public string Path { get; set; }
        }

        
        public List<ClassifyCardInfo> ClassifyCardList { get; set; }
        public List<CategoryDestinations> CategoryList { get; set; }
        public class ClassifyCardInfo
        {
            public int ProductId { get; set; }
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
        public int PerPage {get; set;}

    }

}
