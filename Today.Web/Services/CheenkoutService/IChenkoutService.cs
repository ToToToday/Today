using System.Collections.Generic;
using static Today.Web.DTOModels.ChenkoutDTO.ChenkoutDTO;

namespace Today.Web.Services.CheenkoutService
{
    public interface IChenkoutService
    {
       MemberInfo GetOrderMember(ChenkoutRequestDTO request);
       OrderInfo GetOrderProduct(ChenkoutRequestDTO request);
    }
}
