using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels.MemberDTO;
using Today.Web.Services.AccountService;
using static Today.Web.DTOModels.MemberDTO.MemberDTO;

namespace Today.Web.Services.MemberService
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository _repo;
        private readonly IAccountService _accountService;
        public MemberService(IGenericRepository repo, IAccountService accountService)
        {
           _repo = repo;
            _accountService = accountService;
        }

        public List<CityRegion> AllCityList()
        {
            var cityList = _repo.GetAll<City>().Select(c => new CityRegion
            {
                CityName = c.CityName,
                CityId = c.CityId,
            }).ToList();

            return cityList;
        }

        public MemberResponseDTO GetMember(MemberRequestDTO MemberId)
        {
            var result = _repo.GetAll<Member>()
                .Where(m => m.MemberId == MemberId.MemberId)
                .Select(m => new MemberResponseDTO { MemberName=m.MemberName, CityId=m.CityId, Age=m.Age, Phone=m.Phone, /*IdentityCard=m.IdentityCard,*/ Gender=m.Gender, Email=m.Email})
                .First();

            return result;
        }

        public ResponseDTO UpdateMember(MemberResponseDTO input)
        {
            var test = new ResponseDTO()
            {
                IsSuccess = false,
                Code = -1,
            };
            var result = new MemberResponseDTO() { 
                MemberId = input.MemberId,
                MemberName = input.MemberName,
                CityId = input.CityId,
                Age = input.Age,
                Phone = input.Phone,
                //IdentityCard = input.IdentityCard,
                Gender = input.Gender,
            };

            var user = _repo.GetAll<Member>().SingleOrDefault(u => u.MemberId == input.MemberId);
            user.MemberId = input.MemberId;
            user.MemberName = input.MemberName;
            user.CityId = input.CityId;
            user.Age = input.Age;
            user.Phone = input.Phone;
            //user.IdentityCard = input.IdentityCard;
            user.Gender = input.Gender;

            try
            {
                _repo.Update<Member>(user);
                _repo.SavaChanges();
                test.Code = 0;
                test.IsSuccess = true;
                test.Data = result;
                test.Message = "更新成功";
            }
            catch (Exception ex)
            {
                test.Code = 404;
                test.Message = ex.Message;
            }

            return test;
        }

        public string GetMemberName(MemberRequestDTO MemberId)
        {
            var result = _repo.GetAll<Member>()
                .Where(m => m.MemberId == MemberId.MemberId)
                .Select(m => m.MemberName)
                .First();

            return result;
        }

        public int GetLongWayName()
        {
            var memberId = _accountService.GetMemberId();
            var result = _repo.GetAll<LoginWay>()
                .Where(l => l.MemberId == memberId)
                .Select(l => l.LongWayName)
                .FirstOrDefault();

            return result == default? 1: result;
        }
    }
}
