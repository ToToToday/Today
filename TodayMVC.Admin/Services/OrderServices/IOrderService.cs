using System.Collections.Generic;
using static TodayMVC.Admin.DTOModels.OrderDTO.OrderDTO;

namespace TodayMVC.Admin.Services.OrderServices
{
    public interface IOrderService
    {
        List<OrderInfo> GetAllOrder();
    }
}
