using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Elephant.ApiControllers.Attributes
{
    /// <summary>
    /// Validation attribute to check the <see cref="IList"/> contains at least the specified amount of items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ListMinAttribute : ValidationAttribute
    {
        /// <summary>
        /// Minimum length.
        /// </summary>
        private readonly int _minLength;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minLength">Minimum length. Must be equal or greater than 0.</param>
        public ListMinAttribute(int minLength)
        {
            _minLength = Math.Max(0, minLength);
        }

        /// <summary>
        /// Validate that the <see cref="IList"/> contains at least the specified amount of items.
        /// If the value is null and the <see cref="_minLength"/> is 0, then the validation also passes.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if valid.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                if (_minLength == 0)
                    return ValidationResult.Success;

                return new ValidationResult($"List must contain at least {_minLength} items. Actual: NULL.");
            }

            if (value is IList list)
            {
                if (list.Count < _minLength)
                    return new ValidationResult($"List must contain at least {_minLength} items. Actual: {list.Count}.");

                return ValidationResult.Success;
            }

            return new ValidationResult("Unknown type.");
        }
    }
}
