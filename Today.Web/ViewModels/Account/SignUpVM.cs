using System.ComponentModel.DataAnnotations;

namespace Today.Web.ViewModels.Account
{
    public class SignUpVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
