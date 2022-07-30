using Microsoft.AspNetCore.Mvc;
using Today.Web.ViewModels;
//using Today.Model.Models;
//using Today.Web.Data;
using Today.Web.Services.MemberService;
using System.Linq;
using Today.Web.DTOModels.MemberDTO;
using static Today.Web.ViewModels.MemberVM;
using Today.Model.Repositories;

namespace Today.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IGenericRepository _genericRepository;

        public MemberController(IMemberService memberService, IGenericRepository genericRepository)
        {
            _memberService = memberService;
            _genericRepository = genericRepository;
        }

        //請求 
        [HttpGet]
        public IActionResult CountSetting()
        {
            //抓資料R
            var request = new MemberDTO.MemberRequestDTO()
            {
                MemberId = int.Parse(User.Identity.Name)
            };

            var citySelect = _memberService.AllCityList();
            var emailSelect = _memberService.GetMember(request);

            var memberSelectInfo = new MemberVM.MemberInfo
            {
                MemberName = emailSelect.MemberName,
                CityId = emailSelect.CityId,
                Age = emailSelect.Age,
                Phone = emailSelect.Phone,
                IdentityCard = emailSelect.IdentityCard,
                Gender = emailSelect.Gender,
                Email = emailSelect.Email,


                AllCity = citySelect.Select(c => new MemberVM.CityList
                {
                    CityId = c.CityId,
                    CityName = c.CityName,
                }).ToList()
            };

            return View(memberSelectInfo);
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
        public IActionResult Checkout()
        {
            return View();
        }


    }
}
