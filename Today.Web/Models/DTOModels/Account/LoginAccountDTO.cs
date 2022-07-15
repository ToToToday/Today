namespace Today.Web.Models.DTOModels.Account
{
    public class LoginAccountInputDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginAccountOutputDTO : BaseOutputDTO
    {
        
    }
}
