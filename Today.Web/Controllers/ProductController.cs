using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Web.Services;
using Today.Web.Services.CityService;
using Today.Web.Services.ProductPagesService;
using Today.Web.ViewModels;


namespace Today.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly ICityService _cityServices;
        private readonly IProductPagesService _productPagesService;

        //public ProductController(ICityService cityServices)
        //{
        //    _cityServices = cityServices;
        //}
        public ProductController(IProductPagesService productPagesService, ICityService cityServices)
        {
            _productPagesService = productPagesService;
            _cityServices = cityServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductPages(int id = 4) //商品頁面
        {
            var productPagesServiceDTO = _productPagesService.GetProduct(new DTOModel.ProductInfoDTO.ProductInfoRequstDTO { ProductId = id });
            var productinfo = new ProductPagesVM
            {
                ShoppingNotice = productPagesServiceDTO.ProductInfo.ShoppingNotice,
                ProductId = productPagesServiceDTO.ProductInfo.ProductId,
                CancellationPolicy = productPagesServiceDTO.ProductInfo.CancellationPolicy ,
                HowUse = productPagesServiceDTO.ProductInfo.HowUse,
                ProductName = productPagesServiceDTO.ProductInfo.ProductName,
                CityName = productPagesServiceDTO.ProductInfo.CityName,
                Producttag  = productPagesServiceDTO.ProductInfo.ProductTag,
                ProductlocationName = productPagesServiceDTO.ProductInfo.ProductLocationName,
                ProductText = productPagesServiceDTO.ProductInfo.ProductText,
                ProductLocationAddress = productPagesServiceDTO.ProductInfo.ProductLocationAddress,
                PhtotList  = productPagesServiceDTO.ProductInfo.PhtotList.Select(p=>
                new ProductPagesVM.Photo
                {
                    PhotoUrl = p.PhotoUrl
                }).ToList(),
                progarmList = productPagesServiceDTO.ProductInfo.progarmList.Select(p =>
                new ProductPagesVM.Progarm
                {
                    porgramname = p.PorgramName,
                    PrgarmText = p.PrgarmText,
                }).ToList()
            };
            //return Json(productinfo);
            return View(productinfo);
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
