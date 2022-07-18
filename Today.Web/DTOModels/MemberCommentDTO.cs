using System;
using System.Collections.Generic;

namespace Today.Web.DTOModels
{
    public class MemberCommentDTO
    {
        public class MemberCommentRequestDTO
        {
            public int MemberId { get; set; }
        }
        public class MemberCommentResponseDTO
        {
            public Order OrderInfo { get; set; }
        }
        public class Order
        {
            public List<OrderDetailCard> OrderDtailList { get; set; }
        }
        public class OrderDetailCard
        {
            public int ProductId { get; set; }
            public int OrderId { get; set; }
            public int OrderDetailId { get; set; }
            public string ProductName { get; set; }
            public string Title { get; set; }
            public string Path { get; set; }
            public int Sort { get; set; }
            public decimal UnitPrice { get; set; }
            public DateTime DepartureDate { get; set; }
            public Comment comment { get; set; }
        }
        public class Comment
        {
            public string MemberName { get; set; }
            public int RatingStar { get; set; }
            public int Parnertype { get; set; }
            public string Image { get; set; }
            public string ProductName { get; set; }
            public string CommentTitle { get; set; }
            public string CommentText { get; set; }
            public DateTime CommentDate { get; set; }
        }
    }
}
