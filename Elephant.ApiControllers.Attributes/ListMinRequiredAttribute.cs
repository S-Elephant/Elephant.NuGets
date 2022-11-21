using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Elephant.ApiControllers.Attributes
{
    /// <summary>
    /// Validation attribute to check the <see cref="IList"/> contains at least the specified amount of items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ListMinRequiredAttribute : ListMinAttribute
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minLength">Minimum length. Must be equal or greater than 0.</param>
        public ListMinRequiredAttribute(int minLength) :
            base(minLength)
        {
        }

        /// <summary>
        /// Validate that the <see cref="IList"/> contains at least the specified amount of items.
        /// If the value is null and the MinLength is 0, then the validation also passes.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if the minimum length is at least MinLength and if its not null.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult($"List must contain at least {MinLength} items and cannot be NULL. Actual: NULL.");

            return base.IsValid(value, validationContext);
        }
    }
}
