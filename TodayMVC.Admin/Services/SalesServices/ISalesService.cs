using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.Services.SalesServices
{
    public interface ISalesService
    {
        SalesVM GetSalesMonth();
        SalesVM GetSalesSevenDays();
    }
}
