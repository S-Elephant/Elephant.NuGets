using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Elephant.ApiControllers.Attributes
{
    /// <summary>
    /// Validation attribute to check the <see cref="IList"/> is null or contains no more than the specified amount of items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ListMaxAttribute : ValidationAttribute
    {
        /// <summary>
        /// Maximum length.
        /// </summary>
        private readonly int _maxLength;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxLength">Maximum length.</param>
        public ListMaxAttribute(int maxLength)
        {
            _maxLength = maxLength;
        }

        /// <summary>
        /// Validate that the <see cref="IList"/> is null or contains no more than the specified amount of items.
        /// If the specified amount of items equals 0 and the list itself is null, then the validation also passes.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if valid.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is IList list)
            {
                if (list.Count > _maxLength)
                    return new ValidationResult($"List must contain no more than {_maxLength} items. Actual: {list.Count}.");
         
                return ValidationResult.Success;
            }

            return new ValidationResult("Unknown type.");
        }
    }
}
