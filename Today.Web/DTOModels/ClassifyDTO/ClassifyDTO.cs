using System;
using System.Collections.Generic;
using Today.Web.ViewModels;

namespace Today.Web.DTOModels.ClassifyDTO
{
    public class ClassifyDTO
    {
        public List<ClassifyCardInfo> ClassifyCardList { get; set; }
        public int CardCount { get; set; }


        //public List<CategoryDestinations> CategoryList { get; set; }
        //public class CategoryDestinations
        //{
        //    public int ProductCategoryId { get; set; }
        //    public string CategoryName { get; set; }
        //    public List<CategoryDestinations> ChildCategory { get; set; }
        //}

        public class ClassifyRequestDTO
        {
            public int CategoryId { get; set; }
            public int Page { get; set; }
            public int MemberId { get; set; }
        }
        public class RatingInfo
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public bool Favorite { get; set; }
            public string Path { get; set; }
            public int CityId { get; set; }
            public string CityName { get; set; }
            public List<string> TagText { get; set; }
            public decimal UnitPrice { get; set; }
            public double RatingStar { get; set; }
            public int TotalComment { get; set; }
        }
    }



    public class FilterDTO
    {
        public List<int> CategoryFilterList { get; set; }
        public List<int> CityFilterList { get; set; }
        public int Page { get; set; }
        public int MemberId { get; set; }
    }

    public class GetAllFiltersOutputDTO
    {
        public List<FilterVM.CategoryFilter> CategoryFilterList { get; set; }
        public List<FilterVM.CityFilter> CityFilterList { get; set; }
    }
}
