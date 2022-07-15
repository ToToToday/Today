using System.Collections.Generic;
using Today.Model.Models;
using Today.Web.ViewModels;

namespace Today.Web.Models
{
    public class CartItem
    {
        public int ItemId { get; set; }
        public int CartId { get; set; }
        public string Name { get; set; }


    }
}
