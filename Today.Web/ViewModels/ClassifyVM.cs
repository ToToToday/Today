using System;
using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.ViewModels
{
    public class ClassifyVM
    {
        public List<ClassifyCardInfo> ClassifyCardList { get; set; }
        public List<CategoryDestinations> CategoryList { get; set; }
        public class ClassifyCardInfo
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Path { get; set; }
            public List<string> TagText { get; set; }
            public string CityName { get; set; }
            public int CityId { get; set; }
            public decimal UnitPrice { get; set; }
            public int RatingStar { get; set; }
            public int TotalComment { get; set; }
            public decimal Evaluation { get; set; }
        }

        public class CategoryDestinations
        {
            public int ProductCategoryId { get; set; }
            public string CategoryName { get; set; }
            public List<CategoryDestinations> ChildCategory { get; set; }

        }
    }
}

