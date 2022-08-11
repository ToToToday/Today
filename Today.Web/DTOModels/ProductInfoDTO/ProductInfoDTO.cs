using System;
using System.Collections.Generic;

namespace Today.Web.DTOModels.ProductInfoDTO
{
    public class ProductInfoDTO
    {
        public class ProductInfoRequstDTO
        {
            public int ProductId { get; set; }
            public int MemberId { get; set; }
        }
        public class ProductInfoResponseDTO
        {
            public Product ProductInfo { get; set; }
        }
        public class Product
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public bool Favorite { get; set; }
            public string CityName { get; set; }
            public string ProductTag { get; set; }
            public List<Photo> PhtotList { get; set; }
            public List<Progarm> ProgarmList { get; set; }
            public string ProductDesc { get; set; }//商品說明
            public string ProductLocationName { get; set; }//地點名
            public string ProductLocationAddress { get; set; }//地址
            public string CancellationPolicy { get; set; }//取消政策
            public string HowUse { get; set; }//如何使用
            public string ShoppingNotice { get; set; }//購物須知
            public bool ProductIsdeleted { get; set; }//商品狀態
            public List<MemberComment> MemberComment { get; set; }//會員留言List
        }
        public class Progarm
        {
            public int ProgarmId { get; set; }
            public string PorgramName { get; set; }
            public string PrgarmText { get; set; }//產品說明
            public List<AboutProgram> AboutProgramList { get; set; }
            public List<ProgramInciude> ProgramInciudeList { get; set; }
            public List<ProgramSpecification> ProgramSpecificationList { get; set; }
            public List<Date> DateList { get; set; }
            public List<Screening> ScreeningList { get; set; }
            public bool ProgarmIsdeleted { get; set; }//商品狀態

            //progarm狀態
        }
        public class Screening//場次
        {
            public int ScreenId { get; set; }
            public string Date { get; set; }
            public int? SpecificationId { get; set; }
            public int? Status { get; set; }

        }
        public class ProgramSpecification
        {
            public int SpecificationId { get; set; }
            public string Itemtext { get; set; }//人
            public string UnitText { get; set; }//間
            public decimal PorgarmUnitPrice { get; set; }//價錢
            //規格最大購買數數量
        }
        public class Photo
        {
            public string PhotoUrl { get; set; }
            public int Sort { get; set; }
        }
        public class AboutProgram
        {
            public string IconClass { get; set; }
            public string AboutProgramName { get; set; }//產品關於項目
        }
        public class ProgramInciude
        {
            public string Inciudetext { get; set; }//方案包含項目
            public bool IsInclude { get; set; }//狀態
        }
        public class Date
        {

            public int ProgramDateId { get; set; }
            public DateTime CantuseDate { get; set; } //方案不行的日期
        }
        public class MemberComment
        {

            public int  CommentId {get; set;}
            public int MemberId { get; set; }
            public string MemberPhoto { get; set; }
            public string MemberName { get; set; }//會員名
            public string MembermMessageText { get; set; }//會員留言
            public int Star { get; set; }
            public DateTime Data { get; set; }
        }
    }
}
