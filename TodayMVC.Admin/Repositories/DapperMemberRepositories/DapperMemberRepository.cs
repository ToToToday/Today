using Dapper;
using System.Collections.Generic;
using System.Data;
using Today.Model.Models;
using TodayMVC.Admin.Repositories.DapperMemberRepositories;

namespace TodayMVC.Admin.Repositories
{
    public class DapperMemberRepository : DapperBaseRepository, IDapperMemberRepository
    {
        public DapperMemberRepository(IDbConnection conn) : base(conn){ }
        public IEnumerable<Member> GetOne(Member entity)
        {
            return _conn.Query<Member>(@"SELECT * FROM Member Where MemberId = @MemberId", new { entity.MemberId });
        }
        public IEnumerable<Member> SelectAll()
        {
            return _conn.Query<Member>(@"
                    SELECT * FROM Member
                ");
        }
        public int Delete(Member entity)
        {
            return _conn.Execute(@"
                    DELETE FROM Member
                    WHERE MemberId = @MemberId
                ",
            new { entity.MemberId });
        }
        public int Create(Member entity)
        {
            throw new System.NotImplementedException();
        }

        public int Update(Member entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
