using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IT_Talent_Program.Helpers
{
    public class CustomAttribute : ValidationAttribute
    {
        private readonly string _fieldName;
        public CustomAttribute(string fieldName)
        {
            _fieldName = fieldName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? loginparol = value as string;
            if(loginparol != null)
            {
                if(Regex.IsMatch(loginparol, "^[a-zA-Z0-9]+$"))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult($"{_fieldName} can only contain Latin letters and numbers.");
        }
    }
}
