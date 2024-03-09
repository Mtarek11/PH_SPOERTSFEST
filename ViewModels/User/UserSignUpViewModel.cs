using Microsoft.AspNetCore.Http;
using Models;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class UserSignUpViewModel
    {
        [Required]
        public string FullName { get; set; }
        [PhoneNumberValidationAttribute]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [NationalIdValidation]
        public string NationalId { get; set; }
        [AllowedImageExtensions]
        public IFormFile NationalIdImage {  get; set; }
        [Required, AllowedImageExtensions]
        public IFormFile PersonalImage {  get; set; }
        [PasswordStructureValidation]
        public string Password {  get; set; }
    }
}
