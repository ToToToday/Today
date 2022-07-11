using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.ViewModels
{
    public class ClassifyVM
    {
        public string ProductName { get; set; }
        public string Path { get; set; }
        public List<string> TagText { get; set; }

        public string CityName { get; set; }
        public decimal UnitPrice { get; set; }

        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}

