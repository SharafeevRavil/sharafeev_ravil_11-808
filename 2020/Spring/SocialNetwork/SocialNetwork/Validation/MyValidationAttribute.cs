using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Validation
{
    public abstract class MyValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return new ValidationResult(ErrorMessage);
        }
        public new string ErrorMessage { get; set; } = "Value is incorrect";
    }
}