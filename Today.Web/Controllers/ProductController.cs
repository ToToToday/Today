using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Web.Services;
using Today.Web.Services.CityService;
using Today.Web.Services.ProductService;
using Today.Web.Services.ClassifyService;
using Today.Web.Services.locationService;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.CityDTO.CityDTO;
using static Today.Web.DTOModels.CityDTO.RaiderDTO;

namespace Today.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly ICityService _cityServices;
        private readonly IProductService _productServices;
        private readonly ILocationService _locationServices;

        private readonly IClassifyService _classifyService;
        
        
        public ProductController(ICityService cityServices, ILocationService locationServices, IProductService productService, IClassifyService classifyService)
        {
            _cityServices = cityServices;
            _productServices = productService;
            _locationServices = locationServices;
            _classifyService = classifyService;

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
            var classPages = _classifyService.GetClassifyPages();
            var cardsource = classPages.ClassifyCardList.ToList();
            var Categorysource = classPages.CategoryList.ToList();
            var result = new ClassifyVM()
            {
                ClassifyCardList = cardsource.Select(c => new ClassifyVM.ClassifyCardInfo
                {
                    ProductName = c.ProductName,
                    CityName = c.CityName,
                    Path = c.Path,
                    TagText = c.TagText,
                    UnitPrice = c.UnitPrice,
                    Evaluation = c.Evaluation
                }).ToList(),
                CategoryList = Categorysource.Select(x => new ClassifyVM.CategoryDestinations
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    ChildCategory = x.ChildCategory.Select(y => new ClassifyVM.CategoryDestinations()
                    {
                        Id = y.Id,
                        CategoryName = y.CategoryName
                    }).ToList()
                }).ToList()
            };
            
            return View(result);
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
            var CityAllCard = _cityServices.GetAllCity(cityRequest);
            var CityAllRaider = _cityServices.GetRaiderCard(cityRequest);
            var CityAllComment = _cityServices.GetAllComment(cityRequest);
            var NewActiviy = _cityServices.GetNewActiviy(cityRequest);
            var AboutProduct = _cityServices.GetAboutProduct(cityRequest);
            var TopTen = _cityServices.GetTopTen(cityRequest);
            var cityTourPage = new CityVM
            {
                CurrentCityInfo = new CityVM.CityInfo
                {
                    Id = CityDetail.CityInfo.Id,
                    CityName = CityDetail.CityInfo.CityName,
                    CityImage = CityDetail.CityInfo.CityImage,
                    CityIntrod = CityDetail.CityInfo.CityIntrod
                },

                CityCardsList = CityAllCard.Select(cc => new CityVM.CityCardList
                {
                    Id = cc.Id,
                    CityImage = cc.CityImage,
                    CityName = cc.CityName,
                }).ToList(),
                RaiderList = CityAllRaider.Select(rl => new CityVM.CityRaiderList
                {
                    CityId = rl.CityId,
                    Title = rl.Title,
                    SubTitle = rl.SubTitle
                }).ToList(),
                CommentList = CityAllComment.Select(cl => new CityVM.CityCommentList
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
                }).ToList(),
                NewActiviyList = NewActiviy.Select(newp => new CityVM.ProductCardVM
                {
                    Id = newp.Id,
                    ProductPhoto = newp.ProductPhoto,
                    ProductName = newp.ProductName,
                    Tags = newp.Tags,
                    CityName = newp.CityName,
                    OriginalPrice = newp.OriginalPrice,
                    Price =newp.Price,
                    Rating = newp.Rating,
                    TotalGiveComment = newp.TotalComment,
                    TotalOrder = newp.Quantity


                }).ToList(),
                AboutActiviyList = AboutProduct.Select(aboutp => new CityVM.ProductCardVM
                {
                    Id = aboutp.Id,
                    ProductPhoto = aboutp.ProductPhoto,
                    ProductName = aboutp.ProductName,
                    Tags = aboutp.Tags,
                    CityName = aboutp.CityName,
                    OriginalPrice = aboutp.OriginalPrice,
                    Price = aboutp.Price
                }).ToList(),
                TopActiviyList = TopTen.Select(top => new CityVM.ProductCardVM
                {
                    Id = top.Id,
                    ProductPhoto = top.ProductPhoto,
                    ProductName = top.ProductName,
                    Tags = top.Tags,
                    CityName = top.CityName,
                    OriginalPrice = top.OriginalPrice,
                    Price = top.Price
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
            var getLocation = _locationServices.GetLocations();
            var getProduct = _locationServices.GetProducts();
            var getPhoto = _locationServices.GetPhoto();
            var result = new LocationVM
            {
                GetLocation = getLocation.Select(lo => new LocationVM.ProductLocation
                {
                    Id = lo.locationID,
                    latitude = lo.Latitude,
                    longitude = lo.Longitude
                }).ToList(),
                GetProdocutName = getProduct.Select(p => new LocationVM.GetProduct
                {
                    ProductId = p.ProductId, 
                    ProductName = p.ProductName,
                }).ToList()
                ,
                GetProductPhoto = getPhoto.Select(p => new LocationVM.ProductPhoto
                {
                    PhotoId = p.PhotoId,
                    PhotoPath = p.Path,
                    ProductId = p.ProductId,
                    Sort = p.Sort
                }).ToList(),
            };
            string locationJson = System.Text.Json.JsonSerializer.Serialize(result.GetLocation); //把資料編碼 
            string ProductNamedJson = System.Text.Json.JsonSerializer.Serialize(result.GetProdocutName);
            string GetPhotoJson = System.Text.Json.JsonSerializer.Serialize(result.GetProductPhoto);
            ViewData["locationJson"] = locationJson;
            ViewData["PNameJson"] = ProductNamedJson;
            ViewData["PhotoJson"] = GetPhotoJson;
            return View(result);
        }
        public IActionResult ParentChild() //親子 分類
        {
            ViewData["banner-h2"] = "特搜親子體驗！親子餐廳・親子旅遊・親子住宿";
            ViewData["banner-p"] = "Today 親子旅遊特搜200+項親子體驗活動！不可錯過親子餐廳、親子住宿以及全台灣各縣市親子景點！小朋友參加營隊放電、科學課程輕鬆學習，大人無憂度假";
            ViewData["banner-img"] = "https://cdn.kkday.com/m-web/assets/img/family/family-banner.jpg";
            ViewData["banner-location-word"] = "目的地";
            ViewData["banner-date-word"] = "日期";
            ViewData["collapse-search"] = "請選擇取車地點及日期";


            var getLocation = _locationServices.GetLocations();
            var getProduct = _locationServices.GetProducts();
            var getPhoto = _locationServices.GetPhoto();
            var result = new LocationVM
            {
                GetLocation = getLocation.Select(lo => new LocationVM.ProductLocation
                {
                    Id = lo.locationID,
                    latitude = lo.Latitude,
                    longitude = lo.Longitude
                }).ToList(),
                GetProdocutName = getProduct.Select(p => new LocationVM.GetProduct
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                }).ToList()
                ,
                GetProductPhoto = getPhoto.Select(p => new LocationVM.ProductPhoto
                {
                    PhotoId = p.PhotoId,
                    PhotoPath = p.Path,
                    ProductId = p.ProductId,
                    Sort = p.Sort
                }).ToList()
            };

            string locationJson = System.Text.Json.JsonSerializer.Serialize(result.GetLocation); //把資料編碼 
            string ProductNamedJson = System.Text.Json.JsonSerializer.Serialize(result.GetProdocutName);
            string GetPhotoJson = System.Text.Json.JsonSerializer.Serialize(result.GetProductPhoto);
            ViewData["locationJson"] = locationJson;
            ViewData["PNameJson"] = ProductNamedJson;
            ViewData["PhotoJson"] = GetPhotoJson;
            return View(result);
        }
        public IActionResult DIY() //DIY 分類
        {
            ViewData["banner-h2"] = "手作課程一次看！蛋糕甜點・蠟燭香氛・花藝植栽";
            ViewData["banner-p"] = "風格手作體驗活動，讓你輕鬆將儀式感帶入日常生活";
            ViewData["banner-img"] = "https://image.kkday.com/v2/image/get/w_1920%2Cc_fit%2Cq_75%2Ct_webp/s1.kkday.com/campaign_1670/20210519100328_WsPdN/jpg";
            ViewData["banner-location-word"] = "目的地";
            ViewData["banner-date-word"] = "出發日期";
            ViewData["collapse-search"] = "請選擇目的地與日期";

            var getLocation = _locationServices.GetLocations();
            var getProduct = _locationServices.GetProducts();
            var getPhoto = _locationServices.GetPhoto();
            var result = new LocationVM
            {
                GetLocation = getLocation.Select(lo => new LocationVM.ProductLocation
                {
                    Id = lo.locationID,
                    latitude = lo.Latitude,
                    longitude = lo.Longitude
                }).ToList(),
                GetProdocutName = getProduct.Select(p => new LocationVM.GetProduct
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                }).ToList()
                ,
                GetProductPhoto = getPhoto.Select(p => new LocationVM.ProductPhoto
                {
                    PhotoId = p.PhotoId,
                    PhotoPath = p.Path,
                    ProductId = p.ProductId,
                    Sort = p.Sort
                }).ToList()
            };

            string locationJson = System.Text.Json.JsonSerializer.Serialize(result.GetLocation);
            string ProductNamedJson = System.Text.Json.JsonSerializer.Serialize(result.GetProdocutName);
            string GetPhotoJson = System.Text.Json.JsonSerializer.Serialize(result.GetProductPhoto);
            ViewData["locationJson"] = locationJson;
            ViewData["PNameJson"] = ProductNamedJson;
            ViewData["PhotoJson"] = GetPhotoJson;
            return View(result);
        }
        public IActionResult HSRClassify() //高鐵 分類
        {
            ViewData["banner-h2"] = "台灣高鐵國旅聯票";
            ViewData["banner-p"] = "【台灣高鐵國旅聯票85折】租車・樂園門票及更多高鐵優惠組合，從租車、樂園門票到在地體驗，一指下訂擁有台灣高鐵85折優惠！取票零接觸，高鐵「T-EX行動購票」App直接兌換車票！輕鬆抵達高鐵沿線城市、盡情體驗屬於你的愉快假期";
            ViewData["banner-img"] = "https://cdn.kkday.com/pc-web/assets/img/thsr/thsr-banner.jpeg";
            ViewData["banner-location-word"] = "你要去哪裡玩?";
            ViewData["banner-date-word"] = "出發日期";
            ViewData["collapse-search"] = "你要去哪裡玩?";

            var getLocation = _locationServices.GetLocations();
            var getProduct = _locationServices.GetProducts();
            var getPhoto = _locationServices.GetPhoto();
            var result = new LocationVM
            {
                GetLocation = getLocation.Select(lo => new LocationVM.ProductLocation
                {
                    Id = lo.locationID,
                    latitude = lo.Latitude,
                    longitude = lo.Longitude
                }).ToList(),
                GetProdocutName = getProduct.Select(p => new LocationVM.GetProduct
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                }).ToList()
                ,
                GetProductPhoto = getPhoto.Select(p => new LocationVM.ProductPhoto
                {
                    PhotoId = p.PhotoId,
                    PhotoPath = p.Path,
                    ProductId = p.ProductId,
                    Sort = p.Sort
                }).ToList()
            };

            string locationJson = System.Text.Json.JsonSerializer.Serialize(result.GetLocation);
            string ProductNamedJson = System.Text.Json.JsonSerializer.Serialize(result.GetProdocutName);
            string GetPhotoJson = System.Text.Json.JsonSerializer.Serialize(result.GetProductPhoto);
            ViewData["locationJson"] = locationJson;
            ViewData["PNameJson"] = ProductNamedJson;
            ViewData["PhotoJson"] = GetPhotoJson;
            return View(result);
        }
        public IActionResult Rent() //租車 分類
        {
            ViewData["banner-h2"] = "租車推薦 即刻預訂享折扣・輕鬆享受自駕遊";
            ViewData["banner-p"] = "多元的租車商品與Today獨家優惠，讓你的自駕遊，安全輕鬆沒煩惱！";
            ViewData["banner-img"] = "https://cdn.kkday.com/pc-web/assets/img/car_rentals/car-rentals-banner.jpg";
            ViewData["banner-location-word"] = "取車地點";
            ViewData["banner-date-word"] = "取車日期";
            ViewData["collapse-search"] = "請選擇取車地點及日期";

            var getLocation = _locationServices.GetLocations();
            var getProduct = _locationServices.GetProducts();
            var getPhoto = _locationServices.GetPhoto();
            var result = new LocationVM
            {
                GetLocation = getLocation.Select(lo => new LocationVM.ProductLocation
                {
                    Id = lo.locationID,
                    latitude = lo.Latitude,
                    longitude = lo.Longitude
                }).ToList(),
                GetProdocutName = getProduct.Select(p => new LocationVM.GetProduct
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                }).ToList()
                ,
                GetProductPhoto = getPhoto.Select(p => new LocationVM.ProductPhoto
                {
                    PhotoId = p.PhotoId,
                    PhotoPath = p.Path,
                    ProductId = p.ProductId,
                    Sort = p.Sort
                }).ToList()
            };

            string locationJson = System.Text.Json.JsonSerializer.Serialize(result.GetLocation);
            string ProductNamedJson = System.Text.Json.JsonSerializer.Serialize(result.GetProdocutName);
            string GetPhotoJson = System.Text.Json.JsonSerializer.Serialize(result.GetProductPhoto);
            ViewData["locationJson"] = locationJson;
            ViewData["PNameJson"] = ProductNamedJson;
            ViewData["PhotoJson"] = GetPhotoJson;
            return View(result);
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
