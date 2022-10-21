using System.ComponentModel.DataAnnotations;

namespace Elephant.ApiControllers.Attributes
{
    /// <summary>
    /// Validation attribute to check the value is numeric and not null and greater than zero.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GreaterThanZeroAndRequiredAttribute : GreaterThanZeroAttribute
    {
        /// <summary>
        /// Validate that the <paramref name="value"/> is numeric and not null and greater than zero.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if valid.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Value is required.");

            return base.IsValid(value, validationContext); 
        }
    }
}
