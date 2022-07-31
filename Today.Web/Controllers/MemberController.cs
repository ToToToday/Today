using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using Today.Web.DTOModels;
using Today.Web.Services.MemberCommentService;
using Today.Web.ViewModels;


namespace Today.Web.Controllers
{
    
    public class MemberController : Controller
    {
        private readonly IMemberCommentService _membercommentservic;
        public MemberController(IMemberCommentService membercommentservic)
        {
            _membercommentservic = membercommentservic;
        }
        public IActionResult CountSetting()
        {
            return View();
        }
        public IActionResult Coupon()
        {
            return View();
        }
        
        public IActionResult OrderManage(int ID =3 )
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
                        //
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
