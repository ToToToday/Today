using Today.Model.Models;
using Today.Web.Models.DTOModels.Account;

namespace Today.Web.Services.AccountService
{
    public interface IAccountService
    {
        CreateAccountOutputDTO CreateAccount(CreateAccountInputDTO input); //創建賬戶
        LoginAccountOutputDTO LoginAccount(LoginAccountInputDTO input); //登錄帳號
        void LogoutAccount(); //登出帳號


        //一開始通常先想到這個(常用的通用方法)
        bool IsExistAccount(string Email); //是否存在帳戶
        Member FindAccountOrNull(string Email); //查找帳戶或空值
        //Member FindAccountOrNull(int memberId); //(不需要)


        //void SendVerifyMail(string email); //發送驗證郵件(不需要)
        //void VerifyAccount(int memberId); //驗證賬戶(不需要)

    }
}
