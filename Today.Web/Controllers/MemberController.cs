using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using Today.Web.Services.MemberCommentService;
using Today.Web.ViewModels;
using Today.Web.Services.CheenkoutService;
using static Today.Web.DTOModels.ChenkoutDTO.ChenkoutDTO;
using Today.Web.Services.MemberService;
using Today.Web.DTOModels.MemberDTO;
using Today.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Web.DTOModels.ShopCartDTO;
using Today.Web.Models;
using Today.Web.Services.ShopCartService;
using static Today.Web.DTOModels.ShopCartMemberDTO;
using static Today.Web.ViewModels.ShopCartVM;


namespace Today.Web.Controllers
{
    
    public class MemberController : Controller
    {
        private readonly IChenkoutService _chenkoutService;
        private readonly IMemberService _memberService;
        private readonly IGenericRepository _genericRepository;
        private readonly IMemberCommentService _membercommentservic;
        private readonly IShopCartService _shopCartService;
        public MemberController(IChenkoutService chenkoutService, IMemberService memberService, IGenericRepository genericRepository, IMemberCommentService membercommentservic,IShopCartService shopCartService)
        {
            _chenkoutService = chenkoutService;
            _memberService = memberService;
            _genericRepository = genericRepository;
            _membercommentservic = membercommentservic;
            _shopCartService = shopCartService;
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
                //IdentityCard = emailSelect.IdentityCard,
                Gender = emailSelect.Gender,
                Email = emailSelect.Email,


                AllCity = citySelect.Select(c => new MemberVM.CityList
                {
                    CityId = c.CityId,
                    CityName = c.CityName,
                }).ToList()
            };

            //ViewData["MemberName"] = memberSelectInfo.MemberName;

            return View(memberSelectInfo);
        }


        public IActionResult Coupon()
        {
            return View();
        }
        
        public IActionResult OrderManage(int ID =19 )
        {
            var DTO = _membercommentservic.ReadMemberComment(new DTOModels.MemberCommentDTO.MemberCommentRequestDTO { MemberId = ID });
            var MemberCommentInfo = new MemberCommentVM
            {
                MemberId=ID,
                MemberName = DTO.MemberName,
                OrderDtailList = DTO.OrderInfo.OrderDtailList.Select(d => new OrderDetailCard
                {
                    Path = d.Path,
                    DepartureDate = d.DepartureDate,
                    OrderId = d.OrderId,
                    ProductName=d.ProductName,
                    UnitPrice=d.UnitPrice,
                    Title=d.Title,
                    comment = new ViewModels.Comment
                    {
                        PartnerType=d.comment.Partnertype,
                        RatingStar=d.comment.RatingStar,
                        CommentTitle=d.comment.CommentTitle,
                        CommentText = d.comment.CommentText,
                        OrderDetailId = d.comment.OrderDetailId,
                        ProductId=d.comment.ProductId,
                        CommentDate=d.comment.CommentDate,
                    },
                }).ToList()
            };
            ViewData["OrderManageCard"]=JsonConvert.SerializeObject(MemberCommentInfo);
            
            return View(MemberCommentInfo);
        }
        public IActionResult MessageManage()
        {
            return View();
        }
        public IActionResult MyCollect()
        {
            return View();
        }
        [HttpGet]//請求
        public IActionResult ShopCart(int Id)
        {
            var ShopCartCardDTO = _shopCartService.GetShopCartCard(new ShopCartMemberRequestDTO { MemberId = Id });   //int.Parse(User.Identity.Name)
            var ShopCartVMs = new ShopCartVM
            {
                ShopCartCardList = ShopCartCardDTO
                .Select(s => new ShopCartCardVM
                {
                    ShoppingCartId = s.ShopCartId,
                    ProductName = s.ProductName,
                    ProgramTitle = s.ProgramTitle,
                    Path = s.ProductPhoto,
                    DepartureDate = s.DepartureDate,
                    Quantity = s.Quantity,
                    ScreenTime = s.ScreenTime,
                    UnitPrice = s.UnitPrice,
                    UnitText = s.UnitText,
                    Sum = s.UnitPrice * s.Quantity,
                }).ToList()
            };
            return View(ShopCartVMs);
        }
        
        public IActionResult Checkout(int id)
        {
            var orderRequet = new ChenkoutRequestDTO
            {
                OrderId = id,
            };
            var memberinfo = _chenkoutService.GetOrderMember(orderRequet);
            var orderinfo = _chenkoutService.GetOrderProduct(orderRequet);
            var screeninfo = _chenkoutService.GetOrderSceen(orderRequet);
            string s;
            if (screeninfo == null)
            {
                s = screeninfo == null ? "": screeninfo.ToString();
            }
            else
            {
                s = $"場次:{screeninfo.Screen}";
                //s = "場次:" + string.Format("{0:yyyy/MM/dd}", screeninfo.Screen);
            }
            var OrderProduct = orderinfo.ProductName;
            var OrderQuantity = orderinfo.Quantity;
            var OrderPrice = (int)orderinfo.UnitPrice;
            var OrderId = orderinfo.OrderId;
            TempData["OrderProduct"] = OrderProduct;
            TempData["OrderQuantity"] = OrderQuantity;
            TempData["OrderPrice"] = OrderPrice;
            TempData["OrderId"] = OrderId;
            
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
                    DepartureDate = string.Format("{0:yyyy/MM/dd}",orderinfo.DepartureDate),
                    Screen = s,
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
