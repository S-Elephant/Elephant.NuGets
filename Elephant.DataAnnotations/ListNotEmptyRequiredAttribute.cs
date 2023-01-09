using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Elephant.DataAnnotations
{
    /// <summary>
    /// Validator for validating that the specific <see cref="IList"/> is not null and has at least one item.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ListNotEmptyRequiredAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validate that the specific <see cref="IList"/> is not null and has at least one item (is not empty).
        /// </summary>
        /// <param name="value">Value to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if the <see cref="IList"/> is not empty and isn't null.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("List must contain at least one item. Actual: NULL.");

            if (value is IList list)
            {
                if (list.Count == 0)
                    return new ValidationResult($"List must contain at least one item. Actual: {list.Count}.");

                return ValidationResult.Success;
            }

            return new ValidationResult("Unknown type.");
        }
    }
}