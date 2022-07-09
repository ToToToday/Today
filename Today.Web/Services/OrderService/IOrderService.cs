using System.Collections.Generic;
using Today.Web.ViewModels;

namespace Today.Web.Services.OrderService
{
    public interface IOrderService
    {
        List<OrderVM> GetOrderpages();
    }
}
