using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Web.Services;
using Today.Web.Services.CityService;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.CityDTO;
using static Today.Web.DTOModels.RaiderDTO;

namespace Today.Web.Controllers
{
    public class ProductController : Controller
    {
       
        private readonly ICityService _cityServices;
        public ProductController(ICityService cityServices)
        {
            _cityServices = cityServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ProductPages() //商品頁面
        {
            return View();
        }
        public IActionResult Classify() //楊 分類
        {
            return View();
        }
       
        public IActionResult Souvenir() //伴手禮
        {
            
            return View();
        }
        public IActionResult CityClassify() //城市 分類
        {
           
            return View();
        }
        public IActionResult CityTour(int id) //各城市導覽頁b 

        {
            var cityRequest = new CityRequestDTO
            {
                CityId = id
            }; 
            var CityDetail = _cityServices.GetCity(cityRequest);
            var CityAllCard = _cityServices.GetAllCity();
            var CityAllRaider = _cityServices.GetRaiderCard();
            var cityAllComment = _cityServices.GetAllComment();

            var cityTourPage = new CityVM
            {
                CurrentCityInfo = new CityVM.CityInfo
                {
                    Id = CityDetail.CityInfo.Id,
                    CityName = CityDetail.CityInfo.CityName,
                    CityImage = CityDetail.CityInfo.CityImage,
                    CityIntrod = CityDetail.CityInfo.CityIntrod
                },

                cityCardList = CityAllCard.Where(x => x.Id > id).Take(10).Select(cc => new CityVM.CityCardList
                {
                    Id = cc.Id,
                    CityImage = cc.CityImage,
                    CityName = cc.CityName,
                }).ToList(),
                RaiderList = CityAllRaider.Where(x => x.CityId == id).Select(rl => new CityVM.CityRaiderList
                {
                    CityId = rl.CityId,
                    Title = rl.Title,
                    SubTitle = rl.SubTitle
                }).ToList(),
                commentList = cityAllComment.Where(x => x.CityId == id).Select(cl => new CityVM.CityCommentList
                {
                    CityId = cl.CityId,
                    Name = cl.Name,
                    RatingStar = cl.RatingStar,
                    CommentDate = cl.CommentDate,
                    UseDate = cl.UseDate,
                    PartnerType = cl.PartnerType,
                    ProductName = cl.ProductName,
                    Text = cl.Text,
                    Title = cl.Title
                }).ToList()
                


            };

            return View(cityTourPage);
        }

        public IActionResult CityRaiders(int id) //城市攻略
        {
            var RaiderRequest = new RaiderRequestDTO
            {
                RaiderId = id
            };
            var Raidercontent = _cityServices.GetRaiders(RaiderRequest);
            var CityRaider = new RaiderVM
            {
                RaiderPage = new RaiderVM.RaiderInfo
                {
                    Id = Raidercontent.RaiderInfo.Id,
                    CityId = Raidercontent.RaiderInfo.CityId,
                    Title = Raidercontent.RaiderInfo.Title,
                    Subtitle = Raidercontent.RaiderInfo.Subtitle,
                    Text = Raidercontent.RaiderInfo.Text,
                    Video = Raidercontent.RaiderInfo.Video,
                }
            };   
         
            return View(CityRaider);
        }
        public IActionResult OffIsland() //離島 分類
        {
            return View();
        }
        public IActionResult ParentChild() //親子 分類
        {
            return View();
        }
        public IActionResult DIY() //DIY 分類
        {
            return View();
        }
        public IActionResult HSRClassify() //高鐵 分類
        {
            return View();
        }
        public IActionResult Rent() //租車 分類
        {
            return View();
        }
        public IActionResult Camping() //露營頁面
        {
            return View();
        }
        public IActionResult QuarantineHotel() //防疫旅館頁面
        {
            return View();
        }
        public IActionResult HSR() //國旅
        {
            return View();
        }
        public IActionResult AboutToday() //公司介紹
        {
            return View();
        }
    }
}
