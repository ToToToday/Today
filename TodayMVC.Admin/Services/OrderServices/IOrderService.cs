using System.Collections.Generic;
using static TodayMVC.Admin.ViewModels.OrderVM;

namespace TodayMVC.Admin.Services.OrderServices
{
    public interface IOrderService
    {
        List<OrderInfo> OrderList();
    }
}
