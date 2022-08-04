using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Today.Model.Models;

namespace Today.Model.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly TodayDbContext _context;

        public GenericRepository(TodayDbContext context)
        {
            _context = context;
        }
        public void Create<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Added;
        }
        public void Update<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Modified;

        }
        public void Delete<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Deleted;

        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void SavaChanges()
        {
            
            _context.SaveChanges();
        }


    }
}
