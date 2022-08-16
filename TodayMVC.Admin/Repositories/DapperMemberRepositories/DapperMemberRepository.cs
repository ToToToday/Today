using Dapper;
using System.Collections.Generic;
using System.Data;
using Today.Model.Models;
using TodayMVC.Admin.Repositories.DapperMemberRepositories;

namespace TodayMVC.Admin.Repositories
{
    public class DapperMemberRepository : DapperBaseRepository, IDapperMemberRepository
    {
        public DapperMemberRepository(IDbConnection conn) : base(conn)
        { }

        public Member GetOne(Member entity)
        {
            return (Member)_conn.Query<Member>(@"SELECT * FROM Member Where MemberId = @MemberId", new { entity.MemberId });
        }

        public IEnumerable<Member> SelectAll()
        {
            var sql = @"SELECT MemberId, MemberName, m.CityId, Age, Phone, Gender, Email, c.CityId split_on, c.CityName FROM Member m LEFT JOIN City c ON m.CityId = c.CityId";

            return _conn.Query<Member, City, Member>(sql, (m, c) => { m.City = c; return m; }, splitOn: "split_on");

            //return _conn.Query<Member>(@"
            //        SELECT MemberId, MemberName, CityId, Age, Phone, Gender, Email FROM Member
            //    ");
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
