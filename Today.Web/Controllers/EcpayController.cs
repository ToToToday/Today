using FluentEcpay;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Today.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EcpayController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public EcpayController()
        {
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
            //var s = TempData["OrderProduct"];
            var service = new
            {
                Url = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",
                MerchantId = "2000132",
                HashKey = "5294y06JbISpM5x9",
                HashIV = "v77hoKGq4kWxNNIS",
                ServerUrl = " https://073a-2001-b400-e300-8ed3-d90c-184f-3261-feb9.jp.ngrok.io/Ecpay/callback",
                ClientUrl = "https://073a-2001-b400-e300-8ed3-d90c-184f-3261-feb9.jp.ngrok.io/Ecpay/success" //之後改主頁網址
            };
            var transaction = new
            {
                No = "test00003",
                Description = "測試購物系統",
                Date = DateTime.Now,
                Method = EPaymentMethod.Credit,
                //Items = new List<Item>{
                //    new Item{
                //        Name = "手機",
                //        Price = 14000,
                //        Quantity = 2
                //    },
                //    new Item{
                //        Name = "隨身碟",
                //        Price = 900,
                //        Quantity = 10
                //    }
                //}
                Item = new List<Item>
                {
                    new Item
                    {
                        Name = TempData["OrderProduct"].ToString(),
                        Price = (int)TempData["OrderPrice"],
                        Quantity = (int)TempData ["OrderQuantity"]
                    }
                }
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

            return Ok("1|OK");
        }
    }
}

