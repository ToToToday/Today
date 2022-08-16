using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.Services.MemberService
{
    public interface IMemberService
    {
        MemberVM GetAllMemberList();
        int DeleteMember(MemberVM.MemberInfo member);
    }
}
