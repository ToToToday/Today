using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using static Today.Web.DTOModels.MemberDTO.MemberDTO;

namespace Today.Web.Services.MemberService
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository _repo;
        public MemberService(IGenericRepository repo)
        {
            _repo = repo;
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
                .Select(m => new MemberResponseDTO { MemberName=m.MemberName, CityId=m.CityId, Age=m.Age, Phone=m.Phone, IdentityCard=m.IdentityCard, Gender=m.Gender, Email=m.Email})
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
                //IsSuccess = false,
                MemberId = input.MemberId,
                MemberName = input.MemberName,
                CityId = input.CityId,
                Age = input.Age,
                Phone = input.Phone,
                IdentityCard = input.IdentityCard,
                Gender = input.Gender,
            };

            var user = _repo.GetAll<Member>().SingleOrDefault(u => u.MemberId == input.MemberId);
            user.MemberId = input.MemberId;
            user.MemberName = input.MemberName;
            user.CityId = input.CityId;
            user.Age = input.Age;
            user.Phone = input.Phone;
            user.IdentityCard = input.IdentityCard;
            user.Gender = input.Gender;

            try
            {
                _repo.Update<Member>(user);
                _repo.SavaChanges();
                test.Code = 0;
                test.IsSuccess = true;
                test.Data = result;
               
            }
            catch(Exception ex)
            {
                test.Code = 404;
                test.Message = ex.Message;
            }

            return test;
        }
    }
}
