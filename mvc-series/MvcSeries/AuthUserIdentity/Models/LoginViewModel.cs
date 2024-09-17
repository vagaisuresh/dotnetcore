using System.ComponentModel.DataAnnotations;

namespace AuthUserIdentity.Models
{
    public class LoginViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "The user name field is required.")]
        public string? UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The password field is required.")]
        public string? Password { get; set; }
    }
}