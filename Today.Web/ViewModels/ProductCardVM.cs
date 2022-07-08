using System.Collections.Generic;
using System.Linq;

namespace Today.Web.ViewModels
{
    public class ProductCardVM
    {
        public string ProductPhoto { get; set; }
        public string ProductName { get; set; }
        public string CityName { get; set; }
        public List<string> Tags { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
    }
}
