using System;

namespace Today.Web.ViewModels
{
    public class ProductPagesVM
    {
        public int productId { get; set; }
        public string productname { get; set; }
        public string producttag { get; set; }
        public string cityname { get; set; }
        public string potourl { get; set; }
        public string porgramname { get; set; }
        public decimal porgarmunitprice { get; set; }
        public string porgarmtext { get; set; }//產品說明
        public string aboutprogram { get; set; }//產品關於項目
        public string programinciudetrue { get; set; }//方案包含項目
        public string programinciudefalse { get; set; }//方不案包含項目
        public string producttext { get; set; }//商品說明
        public string productlocationname { get; set; }//地點名
        public string productlocationaddress { get; set; }//地址
        public string membername { get; set; }//會員名
        public string membermmessagetext { get; set; }//會員留言
        public string membermessagePoto { get; set; }//會員留言圖片
        public DateTime cantusedate { get; set; } //方案不行的日期
    }
}
