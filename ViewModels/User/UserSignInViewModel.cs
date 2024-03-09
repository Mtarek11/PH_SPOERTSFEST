using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class UserSignInViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [PasswordStructureValidation]
        public string Password { get; set; }
    }
}
