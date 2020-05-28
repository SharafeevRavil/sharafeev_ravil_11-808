using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SocialNetwork.Validation
{
    public class RegularExprAttribute : MyValidationAttribute
    {
        private string _regularExpression;

        public RegularExprAttribute(string regularExpression)
        {
            _regularExpression = regularExpression;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string model = (string) value;
            if (!new Regex(_regularExpression).IsMatch(model))
            {
                return base.IsValid(value, validationContext);
            }

            return ValidationResult.Success;
        }
    }
}