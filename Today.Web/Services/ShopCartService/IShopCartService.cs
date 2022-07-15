using System.Collections.Generic;
using Today.Web.ViewModels;

namespace Today.Web.Services.ShopCartService
{
    public interface IShopCartService
    {
        List<ShopCartVM> GetShopCartCard();
    }
}
