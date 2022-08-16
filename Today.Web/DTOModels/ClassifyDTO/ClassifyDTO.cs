using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;

namespace Today.Web.DTOModels.ClassifyDTO
{
    public class ClassifyDTO
    {
        public List<ViewModels.ClassifyVM.ClassifyCardInfo> ClassifyCardList { get; set; }
        public int CardCount { get; set; }
        public List<CategoryDestinations> CategoryList { get; set; }

        public List<ProgramCantUseDate> DateCantbeUseList { get; set; }

        public class ClassifyRequestDTO
        {
            public int CategoryId { get; set; }
            public int Page { get; set; }
            public int MemberId { get; set; }
            public List<string> RealDate { get; internal set; }
            public bool isOffIsland { get; set; }
        }



        public class RatingInfo
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public bool Favorite { get; set; }
            public bool IsIsland { get; set; }
            public string Path { get; set; }
            public int CityId { get; set; }
            public string CityName { get; set; }
            public List<string> TagText { get; set; }
            public decimal UnitPrice { get; set; }
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
        public List<string> DateRange { get; set; }

        public int Page { get; set; }
        public int MemberId { get; set; }
        public bool isOffIsland { get; set; }


        //public List<CategoryFilter> CategoryFilterList { get; set; }
        //public List<CityFilter> CityFilterList { get; set; }

        //public class CategoryFilter
        //{
        //    public int CategoryId { get; set; }
        //}
        //public class CityFilter
        //{
        //    public int CityId { get; set; }
        //}
    }
}
