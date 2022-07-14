using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;

namespace Today.Web.Models
{
    public class Cart
    {
        //private List<Product> cartItem;
        private List<CartQuantity> Collection = new List<CartQuantity>();
        public void AddItem(Product product, int quantity)
        {
            CartQuantity line = Collection.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null)
            {
                Collection.Add(new CartQuantity() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }


       
    }
}
