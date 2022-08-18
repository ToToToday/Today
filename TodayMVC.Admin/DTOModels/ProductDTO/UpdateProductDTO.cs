using System;
using System.Collections.Generic;

namespace TodayMVC.Admin.DTOModels.ProductDTO
{
    public class UpdateProductDTO
    {

        public class ProductInfo
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string CityName { get; set; }
            public string Illustrate { get; set; }
            public string ProductTag { get; set; }
            public string CategoryName { get; set; }

            public string CancellationPolicy { get; set; }//取消政策
            public string HowUse { get; set; }//如何使用
            public string ShoppingNotice { get; set; }//購物須知
            //public bool ProductIsdeleted { get; set; }//商品狀態

            public int ProgarmId { get; set; }
            public string PorgramName { get; set; }
            public string PrgarmText { get; set; }//產品說明
            //public bool ProgarmIsdeleted { get; set; }//商品狀態

            public int SpecificationId { get; set; }
            public string Itemtext { get; set; }//人
            public string UnitText { get; set; }//間
            public decimal PorgarmUnitPrice { get; set; }//價錢

            public string Path { get; set; }
            //public int Sort { get; set; }

            public string IconClass { get; set; }
            public string AboutProgramName { get; set; }//產品關於項目

            public string Includetext { get; set; }//方案包含項目

            public decimal UnitPrice { get; set; }

            public string Title { get; set; }

            public string Context { get; set; }
            public DateTime Date { get; set; }
            public DateTime DepartureDate { get; set; }
            //public List<Location> LocationList { get; set; }
        }
        //public class Location
        //{
        //    public string Title { get; set; }
        //    public string Addess { get; set; }
        //} 
        //public class Progarm
        //{
        //    public int ProgarmId { get; set; }
        //    public string PorgramName { get; set; }
        //    public string PrgarmText { get; set; }//產品說明
        //    public List<AboutProgram> AboutProgramList { get; set; }
        //    public List<ProgramInclude> ProgramIncludeList { get; set; }
        //    public List<ProgramSpecification> ProgramSpecificationList { get; set; }
        //    public bool ProgarmIsdeleted { get; set; }//商品狀態

        //    //progarm狀態
        //}
        
        //public class ProgramSpecification
        //{
        //    public int SpecificationId { get; set; }
        //    public string Itemtext { get; set; }//人
        //    public string UnitText { get; set; }//間
        //    public decimal PorgarmUnitPrice { get; set; }//價錢
        //    //規格最大購買數數量
        //}
        //public class Photo
        //{
        //    public string PhotoUrl { get; set; }
        //    public int Sort { get; set; }
        //}
        //public class AboutProgram
        //{
        //    public string IconClass { get; set; }
        //    public string AboutProgramName { get; set; }//產品關於項目
        //}
        //public class ProgramInclude
        //{
        //    public string Includetext { get; set; }//方案包含項目
        //    public bool IsInclude { get; set; }//狀態
        //}



    }

    
}
