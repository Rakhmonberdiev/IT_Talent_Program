using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IT_Talent_Program.Helpers
{
    public class NameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? name = value as string;
            if (name != null)
            {
                if (Regex.IsMatch(name, "^[a-zA-Zа-яА-Я]+$"))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("The name can only contain Latin and Russian letters.");
        }
    }
}
