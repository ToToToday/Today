using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
                OrderDtailList = DTO.OrderInfo.OrderDtailList.Select(d => new OrderDetailCard
                {
                    Path = d.Path,
                    DepartureDate = d.DepartureDate,
                    OrderId = d.OrderId,
                    ProductName=d.ProductName,
                    UnitPrice=d.UnitPrice,
                    Title=d.Title
                }).ToList()
            };
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
