using Today.Web.DTOModels.CreateOrderDTO;

namespace Today.Web.Services.OrderService
{
    public interface IOrderService
    {
        public void CreateOrder(CreateOrderRequstDTO requstDTO);
    }
}
