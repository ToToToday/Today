using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Web.ViewModels;

namespace Today.Web.Models
{
    public class Cart
    {
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }
        private List<CartItem> cartItems;

        private List<CartQuantity> Collection = new List<CartQuantity>();
        public void AddItem(Product product, int quantity,List<CartItem> cartItems)
        {

            CartQuantity line = Collection.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null)
            {
                line = new CartQuantity();
                Collection.Add(new CartQuantity() { Product = product, Quantity = quantity});

            }
            else
            {
                line.Quantity += quantity;
            }
        }


       
    }
}
