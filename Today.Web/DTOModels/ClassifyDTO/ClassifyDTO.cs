using System;
using System.Collections.Generic;
using System.Linq;

namespace Today.Web.DTOModels.ClassifyDTO
{
    public class ClassifyDTO
    {
        public List<ViewModels.ClassifyVM.ClassifyCardInfo> ClassifyCardList { get; set; }
        public int CardCount { get; set; }
        public List<CategoryDestinations> CategoryList { get; set; }
        public class ClassifyRequestDTO
        {
            public int CategoryId { get; set; }
            public int Page { get; set; }
        }



        public class RatingInfo
        {
            public float RatingStar { get; set; }
            public int TotalComment { get; set; }
        }
        public class CategoryDestinations
        {
            public int ProductCategoryId { get; set; }
            public string CategoryName { get; set; }
            public List<CategoryDestinations> ChildCategory { get; set; }

        }
    }
    public class FilterDTO
    {
        public List<int> CategoryFilterList { get; set; }
        public List<int> CityFilterList { get; set; }
        public int Page { get; set; }
    }
    //public class CityinputRequestDTO
    //{
    //    public string SearchWord { get; set; }
    //}
    //public class CityinputResponseDTO
    //{
    //    public bool HasCityId { get; set; }
    //    public int Id { get; set; }
    //}


}
