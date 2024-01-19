using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    [NotMapped]
    public class NotEqualAttribute(string otherProperty) : ValidationAttribute
    {
        private readonly string otherProperty = otherProperty;

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyValue = validationContext.ObjectType.GetProperty(otherProperty)?.GetValue(validationContext.ObjectInstance, null);

            if (value != null && value.Equals(otherPropertyValue))
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must not be equal to {otherProperty}.");
            }

            return ValidationResult.Success;
        }
    }
}