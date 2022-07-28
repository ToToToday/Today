using System;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels.EcpayDTO;

namespace Today.Web.Services.EcpayService
{
    public class EcpayService : IEcpayService
    {
        private readonly IGenericRepository _repo;
        public EcpayService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public EcpayDTO UpdateStatus(string id)
        {
            //orderid 讓綠界回傳刪除亂數
            var OrderId = id.Remove(0, 6).ToString();
            var result = new EcpayDTO
            {
                IsSuccess = false,
            };
            var orderData = _repo.GetAll<Order>().Where(x => x.OrderId.ToString() == OrderId).First();
            try
            {
                orderData.Status = 2;
                orderData.StatusUpdate = 2;
                _repo.SavaChanges();
                result.IsSuccess = true;

            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
