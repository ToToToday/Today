using Dapper;
using System.Collections.Generic;
using System.Data;
using Today.Model.Models;

namespace TodayMVC.Admin.Repositories.DapperMailRepositories
{
    public class DapperMailRepository : DapperBaseRepository, IDapperMailRepository
    {
        public DapperMailRepository(IDbConnection conn) : base(conn)
        { }

        public int Create(Subscription entity)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(Subscription entity)
        {
            throw new System.NotImplementedException();
        }

        public Subscription GetOne(Subscription entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Subscription> SelectAll()
        {
            return _conn.Query<Subscription>(@"SELECT * FROM Subscription");
        }

        public int Update(Subscription entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
