 using Ecpay;
using Ecpay.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ProjectServer.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using Today.Web.Services.EcpayService;
using static Today.Web.DTOModels.EcpayDTO.EcpayDTO;

namespace Today.Web.WebApiControllers
{
    [Route("[controller]")]
    [ApiController]
    public class EcpayController : Controller
    {
        private readonly IEcpayService _ecpayService;
        public EcpayController(IEcpayService ecpayService)
        {
            _ecpayService = ecpayService;
        }

        // POST api/payment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New()
        {
            return RedirectToAction("checkout");
        }

        [HttpGet("checkout")]
        public IActionResult CheckOut()
        {
           
            var id = (int)TempData["OrderId"];
            var service = new
            {
                Url = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",
                MerchantId = "2000132",
                HashKey = "5294y06JbISpM5x9",
                HashIV = "v77hoKGq4kWxNNIS",
                ServerUrl = "https://today-frontend.azurewebsites.net/Ecpay/callback",
                ClientUrl = "https://today-frontend.azurewebsites.net/Home/Index" //之後改主頁網址
            };
            var pName = TempData["OrderProduct"];
            var pPrice = TempData["OrderPrice"];
            var pQuantity = TempData["OrderQuantity"];

            var itemList = new List<Item>();
            IList pNameList = (IList)pName;
            IList pPriceList = (IList)pPrice;
            IList pQuantityList = (IList)pQuantity;
            for (int i = 0; i < pNameList.Count; i++)
            {
                itemList.Add(
                new Item
                {
                    Name = pNameList[i].ToString(),
                    Price = (int)pPriceList[i],
                    Quantity = (int)pQuantityList[i]
                });
            }
                


            var transaction = new
            {
                No = id.ToString(),//"test00003"
                Description = "Today購物系統",
                Date = DateTime.UtcNow.AddHours(8), //DateTime.Now
                Method = EPaymentMethod.Credit,

                Item = itemList,
                
            };
            IPayment payment = new PaymentConfiguration()
                .Send.ToApi(
                    url: service.Url)
                .Send.ToMerchant(
                    service.MerchantId)
                .Send.UsingHash(
                    key: service.HashKey,
                    iv: service.HashIV)
                .Return.ToServer(
                    url: service.ServerUrl)
                .Return.ToClient(
                    url: service.ClientUrl)
                .Transaction.New(
                    no: transaction.No,
                    description: transaction.Description,
                    date: transaction.Date)
                .Transaction.UseMethod(
                    method: transaction.Method)
                .Transaction.WithItems(
                    items: transaction.Item)
                .Generate();

            return View(payment);
        }

        [HttpPost("callback")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult Callback([FromForm] PaymentResult result)
        {

            HttpContext.Request.Headers.ContainsKey("Content-Type");

            var hashKey = "5294y06JbISpM5x9";
            var hashIV = "v77hoKGq4kWxNNIS";

            // 務必判斷檢查碼是否正確。
            if (!CheckMac.PaymentResultIsValid(result, hashKey, hashIV)) return BadRequest();

            // 處理後續訂單狀態的更動等等...。

            var updateOrder = _ecpayService.UpdateStatus(result.MerchantTradeNo);
            if (updateOrder.IsSuccess != true)
            {

            }
            return Ok("1|OK");
        }
    }
}

