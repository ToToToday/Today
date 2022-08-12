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
using Today.Web.Services.CollectService;
using static Today.Web.ViewModels.ProductVM;

namespace Today.Web.Controllers
{
    
    public class MemberController : Controller
    {
        private readonly IChenkoutService _chenkoutService;
        private readonly IMemberService _memberService;
        private readonly IGenericRepository _genericRepository;
        private readonly IMemberCommentService _membercommentservic;
        private readonly IShopCartService _shopCartService;
        private readonly ICollectionService _collectionService;
        public MemberController(IChenkoutService chenkoutService, IMemberService memberService, IGenericRepository genericRepository, IMemberCommentService membercommentservic, IShopCartService shopCartService, ICollectionService collectionService)
        {
            _chenkoutService = chenkoutService;
            _memberService = memberService;
            _genericRepository = genericRepository;
            _membercommentservic = membercommentservic;
            _shopCartService = shopCartService;
            _collectionService = collectionService;
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
        public IActionResult OrderManage()
        {
            var DTO = _membercommentservic.ReadMemberComment(new DTOModels.MemberCommentDTO.MemberCommentRequestDTO { MemberId = int.Parse(User.Identity.Name) });
            var MemberCommentInfo = new MemberCommentVM
            {
                MemberId= int.Parse(User.Identity.Name),
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
            var request = new MemberDTO.MemberRequestDTO()
            {
                MemberId = int.Parse(User.Identity.Name)
            };
            var memberName = _memberService.GetMemberName(request);

            var memberNameInfo = new MemberVM.MemberInfo
            {
                MemberName = memberName
            };

            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;
            var dataSource = _collectionService.GetAllCollect(userId);

            var result = new ProductVM { CollectionList = new List<ProductCardInfo>() };
            result.CollectionList = dataSource.Select(d => new ProductCardInfo
            {
                Id = d.Id,
                ProductPhoto = d.ProductPhoto,
                ProductName = d.ProductName,
                CityName = d.CityName,
                Favorite = d.Favorite,
                Tags = d.Tags,
                Rating = d.Rating.RatingStar,
                TotalGiveComment = d.Rating.TotalGiveComment,
                TotalOrder = d.TotalOrder,
                OriginalPrice = (d.Prices == null || d.Prices.OriginalPrice == d.Prices.Price) ? null : d.Prices.OriginalPrice,
                Price = (d.Prices == null) ? null : d.Prices.Price
            }).ToList();

            ViewData["Collection"] = JsonConvert.SerializeObject(result.CollectionList);

            return View(memberNameInfo);
        }

        [HttpGet]//請求
        public IActionResult ShopCart()
        {
            var ShopCartCardDTO = _shopCartService.GetShopCartCard(new ShopCartMemberRequestDTO { MemberId = int.Parse(User.Identity.Name) });   //int.Parse(User.Identity.Name)
            var ShopCartVMs = new ShopCartVM
            {
                MemberId = int.Parse(User.Identity.Name),
                ShopCartCardList = ShopCartCardDTO
                .Select(s => new ShopCartCardVM
                {
                    ScreeningId = s.ScreenId,
                    SpecificationId = s.SpecificationId,
                    ShoppingCartId = s.ShopCartId,
                    ProductId = s.ProductId,
                    ProductName = s.ProductName,
                    Favorite = s.Favorite,
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
                    //}).ToList(),
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
