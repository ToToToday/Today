using System;
using System.Collections.Generic;

namespace Today.Web.ViewModels
{
    public class MemberCommentVM
    {

        public List<OrderDetailCard> OrderDtailList { get; set; }

    }
    public class OrderDetailCard
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
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
