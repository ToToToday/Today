using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Today.Model.Repositories
{
    public interface IGenericRepository
    {
        EntityEntry<T> Create<T>(T value) where T : class;
        void Update<T>(T value) where T : class;
        void Delete<T>(T value) where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        void SavaChanges();
    }
}
