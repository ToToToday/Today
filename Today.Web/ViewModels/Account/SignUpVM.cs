using System;
using System.ComponentModel.DataAnnotations;

namespace Today.Web.ViewModels.Account
{
    public class SignUpVM
    {
        [Required]  //規格
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))] //密碼、確認密碼的比對
        public string PasswordCheck { get; set; }
    } 

    //這是個廢物
    public class GoCheckEmailVM
    {

    }
}
