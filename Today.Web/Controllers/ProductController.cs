using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Web.Services;
using Today.Web.Services.CityService;
using Today.Web.Services.ProductInfoService;
using Today.Web.ViewModels;
using static Today.Web.ViewModels.ProductInfoVM;

namespace Today.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly ICityService _cityServices;
        private readonly IProductInfoService _productInfoService;

        //public ProductController(ICityService cityServices)
        //{
        //    _cityServices = cityServices;
        //}
        public ProductController(IProductInfoService productInfoService, ICityService cityServices)
        {
            _productInfoService = productInfoService;
            _cityServices = cityServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductInfo(int id) //商品頁面
        {
            if (id <= 0)
            {
                return Content("找不到商品");
            }
            else
            {
                var productPagesServiceDTO = _productInfoService.GetProduct(new DTOModel.ProductInfoDTO.ProductInfoRequstDTO { ProductId = id });
                ;
                var productinfo = new ProductInfoVM
                {
                    ProductIsdeleted = productPagesServiceDTO.ProductInfo.ProductIsdeleted,
                    ShoppingNotice = productPagesServiceDTO.ProductInfo.ShoppingNotice,
                    ProductId = productPagesServiceDTO.ProductInfo.ProductId,
                    CancellationPolicy = productPagesServiceDTO.ProductInfo.CancellationPolicy,
                    HowUse = productPagesServiceDTO.ProductInfo.HowUse,
                    ProductName = productPagesServiceDTO.ProductInfo.ProductName,
                    CityName = productPagesServiceDTO.ProductInfo.CityName,
                    Producttag = productPagesServiceDTO.ProductInfo.ProductTag,
                    ProductlocationName = productPagesServiceDTO.ProductInfo.ProductLocationName,
                    ProductText = productPagesServiceDTO.ProductInfo.ProductDesc,
                    ProductLocationAddress = productPagesServiceDTO.ProductInfo.ProductLocationAddress,
                    PhtotList = productPagesServiceDTO.ProductInfo.PhtotList.Select(p =>
                    new ProductInfoVM.Photo
                    {
                        PhotoUrl = p.PhotoUrl
                    }).ToList(),
                    ProgarmList = productPagesServiceDTO.ProductInfo.ProgarmList.Select(p =>
                    new ProductInfoVM.Progarm
                    {
                        ProgarmIsdeleted = p.ProgarmIsdeleted,
                        PrgramName = p.PorgramName,
                        PrgarmText = p.PrgarmText,
                        DateList = p.DateList.Select(d =>
                        new Date
                        {
                            CantuseDate = d.CantuseDate
                        }).ToList(),
                        AboutProgramList = p.AboutProgramList.Select(ap => new ProductInfoVM.AboutProgram
                        {
                            AboutProgramName = ap.AboutProgramName,
                            IconClass = ap.IconClass,
                        }).ToList(),
                        ProgramIncludeList = p.ProgramInciudeList.Select(pi =>
                        new ProductInfoVM.ProgramInclude
                        {
                            Inciudetext = pi.Inciudetext,
                            IsInclude = pi.IsInclude,
                        }).ToList(),
                        ScreeningList = p.ScreeningList.Select(p => new ProductInfoVM.Screening
                        {
                            Date = p.Date,
                            ScreenId = p.ScreenId,
                            SpecificationId = p.SpecificationId,
                            Status =p.Status
                        }).ToList()
                        ,
                        ProgramSpecificationList = p.ProgramSpecificationList.Select(pgsc =>
                            new ProductInfoVM.ProgramSpecification
                            {
                                PorgarmUnitPrice = pgsc.PorgarmUnitPrice,
                                Itemtext = pgsc.Itemtext,
                                UnitText = pgsc.UnitText,
                            }).ToList()
                    }).ToList()
                };
                ViewData["ProgramSpecification"] = JsonConvert.SerializeObject(productinfo.ProgarmList);
                return View(productinfo);
                //return View();
            }

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
        public IActionResult CityTour() //各城市導覽頁
        {

            var CityPages = _cityServices.GetCityPages();
            return View(CityPages);
        }
        public IActionResult CityRaiders() //城市攻略
        {
            return View();
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
