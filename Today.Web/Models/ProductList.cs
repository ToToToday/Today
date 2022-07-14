using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.Models
{
    public class ProductList
    {
        public ProductList()
        {
            this.Products = new List<Product>();
        }
        public IEnumerable<Product> Products { get; set; }
    }
}
