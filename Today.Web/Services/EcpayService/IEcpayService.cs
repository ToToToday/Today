using Today.Web.DTOModels.EcpayDTO;


namespace Today.Web.Services.EcpayService
{
    public interface IEcpayService
    {
        EcpayDTO UpdateStatus(string id);
    }
}
