using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Web.DTOModels.ClassifyDTO;

namespace Today.Web.ViewModels
{
    public class ClassifyVM
    {
        public FilterVM AllFilters { get; set; }
        public List<ClassifyCardInfo> ClassifyCardList { get; set; }
        public int CardCount { get; set; }
    }

    public class FilterVM
    {
        public List<CategoryFilter> CategoryFilterList { get; set; }
        public List<CityFilter> CityFilterList { get; set; }

        //可以擴充 其他篩選器

        public class CategoryFilter
        {
            public int ProductCategoryId { get; set; }
            public string CategoryName { get; set; }
            public bool Checked { get; set; }
            public List<CategoryFilter> ChildCategory { get; set; }
        }
        public class CityFilter
        {
            public int CityId { get; set; }
            public string CityName { get; set; }
            public bool Checked { get; set; }
        }
    }
    public class ClassifyCardInfo
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public bool Favorite { get; set; }
        public string Path { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public List<string> TagText { get; set; }
        public double RatingStar { get; set; }
        public int TotalComment { get; set; }
        public int TotalOrder { get; set; }

        public PriceInfo Prices { get; set; }
        public class PriceInfo
        {
            public decimal? OriginalPrice { get; set; }
            public decimal? Price { get; set; }
        }
    }
    public class ClassifyRequestModel
    {
        //篩 //擴充其他...日期
        public List<int> Cities { get; set; }
        public List<int> Categories { get; set; }
        
        //排
        public SortCondition SortBy { get; set; }
        //切
        public int Page { get; set; }
    }
}

