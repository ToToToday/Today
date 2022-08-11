using System.Data;

namespace TodayMVC.Admin.Repositories
{
    public class DapperBaseRepository
    {
        protected readonly IDbConnection _conn;
        protected DapperBaseRepository(IDbConnection conn)
        {
            _conn = conn;
        }
    }
}
