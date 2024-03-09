using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class NationalIdValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }
            string nationalId = value.ToString();
            if (nationalId.Length != 14 || !nationalId.All(char.IsDigit))
            {
                return new ValidationResult("The National ID must contain exactly 14 digits.");
            }
            return ValidationResult.Success;
        }
    }
}

