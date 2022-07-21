﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Today.Web.Services.CheenkoutService;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ChenkoutDTO.ChenkoutDTO;

namespace Today.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IChenkoutService _chenkoutService;
        public MemberController(IChenkoutService chenkoutService)
        {
            _chenkoutService = chenkoutService;
        }

        public IActionResult CountSetting()
        {
            return View();
        }
        public IActionResult Coupon()
        {
            return View();
        }
        public IActionResult OrderManage()
        {
            return View();
        }
        public IActionResult MessageManage()
        {
            return View();
        }
        public IActionResult MyCollect()
        {
            return View();
        }
        public IActionResult ShopCart()
        {
            return View();
        }
        public IActionResult Checkout(int id)
        {
            var orderRequet = new ChenkoutRequestDTO
            {
                OrderId = id,
            };
            var memberinfo = _chenkoutService.GetOrderMember(orderRequet);
            var orderinfo = _chenkoutService.GetOrderProduct(orderRequet);

            var orderPage = new ChenkoutVM
            {
                OrderMember = new ChenkoutVM.MemberInfo
                {
                    Name = memberinfo.Name,
                    CityName = memberinfo.CityName,
                    PhoneNumber = memberinfo.PhoneNumber,
                    Email = memberinfo.Email
                },
                OrderProduct = new ChenkoutVM.OrderInfo
                {
                    ProductName = orderinfo.ProductName,
                    ProgramTitle = orderinfo.ProgramTitle,
                    Photo = orderinfo.Photo,
                    DepartureDate = orderinfo.DepartureDate,
                    Screen = $"場次:{orderinfo.Screen}".ToString(),
                    Itemtext = orderinfo.Itemtext,
                    Quantity = orderinfo.Quantity,
                    UnitPrice = orderinfo.UnitPrice,
                    UnitText = orderinfo.UnitText,
                }
               
            };
            return View(orderPage);
        }
        

    }
}
