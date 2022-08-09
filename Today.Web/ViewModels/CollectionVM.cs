using System;

namespace Today.Web.ViewModels
{
    public class CollectionVM
    {
        public int MemberId { get;set; }
        public int ProductId { get; set; }
        public bool Favorite { get; set; }
        public DateTime Time { get; set; }
    }
}
