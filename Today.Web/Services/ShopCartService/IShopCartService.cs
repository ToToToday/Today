using System.Collections.Generic;
using Today.Web.DTOModels;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ShopCartMemberDTO;

namespace Today.Web.Services.ShopCartService
{
    public interface IShopCartService
    {
        ShopCartMemberResponseDTO GetShopCartCard(ShopCartMemberRequestDTO Id);


    }
}
