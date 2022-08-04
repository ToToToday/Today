using Today.Web.DTOModels.CreateOrderDTO;
using Today.Web.ViewModels;

namespace Today.Web.Services.OrderService
{
    public interface IOrderService
    {
        public int CreateOrder(CreateOrderRequstDTO requstDTO);
        public int directCreateOrder(ShopCartRequestVM request);
    }
}
