using System;
using System.Collections.Generic;

namespace Today.Web.DTOModels.OrderDTO
{
    public class OrderDTO
    {
        public class OrderInfo
        {
            public int OrderId { get; set; }//訂單ID
            public int MemberId { get; set; }//會員ID
            public DateTime OrderDate { get; set; }//下單日期
            public int PaymentId { get; set; }//付款ID
            public int Status { get; set; }//狀態
            public int StatusUpDate { get; set; }//訂單狀態更新
        }
        public class OrderDetailInfo
        {
            public int  OrderDetailId { get;  set; }
            public int OrderId { get; set; }
            public int SpeciflcationId { get; set; }//規格
            public int Quantity { get; set; }//數量
            public Decimal? Discount { get;  set; }//折扣
            public string Itemtext { get; set; }//票種、房型
            public DateTime? LeaseTime { get; set; }//租時間
            public int? TicketsId { get; set; }//電子憑證ID
            public Decimal UnitPrice { get; set; }//價格
            public DateTime DepartureDate { get; set; }//出發日期
        }

    }
}
