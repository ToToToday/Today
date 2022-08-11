namespace Today.Web.DTOModels.AccountDTO
{
    public class CreateAccountInputDTO
    {
        //^ 註冊必備
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class CreateAccountOutputDTO : BaseOutputDTO
    {
    }

    //public class CreateAccountOutputDTO
    //{
    //    public bool IsSuccess { get; set; } //true / false
    //    public string Message { get; set; } //null / 人看的錯誤訊息
    //}
}
