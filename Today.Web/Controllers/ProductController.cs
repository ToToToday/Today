using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Web.DTOModels.ShopCartDTO;
using Today.Web.Services;
using Today.Web.Services.CityService;
using Today.Web.Services.ProductService;
using Today.Web.Services.ClassifyService;
using Today.Web.Services.locationService;
using Today.Web.Services.ProductInfoService;
using Today.Web.Services.ShopCartService;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.CityDTO.CityDTO;
using static Today.Web.DTOModels.CityDTO.RaiderDTO;
using static Today.Web.ViewModels.ProductInfoVM;
using Today.Web.DTOModels.ProductInfoDTO;
using static Today.Web.DTOModels.ClassifyDTO.ClassifyDTO;

using static Today.Web.DTOModels.ShopCartMemberDTO;
using static Today.Web.DTOModels.ShopCartMemberDTO.ShopCartMemberResponseDTO;
using static Today.Web.ViewModels.ShopCartVM;
using Today.Web.DTOModels.ClassifyDTO;

namespace Today.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICityService _cityServices;
        private readonly IProductService _productServices;
        private readonly ILocationService _locationServices;
        private readonly IProductInfoService _productInfoService;
        private readonly IClassifyService _classifyService;
        private readonly IShopCartService _shopCartService;


        public ProductController(ICityService cityServices, ILocationService locationServices, IProductService productService, IClassifyService classifyService, IProductInfoService productInfoService, IShopCartService shopCartService)
        {
            //_productInfoService = productInfoService;
            _cityServices = cityServices;
            _productServices = productService;
            _locationServices = locationServices;
            _classifyService = classifyService;
            _productInfoService = productInfoService;
            _shopCartService = shopCartService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductInfo(int id) //商品頁面
        {
            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;
            if (id <= 0)
            {
                return Content("找不到商品");
            }
            else
            {
                var productPagesServiceDTO = _productInfoService.GetProduct(new ProductInfoDTO.ProductInfoRequstDTO { ProductId = id, MemberId = userId });
                ;
                var productinfo = new ProductInfoVM
                {
                    ProductIsdeleted = productPagesServiceDTO.ProductInfo.ProductIsdeleted,
                    ShoppingNotice = productPagesServiceDTO.ProductInfo.ShoppingNotice,
                    ProductId = productPagesServiceDTO.ProductInfo.ProductId,
                    CancellationPolicy = productPagesServiceDTO.ProductInfo.CancellationPolicy,
                    HowUse = productPagesServiceDTO.ProductInfo.HowUse,
                    ProductName = productPagesServiceDTO.ProductInfo.ProductName,
                    Favorite = productPagesServiceDTO.ProductInfo.Favorite,
                    CityName = productPagesServiceDTO.ProductInfo.CityName,
                    Producttag = productPagesServiceDTO.ProductInfo.ProductTag,
                    ProductlocationName = productPagesServiceDTO.ProductInfo.ProductLocationName,
                    ProductText = productPagesServiceDTO.ProductInfo.ProductDesc,
                    ProductLocationAddress = productPagesServiceDTO.ProductInfo.ProductLocationAddress,
                    MemberList = productPagesServiceDTO.ProductInfo.MemberComment.Select(m => new MemberComment
                    {
                        MembermMessageText = m.MembermMessageText,
                        MemberName = m.MemberName,
                        MemberPhoto = m.MemberPhoto,
                        MemberId = m.MemberId,
                        CommentId = m.CommentId,
                        Star = m.Star,
                        Data = m.Data
                    }).ToList(),
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
                        //ScreeningList = p.ScreeningList.Select(p => new ProductInfoVM.Screening
                        //{
                        //    Date = p.Date,
                        //    ScreenId = p.ScreenId,
                        //    SpecificationId = p.SpecificationId,
                        //    Status = p.Status
                        //}).ToList(),
                        ProgramSpecificationList = p.ProgramSpecificationList.Select(pgsc =>
                            new ProductInfoVM.ProgramSpecification
                            {
                                SpecificationId = pgsc.SpecificationId,
                                PorgarmUnitPrice = pgsc.PorgarmUnitPrice,
                                Itemtext = pgsc.Itemtext,
                                UnitText = pgsc.UnitText,
                            }).ToList()
                    }).ToList()
                };



                ViewData["ProgramSpecification"] = JsonConvert.SerializeObject(productinfo.ProgarmList);
               return View(productinfo);
            }

        }
        public IActionResult Classify(int id) //楊 路由會收到 天生的篩選參數
        {
            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;
            var classifyFilters = _classifyService.GetClassifyFilters();
            
            var result = new ClassifyVM()
            {
                AllFilters = new FilterVM
                {
                    CityFilterList = classifyFilters.CityFilterList, //空
                    CategoryFilterList = classifyFilters.CategoryFilterList,
                },
                //AllSorts = classifySorts.AllSorts,
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
            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;
            var cityRequest = new CityRequestDTO
            {
                CityId = id,
                MemberId = userId
            };
            var CityDetail = _cityServices.GetCity(cityRequest);
            var CityAllCard = _cityServices.GetAllCity(cityRequest);
            var CityAllRaider = _cityServices.GetRaiderCard(cityRequest);
            var CityAllComment = _cityServices.GetAllComment(cityRequest);
            var getcard = _cityServices.GetAllCard(cityRequest);

            var getLocations = _locationServices.GetLocation();
            var getLocation = getLocations.ProductLocationList.ToList();
            var getCityLocation = _locationServices.GetCityLocation(id).CityLocationList.ToList();
            var cityTourPage = new CityVM
            {
                ProductLocationList = getLocation.Where(x=>x.CityId==id).Select(lo => new LocationVM.ProductLocation
                {
                    ProductId = lo.ProductId,
                    LocationId = lo.LocationId,
                    CityId = lo.CityId,
                    Price = lo.Price,
                    IsIsland = lo.IsIsland,
                    //PhotoId= lo.PhotoId,
                    Longitude = lo.Longitude,
                    Latitude = lo.Latitude,
                    ProductName = lo.ProductName,
                    Path = lo.Path,
                    RatingStar = (float)Math.Floor(lo.RatingStar * 10000) / 10000,
                }).ToList(),
                CityLocationList = getCityLocation.Select(c=>new LocationVM.CityLocation
                {
                    CityId=c.CityId,
                    City_Latitude=c.City_Latitude,
                    City_Longtitude=c.City_Longtitude
                }).ToList(),
                CurrentCityInfo = new CityVM.CityInfo
                {
                    Id = CityDetail.CityInfo.Id,
                    CityName = CityDetail.CityInfo.CityName,
                    CityImage = CityDetail.CityInfo.CityImage,
                    CityIntrod = CityDetail.CityInfo.CityIntrod
                },

                CityCardsList = CityAllCard.Select(cc => new CityVM.CityCardList
                {
                    Id =    cc.Id,
                    CityImage = cc.CityImage,
                    CityName = cc.CityName,
                }).ToList(),
                RaiderList = CityAllRaider.Select(rl => new CityVM.CityRaiderList
                {
                    RaiderId = rl.RaiderId,
                    photo = rl.photo,
                    CityId = rl.CityId,
                    Title = rl.Title,
                    SubTitle = rl.SubTitle
                }).ToList(),
                CommentList = CityAllComment.Select(cl => new CityVM.CityCommentList
                {
                    CityId = cl.CityId,
                    Name = cl.Name,
                    RatingStar = cl.RatingStar,
                    CommentDate = string.Format("{0:yyyy/MM/dd}", cl.CommentDate),
                    UseDate = string.Format("{0:yyyy/MM/dd}", cl.UseDate),
                    PartnerType = cl.PartnerType,
                    ProductName = cl.ProductName,
                    Text = cl.Text,
                    Title = cl.Title,
                    ProductId = cl.ProductId
                    
                }).ToList(),
                NewActiviyList = getcard.NewProductList.Select(newp => new CityVM.ProductCardVM
                {
                    Id = newp.Id,
                    ProductPhoto = newp.ProductPhoto,
                    ProductName = newp.ProductName,
                    Favorite = newp.Favorite,
                    Tags = newp.Tags,
                    CityName = newp.CityName,
                    OriginalPrice = (newp.Prices == null || newp.Prices.OriginalPrice == newp.Prices.Price) ? null : newp.Prices.OriginalPrice,
                    Price = (newp.Prices == null) ? null : newp.Prices.Price,
                    Rating = Math.Round(newp.Rating.RatingStar, 1),
                    TotalGiveComment = newp.Rating.TotalGiveComment,
                    TotalOrder = newp.TotalOrder
                }).ToList(),
                AboutActiviyList = getcard.AboutProductList.Select(aboutp => new CityVM.ProductCardVM
                {
                    Id = aboutp.Id,
                    ProductPhoto = aboutp.ProductPhoto,
                    ProductName = aboutp.ProductName,
                    Favorite = aboutp.Favorite,
                    Tags = aboutp.Tags,
                    CityName = aboutp.CityName,
                    OriginalPrice = (aboutp.Prices == null || aboutp.Prices.OriginalPrice == aboutp.Prices.Price) ? null : aboutp.Prices.OriginalPrice,
                    Price = (aboutp.Prices == null) ? null : aboutp.Prices.Price,
                    Rating = Math.Round(aboutp.Rating.RatingStar, 1),
                    TotalGiveComment = aboutp.Rating.TotalGiveComment,
                    TotalOrder = aboutp.TotalOrder
                }).ToList(),
                TopActiviyList = getcard.TopProductList.Select(top => new CityVM.ProductCardVM
                {
                    Id = top.Id,
                    ProductPhoto = top.ProductPhoto,
                    ProductName = top.ProductName,
                    Favorite = top.Favorite,
                    Tags = top.Tags,
                    CityName = top.CityName,
                    OriginalPrice = (top.Prices == null || top.Prices.OriginalPrice == top.Prices.Price) ? null : top.Prices.OriginalPrice,
                    Price = (top.Prices == null) ? null : top.Prices.Price,
                    Rating = Math.Round(top.Rating.RatingStar, 1),
                    TotalGiveComment = top.Rating.TotalGiveComment,
                    TotalOrder = top.TotalOrder
                }).ToList()

            };
            string locationJson = System.Text.Json.JsonSerializer.Serialize(cityTourPage.ProductLocationList); //把資料編碼 
            ViewData["locationJson"] = locationJson;

            string locationCityJson = System.Text.Json.JsonSerializer.Serialize(cityTourPage.CityLocationList);
            ViewData["locationCityJson"] = locationCityJson;
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

        public IActionResult OffIsland(int id ,String searchString) //離島 分類
        {
            var getLocation = _locationServices.GetLocation().ProductLocationList.ToList();
            var getOffCity = _locationServices.GetOffIslandCard().OffIslandList.ToList();
            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;



            var classFilters = _classifyService.GetClassifyFilters();
            var result = new LocationVM()
            {   
                ProductLocationList = getLocation.Where(c=>c.IsIsland==false).Select(lo => new LocationVM.ProductLocation
                {
                    ProductId = lo.ProductId,
                    LocationId = lo.LocationId,
                    CityId = lo.CityId,
                    IsIsland =lo.IsIsland,
                    Price = lo.Price,
                    //PhotoId= lo.PhotoId,
                    Longitude = lo.Longitude,
                    Latitude=lo.Latitude,
                    ProductName = lo.ProductName,
                    Path = lo.Path,
                    RatingStar = (float)Math.Floor(lo.RatingStar * 10000) / 10000,
                }).ToList(),
                FilterList = getOffCity.Select(x=> new LocationVM.FilterCity
                {
                    CityId=x.CityId,
                    CityImage= x.CityImage,
                    CityName=x.CityName,
                }).ToList(),
                AllFilters = new FilterVM
                {
                    CityFilterList = classFilters.CityFilterList, //空
                    CategoryFilterList = classFilters.CategoryFilterList,
                },
            };

            return View(result);
        }
        public IActionResult ParentChild([FromQuery] List<string> typeDate, int id) //親子 分類
        {
            ViewData["banner-h2"] = "特搜親子體驗！親子餐廳・親子旅遊・親子住宿";
            ViewData["banner-p"] = "Today 親子旅遊特搜200+項親子體驗活動！不可錯過親子餐廳、親子住宿以及全台灣各縣市親子景點！小朋友參加營隊放電、科學課程輕鬆學習，大人無憂度假";
            ViewData["banner-img"] = "https://cdn.kkday.com/m-web/assets/img/family/family-banner.jpg";
            ViewData["banner-location-word"] = "目的地";
            ViewData["banner-date-word"] = "日期";
            ViewData["collapse-search"] = "請選擇取車地點及日期";

            var getLocations = _locationServices.GetLocation();
            var getLocation = getLocations.ProductLocationList.ToList();
            var getCard = _locationServices.GetParentCard().GetParentCardList.ToList();


            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;


            var classFilters = _classifyService.GetClassifyFilters();


            var result = new LocationVM()
            {
                ProductLocationList = getLocation.Where(x => x.CategoryId == 22).Select(lo => new LocationVM.ProductLocation
                {
                    ProductId = lo.ProductId,
                    LocationId = lo.LocationId,
                    CityId = lo.CityId,
                    Price = lo.Price,
                    IsIsland = lo.IsIsland,
                    Longitude = lo.Longitude,
                    Latitude = lo.Latitude,
                    ProductName = lo.ProductName,
                    Path = lo.Path,
                    RatingStar = (float)Math.Floor(lo.RatingStar * 10000) / 10000,
                    
                }).ToList(),
                GetParentCardList = getCard.Select(Ca=> new LocationVM.GetParentCard
                {
                    Id=Ca.ProductId,
                    ProductPhoto = Ca.ProductPhoto,
                    ProductName = Ca.ProductName,
                    Tags = Ca.Tags,
                    CityName = Ca.CityName,
                    OriginalPrice = (Ca.Prices == null || Ca.Prices.OriginalPrice == Ca.Prices.Price) ? null : Ca.Prices.OriginalPrice,
                    Price = (Ca.Prices == null) ? null : Ca.Prices.Price,
                    Rating = (float)Math.Floor(Ca.Rating.RatingStar*10000)/10000,
                    TotalGiveComment = Ca.Rating.TotalGiveComment,
                    TotalOrder = Ca.TotalOrder,
                    Favorite = Ca.Favorite,
                }).OrderByDescending(d => d.Rating).Take(8).ToList(),

                AllFilters = new FilterVM
                {
                    CityFilterList = classFilters.CityFilterList, //空
                    CategoryFilterList = classFilters.CategoryFilterList,
                },
            };


            return View(result);
        }                                                                                                                                                                                                          



        public IActionResult DIY([FromQuery] List<string> typeDate, int id) //DIY 分類
        {
            ViewData["banner-h2"] = "手作課程一次看！蛋糕甜點・蠟燭香氛・花藝植栽";
            ViewData["banner-p"] = "風格手作體驗活動，讓你輕鬆將儀式感帶入日常生活";
            ViewData["banner-img"] = "https://image.kkday.com/v2/image/get/w_1920%2Cc_fit%2Cq_75%2Ct_webp/s1.kkday.com/campaign_1670/20210519100328_WsPdN/jpg";
            ViewData["banner-location-word"] = "目的地";
            ViewData["banner-date-word"] = "出發日期";
            ViewData["collapse-search"] = "請選擇目的地與日期";

            var getLocations = _locationServices.GetLocation();
            var getLocation = getLocations.ProductLocationList.ToList();


            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;
            var classFilters = _classifyService.GetClassifyFilters();


            var result = new LocationVM()
            {
                ProductLocationList = getLocation.Where(x => x.CategoryId == 53).Select(lo => new LocationVM.ProductLocation
                {
                    ProductId = lo.ProductId,
                    LocationId = lo.LocationId,
                    CityId = lo.CityId,
                    Price = lo.Price,
                    IsIsland = lo.IsIsland,
                    //PhotoId= lo.PhotoId,
                    Longitude = lo.Longitude,
                    Latitude = lo.Latitude,
                    ProductName = lo.ProductName,
                    Path = lo.Path,
                    RatingStar = (float)Math.Floor(lo.RatingStar * 10000) / 10000,

                }).ToList(),

                AllFilters = new FilterVM
                {
                    CityFilterList = classFilters.CityFilterList, //空
                    CategoryFilterList = classFilters.CategoryFilterList,
                },
            };


            return View(result);
        }
        public IActionResult HSRClassify([FromQuery] List<string> typeDate, int id) //高鐵 分類
        {
            ViewData["banner-h2"] = "台灣高鐵國旅聯票";
            ViewData["banner-p"] = "【台灣高鐵國旅聯票85折】租車・樂園門票及更多高鐵優惠組合，從租車、樂園門票到在地體驗，一指下訂擁有台灣高鐵85折優惠！取票零接觸，高鐵「T-EX行動購票」App直接兌換車票！輕鬆抵達高鐵沿線城市、盡情體驗屬於你的愉快假期";
            ViewData["banner-img"] = "https://cdn.kkday.com/pc-web/assets/img/thsr/thsr-banner.jpeg";
            ViewData["banner-location-word"] = "你要去哪裡玩?";
            ViewData["banner-date-word"] = "出發日期";
            ViewData["collapse-search"] = "你要去哪裡玩?";


            var getLocations = _locationServices.GetLocation();
            var getLocation = getLocations.ProductLocationList.ToList();
            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;

            var classFilters = _classifyService.GetClassifyFilters();


            var result = new LocationVM()
            {
                ProductLocationList = getLocation.Where(x => x.CategoryId == 13).Select(lo => new LocationVM.ProductLocation
                {
                    ProductId = lo.ProductId,
                    LocationId = lo.LocationId,
                    CityId = lo.CityId,
                    Price = lo.Price,
                    IsIsland = lo.IsIsland,
                    Longitude = lo.Longitude,
                    Latitude = lo.Latitude,
                    ProductName = lo.ProductName,
                    Path = lo.Path,
                    RatingStar = (float)Math.Floor(lo.RatingStar * 10000) / 10000,

                }).ToList(),

                AllFilters = new FilterVM
                {
                    CityFilterList = classFilters.CityFilterList, //空
                    CategoryFilterList = classFilters.CategoryFilterList,
                },
            };


            return View(result);
        }
        public IActionResult Rent([FromQuery] List<string> typeDate, int id) //租車 分類
        {
            ViewData["banner-h2"] = "租車推薦 即刻預訂享折扣・輕鬆享受自駕遊";
            ViewData["banner-p"] = "多元的租車商品與Today獨家優惠，讓你的自駕遊，安全輕鬆沒煩惱！";
            ViewData["banner-img"] = "https://cdn.kkday.com/pc-web/assets/img/car_rentals/car-rentals-banner.jpg";
            ViewData["banner-location-word"] = "取車地點";
            ViewData["banner-date-word"] = "取車日期";
            ViewData["collapse-search"] = "請選擇取車地點及日期";

            var userId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0;

            var classFilters = _classifyService.GetClassifyFilters();


            var getLocations = _locationServices.GetLocation();
            var getLocation = getLocations.ProductLocationList.ToList();
            var result = new LocationVM()
            {
                ProductLocationList = getLocation.Where(x=>x.CategoryId==42).Select(lo => new LocationVM.ProductLocation
                {
                    ProductId = lo.ProductId,
                    LocationId = lo.LocationId,
                    CityId = lo.CityId,

                    IsIsland = lo.IsIsland,
                    Price = lo.Price,
                    Longitude = lo.Longitude,
                    Latitude = lo.Latitude,
                    ProductName = lo.ProductName,
                    Path = lo.Path,
                    RatingStar = (float)Math.Floor(lo.RatingStar * 10000) / 10000,
                    CategoryId =lo.CategoryId,

                }).ToList(),
                AllFilters = new FilterVM
                {
                    CityFilterList = classFilters.CityFilterList, //空
                    CategoryFilterList = classFilters.CategoryFilterList,
                }
            };


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
