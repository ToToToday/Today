namespace Today.Web.DTOModels.AccountDTO
{
    public class LoginAccountInputDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginAccountOutputDTO : BaseOutputDTO
    {
    }

    //public class LoginAccountOutputDTO
    //{
    //    public bool IsSuccess { get; set; } //true / false
    //    public string Message { get; set; } //null / 人看的錯誤訊息
    //}

    public class Login3rdPartyInputDto
    {
        public string Provider { get; set; }
        public string NameIdentifier { get; set; }
    }
    public class Login3rdPartyOutputDto : BaseOutputDTO
    { }
}
