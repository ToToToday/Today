namespace Today.Web.DTOModels.AccountDTO
{
    public class LoginAccountInputDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginAccountOutputDTO
    {
        public bool IsSuccess { get; set; } //true / false
        public string Message { get; set; } //null / 人看的錯誤訊息
    }
}
