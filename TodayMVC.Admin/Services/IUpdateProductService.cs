using System.Collections.Generic;
using TodayMVC.Admin.ViewModels;
using static TodayMVC.Admin.ViewModels.UpdateProductVM;

namespace TodayMVC.Admin.Services
{
    public interface IUpdateProductService
    {
        public int UpdateProduct(UpdateProductVM updateProduct);
    }
}
