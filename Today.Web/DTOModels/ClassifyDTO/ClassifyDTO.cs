using System;
using System.Collections.Generic;

namespace Today.Web.DTOModels.ClassifyDTO
{
    public class ClassifyDTO
    {
        public List<ClassifyCardInfo> ClassifyCardList { get; set; }
        public int CardCount { get; set; }
        public List<CategoryDestinations> CategoryList { get; set; }

        public List<DateCantbeUse> DateCantbeUseList { get; set; }

                
        public class ClassifyRequestDTO
        {
            public int CategoryId { get; set; }
            //public int CityId { get; set; }
            public int Page { get; set; }
            public List<string> RealDate { get; set; }
        }

        public class ClassifyCardInfo
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public bool IsIsland { get; set; }
            public string Path { get; set; }
            public int CityId { get; set; }
            public string CityName { get; set; }
            public List<string> TagText { get; set; }
            public decimal UnitPrice { get; set; }
            public float RatingStar { get; set; }
            public int TotalComment { get; set; }
            public decimal Evaluation { get; set; }
        }
        public class CategoryDestinations
        {
            public int ProductCategoryId { get; set; }
            public string CategoryName { get; set; }
            public List<CategoryDestinations> ChildCategory { get; set; }
        }
        public class DateCantbeUse
        {
            public int ProgramId { get; set; }
            public int ProgramDateId { get; set; }
 
        }
    }
    public class FilterDTO
    {
        //public List<int> MinPrice { get; set; }
        //public List<int> MaxPrice { get; set; }


        public List<int> CategoryFilterList { get; set; }
        public List<int> CityFilterList { get; set; }
        public int Page { get; set; }



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
