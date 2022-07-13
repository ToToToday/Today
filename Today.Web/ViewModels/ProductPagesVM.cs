﻿using System;
using System.Collections.Generic;
namespace Today.Web.ViewModels
{
    public class ProductPagesVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CityName { get; set; }
        public string Producttag { get; set; }
        public List<Photo> PhtotList { get; set; }
        public List<Progarm> progarmList { get; set; }
        public string ProductText { get; set; }//商品說明
        public string ProductlocationName { get; set; }//地點名
        public string ProductLocationAddress { get; set; }//地址
        public string CancellationPolicy { get; set; }//取消政策
        public string HowUse { get; set; }//如何使用
        public string ShoppingNotice { get; set; }//購物須知

        public class Progarm
        {
            public string porgramname { get; set; }
            public string PrgarmText { get; set; }//產品說明
            public List<AboutProgram> AboutProgramList { get; set; }
            public List<ProgramInciude> programInciudeList { get; set; }
            public List<ProgramSpecification> ProgramSpecificationList { get; set; }
            public List<Date> DateList { get; set; }
        }
        public class ProgramSpecification
        {
            public string Itemtext { get; set; }//人
            public string UnitText { get; set; }//間
            public decimal PorgarmUnitPrice { get; set; }//價錢
        }
        public class Photo
        {
            public string PhotoUrl { get; set; }
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
            public DateTime CantuseDate { get; set; } //方案不行的日期
        }
        public class Member
        {
            public string MemberPhoto { get; set; }
            public string MemberName { get; set; }//會員名
            public string MembermMessageText { get; set; }//會員留言
        }
    }
}
