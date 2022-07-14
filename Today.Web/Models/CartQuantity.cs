using Today.Model.Models;

namespace Today.Web.Models
{
    public class CartQuantity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
