using System.Collections.Generic;

namespace TodayMVC.Admin.DTOModels.ProductDTO
{
    public class CreateProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<int> TagList { get; set; }//標籤s
        //public string? Producttag { get; set; }
        public List<int> CategoryList { get; set; } //類別s 只能選取已有的 多對多
        public string ProductText { get; set; }//商品說明
        public string CancellationPolicy { get; set; }//取消政策
        public string HowUse { get; set; }//如何使用
        public string ShoppingNotice { get; set; }//購物須知
        public int City { get; set; }
        //public string CityName { get; set; }
        public location location { get; set; }
        public Supplier Supplier { get; set; }
        public bool ProductIsdeleted { get; set; }//商品狀態
        public bool Favorite { get; set; }
        public bool Isdeleted { get; set; }//狀態
        public List<string> PhtotList { get; set; }
        public List<AboutProgramOption> AboutProgramOptionList { get; set;}
        public List<Progarm> ProgarmList { get; set; }
    }
    public class Supplier
    {
        public string CompanyName { get; set; }//公司名稱
        public string ContactName { get; set; }//負責人名稱
        public string ContactTitle { get; set; }//負責人職稱
        public string Address { get; set; }//公司地址
        public string Phon { get; set; }//負責人電話
        public int City { get; set; }//城市
    }
    public class location
    {
        public string locationTitle { get; set; }//地點名
        public string locationtext { get; set; }//地址
        public string locationAddress { get; set; } //地點
        public string Longitude { get; set; }//經度
        public string Latitude { get; set; } //緯度
        public int? Type { get; set; }
    }
    public class Progarm
    {
        public bool ProgarmIsdeleted { get; set; }//商品狀態
        public string PrgramName { get; set; }
        public string PrgarmText { get; set; }//產品說明
        //public List<int> AboutProgramList { get; set; }
        public List<ProgramInclude>? ProgramIncludeList { get; set; }
        public List<ProgramSpecification>? ProgramSpecificationList { get; set; }
        public List<string>? DateList { get; set; }

        public List<Screening>? ScreeningList { get; set; }
    }
    public class AboutProgramOption
    {

        public string Context { get; set; }
        public string IconClass { get; set; }

    }
    public class Screening
    {
        public string Date { get; set; }
        public int? SpecificationId { get; set; }
        public int? Status { get; set; }
    }
    public class ProgramSpecification
    {
        public bool IsScreening { get; set; }//有無場次
        public string Itemtext { get; set; }//人
        public string UnitText { get; set; }//間
        public decimal PorgarmUnitPrice { get; set; }//單錢
        public decimal OriginalUnitPrice { get; set; }//原價
        //public int Status { get; set; }
    }
    public class Photo
    {
        public string PhotoUrl { get; set; }
        //public int Sort { get; set; } //排序
    }
    //public class AboutProgram
    //{
    //    public List<int> AboutProgramId { get; set; }
    //    //public string AboutProgramName { get; set; }//產品關於項目
    //}
    public class ProgramInclude
    {
        public string Inciudetext { get; set; }//方案包含項目
        public bool IsInclude { get; set; }//狀態
    }
    //public class Date
    //{
    //    //public int ProgramDateId { get; set; }
    //    //public int ProgramID { get; set; }
    //    public string CantuseDate { get; set; } //方案不行的日期
    //}
}
