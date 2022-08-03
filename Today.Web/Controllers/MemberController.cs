using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using Today.Web.DTOModels;
using Today.Web.Services.MemberCommentService;
using Today.Web.ViewModels;
using Today.Web.Services.CheenkoutService;
using static Today.Web.DTOModels.ChenkoutDTO.ChenkoutDTO;
using Today.Web.Services.MemberService;
using Today.Web.DTOModels.MemberDTO;
using static Today.Web.ViewModels.MemberVM;
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
        
        public IActionResult OrderManage(int ID =1 )
        {
            var DTO = _membercommentservic.ReadMemberComment(new DTOModels.MemberCommentDTO.MemberCommentRequestDTO { MemberId = ID });
            var MemberCommentInfo = new MemberCommentVM
            {
                MemberId=ID,
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
                MemberId = Id,
                ShopCartCardList = ShopCartCardDTO
                .Select(s => new ShopCartCardVM
                {
                    ScreeningId = s.ScreenId,
                    SpecificationId = s.SpecificationId,
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
            
            List<string> s = new List<string>();
            foreach (var sc in screeninfo)
            {
                if (screeninfo == null)
                {
                    s.Add(screeninfo == null ? "" : screeninfo.ToString());
                }
                else
                {
                    
                     s.Add($"場次:{sc.Screen}");
                    
                }
            }
            List<string> OrderProduct  = new List<string>();
            List<int> OrderQuantity = new List<int>();
            List<int> OrderPrice = new List<int>();
            

            foreach (var p in orderinfo)
            {
                OrderProduct.Add(p.ProductName);
                OrderQuantity.Add(p.Quantity);
                OrderPrice.Add((int)p.UnitPrice);
              

            }
            
            var OrderId = id;
            TempData["OrderProduct"] = OrderProduct.ToList();
            TempData["OrderQuantity"] = OrderQuantity.ToList();
            TempData["OrderPrice"] = OrderPrice.ToList();
            TempData["OrderId"] = OrderId;

            var orderPage = new ChenkoutVM
            {
                OrderMember = memberinfo.Select(m => new ChenkoutVM.MemberInfo
                {
                    Name = m.Name,
                    CityName = m.CityName,
                    PhoneNumber = m.PhoneNumber,
                    Email = m.Email,
                }).First(),
                
                OrderProduct = orderinfo.Select(oi => new ChenkoutVM.OrderInfo
                {
                    ProductName = oi.ProductName,
                    ProgramTitle = oi.ProgramTitle,
                    Photo = oi.Photo,
                    DepartureDate = String.Format("{0:yyyy/MM/dd}", oi.DepartureDate),
                    //SceenLists = s.Select(sn => new ChenkoutVM.OrderInfo.SceenList
                    //{
                    //    Screen = sn
                    //}),
                    Itemtext = oi.Itemtext,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    UnitText = oi.UnitText,
                }).ToList()


            };
            return View(orderPage);
        }


    }
}
