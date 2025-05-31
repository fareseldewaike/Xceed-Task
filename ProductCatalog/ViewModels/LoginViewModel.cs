using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "please enter valid email")]
        [EmailAddress(ErrorMessage ="please enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter valid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
