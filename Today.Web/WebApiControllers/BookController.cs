using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Today.Web.Models.ShopCartAPI;
using Today.Web.Services.BookService;
using Today.Web.ViewModels;

namespace Today.Web.WebApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody]BookVM request)
        {
             _bookService.CreateBook(request);

            try
            {
                return Ok(new APIResult(APIStatus.Success, string.Empty, true));
            }
            catch (Exception ex)
            {
                return Ok(new APIResult(APIStatus.Fail, ex.Message, false));
            }
        }
    }
}
