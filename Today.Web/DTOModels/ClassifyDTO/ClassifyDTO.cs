using System;
using System.Collections.Generic;

namespace Today.Web.DTOModels.ClassifyDTO
{
    public class ClassifyDTO
    {
        public class ClassifyDTORequest
        {
            public int categoryId { get; set; }
            public int ProgramDateId { get; set; }
            public int Date { get; set; }
            public List<string> RealDate { get; set; }

        }

        public class ClassifyDTOinfo
        {
            //public ClassifyDTOinfo classifyDTOinfo { get; set; }
            public List<ClassifyCardInfo> ClassifyCardList { get; set; }
            public List<CategoryDestinations> CategoryList { get; set; }

            //public class ClassifyAccordion
            //{
            //    public string CategoryName { get; set; }
            //    public int? ParentCategoryId { get; set; }
            //}

        }

        public class ClassifyCardInfo
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Path { get; set; }
            public List<string> TagText { get; set; }
            public int CityId { get; set; }
            public string CityName { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Evaluation { get; set; }
            public  int ProgramId { get; set; }
            public int ProgramDateId { get; set; }
            public List<DateTime> Date { get; set; }
        }
        public class CategoryDestinations
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public List<CategoryDestinations> ChildCategory { get; set; }

        }


    }


}
