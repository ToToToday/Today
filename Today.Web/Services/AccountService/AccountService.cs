using Cookie_Auth.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.Models.DTOModels.Account;

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


        //^註冊
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
                result.Message = "Email已存在"; //去DB檢查
                return result;
            }

            //if (input.Password != input.PasswordCheck)
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


        //public void SendVerifyMail(string email) //發送驗證郵件(不需要)
        ////實作介面成員要public
        //{
        //    //從現有帳戶去找MemberId
        //    int? memberId = FindAccountOrNull(email)
        //                    ?.MemberId;
        //    ;

        //    //@逐字字串常值
        //    string body = $@"
        //        <h3>點擊
        //            <a href='https://{_httpContextAccessor.HttpContext.Request.Host.Value}/Account/Verify?VIP_NO={memberId}' target='_blank'>連結</a>
        //            以啟用帳戶
        //        </h3>"; //target='_blank' = 新分頁 方式開連結，還有其他值 _new _top ...


        //    MailHelper.SendMail(
        //        new List<string> { email },
        //        "BS會員註冊驗證",
        //        body
        //    );
        //}

        public bool IsExistAccount(string Email) //是否存在帳戶
        {
            return
                _repo.GetAll<Member>()
                    .Any(m => m.Email == Email);
        }

        
        public Member FindAccountOrNull(string email) //查找帳戶或空值
        {
            return _repo.GetAll<Member>()
                .FirstOrDefault(m => m.Email == email);
        }
        /*
        public Member FindAccountOrNull(int memberId) //(不需要)
        {
            return _repo.GetAll<Member>()
                .FirstOrDefault(m => m.MemberId == memberId);
        }

        public void VerifyAccount(int memberId) //驗證賬戶 //(不需要)
        {
            var member = FindAccountOrNull(memberId);

            //使用者有時沒耐心 會 點多次驗證連結
            if (!member.IsVerify)
            {
                member.IsVerify = true;
                member.UpdateTime = DateTime.UtcNow;

                _repo.Update(member);
                _repo.Save();
            }
        }
        */


        //v登入 登出(讀取的功能，因此不用DB_Model寫入)
        public LoginAccountOutputDTO LoginAccount(LoginAccountInputDTO input) //登錄帳號
        {
            var result = new LoginAccountOutputDTO
            {
                IsSuccess = false,
            };

            var memberFound = FindAccountOrNull(input.Email);
            //檢核 (檢核 欄位以外的邏輯 )
            if( memberFound == null)
            {
                result.Message = "使用者帳號不存在";
                return result;
            }
            //if ( ! memberFound.IsVerify) (不需要)
            //{
            //    result.Message = "使用者帳號尚未驗證";
            //    return result;
            //}
            if ( memberFound.Password !=  Encryption.SHA256(input.Password)  ) //(0630-註冊與cookie驗證，35:50)
            {
                result.Message = "密碼錯誤";
                return result;
            }

            result.IsSuccess = true;

            //======【重點】：發claim聲明後，用指定的scheme(計畫)登入
            //lv1 各項聲明資訊
            List<Claim> claims = new List<Claim>()
            {
                //new Claim( ClaimTypes.NameIdentifier , memberFound.MemberId.ToString()), //三方登入會用到
                new Claim( ClaimTypes.Name , memberFound.MemberId.ToString() ), //有點迷的是，ClaimTypes.Name 這個是代表性的claim...
                new Claim( ClaimTypes.Email , memberFound.Email),
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
            /*await*/
            _httpContextAccessor.HttpContext.SignInAsync(
                //CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties
            );

            return result;
        }


        void LogoutAccount() //登出帳號
        {
            //基本上就是把cookie刪除
            _httpContextAccessor.
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

    }
}
