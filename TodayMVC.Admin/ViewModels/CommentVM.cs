using System.Collections.Generic;
using Today.Model.Models;

namespace TodayMVC.Admin.ViewModels
{
    public class CommentVM
    {
        public Product P { get; set; }
        public Location L { get; set; }
        public ProductPhoto PP { get; set; }
        public Comment C { get; set; }
        public Member M { get; set; }
    }
}
