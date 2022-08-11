using Today.Web.ViewModels;

namespace Today.Web.Services.BookService
{
    public interface IBookService
    {
        void CreateBook(BookVM request);
    }
}
