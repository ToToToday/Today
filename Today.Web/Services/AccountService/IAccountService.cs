using System.Threading.Tasks;
using Today.Model.Models;
using Today.Web.DTOModels.AccountDTO;

namespace Today.Web.Services.AccountService
{
    public interface IAccountService
    {
        CreateAccountOutputDTO CreateAccount(CreateAccountInputDTO input); //創建賬戶
        LoginAccountOutputDTO LoginAccount(LoginAccountInputDTO input); //登錄帳號
        void LogoutAccount(); //登出帳號
        Task<Login3rdPartyOutputDto> LoginAccountAsync(Login3rdPartyInputDto input);


        //一開始通常先想到這個(常用的通用方法)
        /// <summary>
        /// 是否存在帳戶
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        bool IsExistAccount(string Email);
        /// <summary>
        /// 查找帳戶或空值
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        Member FindAccountOrNull(string Email);
        //Member FindAccountOrNull(int memberId); //(不需要)


        //void SendVerifyMail(string email); //發送驗證郵件(不需要)

        //void VerifyAccount(int memberId); //驗證帳號(不需要)


        /// <summary>
        /// 判斷是否登入至網站
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();

        /// <summary>
        /// 取得當前網站會員Id
        /// </summary>
        /// <returns></returns>
        int GetMemberId();
    }
}
