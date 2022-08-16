using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Today.Model.Models;
using TodayMVC.Admin.DTOModels;
using TodayMVC.Admin.Repositories.DapperMemberRepositories;
using TodayMVC.Admin.ViewModels;
using static TodayMVC.Admin.ViewModels.MemberVM;

namespace TodayMVC.Admin.Services.MemberService
{
    public class MemberService : IMemberService
    {
        private readonly IDapperMemberRepository _dapperRepo;
        public MemberService(IDapperMemberRepository dapperRepo)
        {
            _dapperRepo = dapperRepo;
        }

        public MemberVM GetAllMemberList()
        {
            var dataSource = _dapperRepo.SelectAll();
            var result = new MemberVM { MemberList = dataSource.Select(r => new MemberInfo { MemberId = r.MemberId, MemberName = r.MemberName, CityName = r.City.CityName, Age = r.Age, Phone = r.Phone, Gender = r.Gender, Email = r.Email}).ToList()};
            return result;
        }
    }
}
