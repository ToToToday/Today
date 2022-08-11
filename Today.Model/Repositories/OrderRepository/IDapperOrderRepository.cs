using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Today.Model.Repositories.OrderRepository
{
    public interface IDapperOrderRepository<T> where T : class
    {
		/// <summary>
		/// C，回傳受影響資料列數
		/// </summary>
		int Create(T entity);

		/// <summary>
		/// U，回傳受影響資料列數
		/// </summary>
		int Update(T entity);

		/// <summary>
		/// D，回傳受影響資料列數
		/// </summary>
		int Delete(T entity);

		/// <summary>
		/// R全部(多筆)
		/// </summary>
		IEnumerable<T> SelectAll();

		/// <summary>
		/// 用ID R單筆
		/// </summary>
		T SelectById(int id);
	}
}
