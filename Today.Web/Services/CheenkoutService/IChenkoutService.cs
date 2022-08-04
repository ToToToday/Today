using System.Collections.Generic;
using static Today.Web.DTOModels.ChenkoutDTO.ChenkoutDTO;

namespace Today.Web.Services.CheenkoutService
{
    public interface IChenkoutService
    {
        //MemberInfo GetOrderMember(ChenkoutRequestDTO request);
        List<MemberInfo> GetOrderMember(ChenkoutRequestDTO request);
        //OrderInfo GetOrderProduct(ChenkoutRequestDTO request);
        List<OrderInfo> GetOrderProduct(ChenkoutRequestDTO request);
        //OrderScreen GetOrderSceen(ChenkoutRequestDTO request);
        List<OrderScreen> GetOrderSceen(ChenkoutRequestDTO request);
    }
}
