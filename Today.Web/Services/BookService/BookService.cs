using System;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;

namespace Today.Web.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository _repo;
        public BookService(IGenericRepository repo)
        {
            _repo = repo;
        }
    
        public void CreateBook(BookVM request)
        {
            var findIsOrNot = _repo.GetAll<Subscription>().Where(s => s.Email == request.Email).Count();

            if (findIsOrNot == 0)
            {
                var result = new Subscription()
                {
                    Email = request.Email
                };

                try
                {
                    _repo.Create<Subscription>(result);
                    _repo.SavaChanges();
                }
                catch(Exception ex)
                {
                    //return ex.Message;
                }
            }
        }
    }
}
