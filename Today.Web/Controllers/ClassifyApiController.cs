using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Today.Web.Services.ClassifyService;
using Today.Web.ViewModels;

namespace Today.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassifyApiController : ControllerBase
    {
        private readonly IClassifyService _classifyService;

        public ClassifyApiController(IClassifyService classifyService)
        {
            _classifyService = classifyService;
        }

        [HttpPost]
        public IActionResult Classify(String searchString)
        {
            //var Classify = _classifyService.

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
                    RatingStar = (int)c.RatingStar,
                    TotalComment = c.TotalComment,
                    Evaluation = c.Evaluation
                }).ToList(),
                CategoryList = Categorysource.Select(x => new ClassifyVM.CategoryDestinations
                {
                    ProductCategoryId = x.ProductCategoryId,
                    CategoryName = x.CategoryName,
                    ChildCategory = x.ChildCategory.Select(y => new ClassifyVM.CategoryDestinations()
                    {
                        ProductCategoryId = y.ProductCategoryId,
                        CategoryName = y.CategoryName
                    }).ToList()
                }).ToList()
            };
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Classify(int id)
        {
            //var Classify = _classifyService.

            return Ok();
        }
    }
}

