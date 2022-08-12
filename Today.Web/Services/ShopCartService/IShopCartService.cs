using System.Collections.Generic;
using System.Threading.Tasks;
using Today.Web.DTOModels;
using Today.Web.DTOModels.ShopCartDTO;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ShopCartMemberDTO;
using static Today.Web.DTOModels.ShopCartMemberDTO.ShopCartMemberResponseDTO;

namespace Today.Web.Services.ShopCartService
{
    public interface IShopCartService
    {
        List<ShopCartCard> GetShopCartCard(ShopCartMemberRequestDTO Id);
        public void CreateShopCard(ShopCartRequestVM input);
        public void DeleteShopCard(DeleteCardVM input);

        
    }
}
