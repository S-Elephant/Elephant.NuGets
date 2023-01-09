using System.ComponentModel.DataAnnotations;

namespace Elephant.DataAnnotations
{
    /// <summary>
    /// Validation attribute to check the value is numeric and not null and greater than zero.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GreaterThanZeroRequiredAttribute : GreaterThanZeroAttribute
    {
        /// <summary>
        /// Validate that the <paramref name="value"/> is numeric and not null and greater than zero.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if it is greater than zero and if it is not null.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(DataAnnotationConstants.ValidationNullErrorMessage);

            return base.IsValid(value, validationContext);
        }
    }
}
