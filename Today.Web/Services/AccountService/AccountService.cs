using Cookie_Auth.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels.AccountDTO;

namespace Today.Web.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly GenericRepository _repo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(GenericRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _httpContextAccessor = httpContextAccessor;
        }

        //註冊
        public CreateAccountOutputDTO CreateAccount(CreateAccountInputDTO input)
        {
            var result = new CreateAccountOutputDTO()
            {
                IsSuccess = false,
                Message = null,
            };

            //檢核 (檢核欄位以外的邏輯)
            if (IsExistAccount(input.Email))
            {
                result.Message = "Email已存在或密碼有誤"; //去DB檢查
                return result;
            }

            //if (input.Password != input.Password)
            //{
            //    result.Message = "密碼、確認密碼不相符"; //已經被模型解決了(可省略)
            //    return result;
            //}


            //mapping 成 DB_Model
            var entity = new Member
            {
                //MemberId 識別規格免填
                //^ 使用者填的
                Email = input.Email,
                Password = Encryption.SHA256(input.Password),

                //v 預設規則
                //IsVerify = false, //是否驗證完成(不需要)
                //CreateTime = DateTime.UtcNow, //格林威治時間 UTC+0  //創建時間(不需要)
                //UpdateTime = null,
            };

            //寫入DB
            _repo.Create(entity);
            _repo.SavaChanges();

            //寄信...下午
            //this.SendVerifyMail(input.Email); //(不需要)

            result.IsSuccess = true;
            return result;
        }


        public bool IsExistAccount(string Email) //是否存在帳戶
        {
            return _repo.GetAll<Member>()
                    .Any(m => m.Email == Email);
        }


        public Member FindAccountOrNull(string Email) //查找帳戶或空值
        {
            return _repo.GetAll<Member>()
                .FirstOrDefault(m => m.Email == Email);
        }


        //登入 (讀取的功能，因此不用DB_Model寫入)
        public LoginAccountOutputDTO LoginAccount(LoginAccountInputDTO input) //登錄帳號
        {
            var result = new LoginAccountOutputDTO
            {
                IsSuccess = false,
            };

            var memberFound = FindAccountOrNull(input.Email);
            //檢核 (檢核 欄位以外的邏輯 )
            if (memberFound == null)
            {
                result.Message = "使用者帳號不存在";
                return result;
            }
            if (memberFound.Password != Encryption.SHA256(input.Password)) //(0630-註冊與cookie驗證，35:50)
            {
                result.Message = "帳號不存在或密碼有誤";
                return result;
            }

            result.IsSuccess = true;

            //LoginInByMember(memberFound);  //原本這樣
            _ = LoginInByMember(memberFound);

            return result;
        }
        public async Task<Login3rdPartyOutputDto> LoginAccountAsync(Login3rdPartyInputDto input)
        {
            var result = new Login3rdPartyOutputDto
            {
                IsSuccess = false,
            };

            //以 第三方的provider + nameId，查會員
            Member memberFound;

            int LongWayName = 0;
            if (input.Provider == GoogleDefaults.AuthenticationScheme)
            {
                LongWayName = 3;
            }
            else if (input.Provider == FacebookDefaults.AuthenticationScheme)
            {
                LongWayName = 2;
            }
            else if (input.Provider == "Line")
            {
                LongWayName = 4;
            }

            LoginWay loginWayFound =
            _repo.GetAll<LoginWay>().FirstOrDefault(lw =>
                lw.LongWayName == LongWayName &&
                lw.UniqueId == input.NameIdentifier
            );

            //沒查到 => 代表第一次用 這第三方帳號登入 => 代註冊一個無帳密的Member 
            if (loginWayFound == null)
            {
                //資訊可以從第三方來的資訊預填

                //創建帳號
                var memberEntity = new Member
                {
                    //MemberId 識別規格免填
                    Email = "",
                    Password = "",


                    //註冊時要填的欄位，第三方都不一定會提供 => DB欄位 應null
                };
                //你們實務上 要審慎考慮 信箱 有多少作用、是否是唯一...等

                var e = _repo.Create(memberEntity);
                _repo.SavaChanges();
                memberFound = e.Entity;

                var loginWayEntity = new LoginWay
                {
                    MemberId = memberFound.MemberId,
                    LongWayName = LongWayName,
                    UniqueId = input.NameIdentifier,
                };

                _repo.Create(loginWayEntity);
                _repo.SavaChanges();
            }
            else
            {
                memberFound = _repo.GetAll<Member>()
                    .FirstOrDefault(m => m.MemberId == loginWayFound.MemberId);

            }
            result.IsSuccess = true;
            await LoginInByMember(memberFound);
            return result;
        }

        private async Task LoginInByMember(Member memberFound)
        {
            //======【重點】：發claim聲明後，用指定的scheme(計畫)登入
            //lv1 各項聲明資訊
            List<Claim> claims = new List<Claim>()
            {
                //new Claim( ClaimTypes.NameIdentifier , memberFound.MemberId.ToString()), //三方登入會用到
                new Claim( ClaimTypes.Name , memberFound.MemberId.ToString() ),
                //new Claim( ClaimTypes.Email , memberFound.Email),
            };

            //lv2 用上面的資訊集合，造一個ClaimsIdentity物件。
            //(用多項個資，組出【證件】)
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims
                , CookieAuthenticationDefaults.AuthenticationScheme //指定計畫
            );

            //lv3 用ClaimsIdentity物件，造一個 ClaimsPrincipal聲明主體 物件
            //(證件 造出 【人】的身分概念)
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //用指定的 驗證選項  登入
            AuthenticationProperties authProperties = new AuthenticationProperties
            {
                //舉幾個例，可參考官方文件AuthenticationProperties類別中的屬性
                //AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(111), //有設才不會關掉網頁就自動消失?
                IsPersistent = true,
            };

            //將此ClaimsPrincipal登入。此登入方法，會創造一個cookie
            await _httpContextAccessor.HttpContext.SignInAsync(
                //CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties);

            //HttpContext在controller可直接存取。
            //如果在service層，會需要注入HttpContextAccessor(存取者)才能存取HttpContext
        }

        //登出
        public void LogoutAccount() //登出帳號
        {
            //基本上就是把cookie刪除
            _httpContextAccessor.
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated() //判斷是否登入至網站
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public int GetMemberId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
        }

    }
}
