using System.Collections.Generic;
using Today.Model.Models;

namespace TodayMVC.Admin.Repositories
{
	public interface IDapperGenericRepository<T> where T : class
	{
		int Create(T entity);

		int Update(T entity);

		int Delete(T entity);

		T GetOne(T entity);

		IEnumerable<T> SelectAll();
    }
}
