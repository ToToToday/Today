using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Today.Web.DTOModels.ClassifyDTO;
using Today.Web.DTOModels.ProductDTO;
using Today.Web.Services.ClassifyService;
using Today.Web.ViewModels;
using Today.Model.Repositories;
using System.Threading.Tasks;
using Today.Model.Models;

namespace Today.Web.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassifyApiController : ControllerBase
    {
        private readonly IClassifyService _classifyService;
        private readonly IGenericRepository _repo;

        public ClassifyApiController(IClassifyService classifyService, IGenericRepository repo)
        {
            _classifyService = classifyService;
            _repo = repo;
        }

        //[HttpPost]
        //public IActionResult searchCity(string searchword)
        //{
        //    var searchWord = new ProductDTO.ProductRequestDTO
        //    {
        //        CityName = searchword
        //    };
        //    ViewData
        //}
        //[HttpPost]

        //public ActionResult pagedlist(int page = 1, int pageSize = 10)
        //{
        //    var model = _pagedList.AsQueryable();
        //    var result = model.OrderBy(x => x.id).ToPagedList(page, pageSize);
        //    return View(result);
        //}
        //public async Task<IActionResult> GetPages(int? page = 1, object ViewData = null)
        //{
        //    //每頁幾筆
        //    const int pageSize = 10;    
        //    //處理頁數
        //    ViewData.Product = GetPagedProcess(page, pageSize);
        //    //填入頁面資料
        //    return Ok(_repo.Product.Skip<Product>(pageSize * ((page ?? 1) - 1)).Take(pageSize).ToListAsync());
        //}

        [HttpPost]
        public IActionResult Classify([FromBody] ClassifyRequestModel c)
        {
            var inputDto = new FilterDTO
            {
                CategoryFilterList = c.Categories,
                //c.Categories.Select(x=> new FilterDTO.CategoryFilter { CategoryId = x}).ToList(),
                CityFilterList = c.Cities,
                //c.Cities.Select(x => new FilterDTO.CityFilter { CityId = x}).ToList(),
                //擴充其他條件...

                SortBy = c.SortBy,
                DateRange = c.DateRange,
                Page = c.Page,

                IsOffIsland=c.IsOffIsland,
                IsDIY=c.IsDIY,
                IsHSR=c.IsHSR,
                IsParent=c.IsParent,
                IsRent=c.IsRent,
                city_choosed = c.City_choosed,
                OffCityName = c.offCityName,
                typeBanner=c.typeBanner,
                MemberId = (User.Identity.Name != null) ? int.Parse(User.Identity.Name) : 0
            };

            var classifyCardList = _classifyService.GetClassifyMatchedCards(inputDto);

            var result = new ClassifyVM()
            {
                ClassifyCardList = classifyCardList.ClassifyCardList,
                CardCount = classifyCardList.CardCount,
            };

           return Ok(result);
        }
    }
}

