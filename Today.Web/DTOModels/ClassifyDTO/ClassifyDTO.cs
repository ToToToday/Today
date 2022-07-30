using System.Collections.Generic;

namespace Today.Web.DTOModels.ClassifyDTO
{
    public class ClassifyDTO
    {
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

        //public class ClassifyAccordion
        //{
        //    public string CategoryName { get; set; }
        //    public int? ParentCategoryId { get; set; }
        //}
        public class CategoryDestinations
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public List<CategoryDestinations> ChildCategory { get; set; }

        }

    }

}
