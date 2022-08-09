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

namespace Today.Web.Controllers
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

        public class ClassifyRequestModel
        {
            public List<int> Cities { get; set; }
            public List<int> Categories { get; set; }
            public int Page { get; set; }

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

        //protected IPagedList<Product> GetPagedProcess(int? page, int pageSize)
        //{
        //    // 過濾從client傳送過來有問題頁數
        //    if (page.HasValue && page < 1)
        //        return null;
        //    // 從資料庫取得資料
        //    var listUnpaged = GetStuffFromDatabase();
        //    IPagedList<Product> pagelist = listUnpaged.ToPagedList(page ?? 1, pageSize);
        //    // 過濾從client傳送過來有問題頁數，包含判斷有問題的頁數邏輯
        //    if (pagelist.PageNumber != 1 && page.HasValue && page > pagelist.PageCount)
        //        return null;
        //    return pagelist;
        //}
        //protected IQueryable<Product> GetStuffFromDatabase()
        //{
        //    return _repo.product;
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
                Page = c.Page,
                MemberId = int.Parse(User.Identity.Name)
            };

            var classifyCardList = _classifyService.GetClassifyMatchedProducts(inputDto);

            return Ok(classifyCardList);
        }

    }
}

