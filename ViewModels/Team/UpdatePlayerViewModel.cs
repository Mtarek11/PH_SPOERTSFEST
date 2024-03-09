using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UpdatePlayerViewModel
    {
        [Required]
        public string PlayerId { get; set; }
        [NationalIdValidation]
        public string NationalId { get; set; }
        [Required]
        public string Name { get; set; }
        [PhoneNumberValidation]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [AllowedImageExtensions]
        public IFormFile NationalIdImage { get; set; }
        public string NationalIdImageUrl { get; set; }
        [AllowedImageExtensions]
        public IFormFile PersonalImage { get; set; }
        public string PersonalImageUrl { get; set; }
    }
}
