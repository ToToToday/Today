namespace Today.Web.Models.DTOModels.Account
{
    public class CreateAccountInputDTO
    {
        //^ 註冊必備
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordCheck { get; set; }
    }

    public class CreateAccountOutputDTO : BaseOutputDTO
    {
        
    }
}
