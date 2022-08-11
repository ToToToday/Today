using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Today.Model.Models;
using TodayMVC.Admin.DTOModels;
using TodayMVC.Admin.Repositories.DapperMemberRepositories;
using static TodayMVC.Admin.DTOModels.MemberDTO;

namespace TodayMVC.Admin.Services.MemberService
{
    public class MemberService : IMemberService
    {
        private readonly IDapperMemberRepository _dapperRepo;
        public MemberService(IDapperMemberRepository dapperRepo)
        {
            _dapperRepo = dapperRepo;
        }

        public MemberDTO GetAllMemberList()
        {
            var result = new MemberDTO { MemberList = _dapperRepo.SelectAll().Select(r => new MemberInfo { MemberId = r.MemberId, MemberName = r.MemberName, Gender = r.Gender, CityName = r.CityId}).ToList()};
            return result;
        }
    }
}
