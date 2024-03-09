using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class UserResetPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email {  get; set; }
        [Required]
        public string Code { get; set; }
        [PasswordStructureValidation]
        public string NewPassword { get; set; }
    }
}
