using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Validation
{
    public class NotEmptyAttribute : MyValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string model = (string) value;
            if (string.IsNullOrEmpty(model))
            {
                var a = base.IsValid(value, validationContext);
                return a;
            }
            return ValidationResult.Success;
        }
    }
}