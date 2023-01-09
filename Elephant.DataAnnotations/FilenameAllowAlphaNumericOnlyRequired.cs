using System.ComponentModel.DataAnnotations;

namespace Elephant.DataAnnotations
{
    /// <summary>
    /// Validation attribute to check if the <see cref="string"/> or <see cref="int"/>
    /// is not null and only contains alphanumeric (= the numbers 0-9 and letters A-Z (both uppercase and lowercase)) characters
    /// and the numeric value (if any) must be equal or greater than zero.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FilenameAllowAlphaNumericOnlyRequired : FilenameAllowAlphaNumericOnly
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public FilenameAllowAlphaNumericOnlyRequired()
        {
        }

        /// <summary>
        /// Validate that the <paramref name="value"/> is not null and only contains alphanumeric (= the numbers 0-9 and letters A-Z (both uppercase and lowercase)) characters
        /// and the numeric value (if any) must be equal or greater than zero.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if the <paramref name="value"/> is not null and contains only alphanumeric characters and the numeric value (if any) must be equal or greater than zero..</returns>
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            // Disallow null.
            if (value == null)
                return new ValidationResult(DataAnnotationConstants.ValidationNullErrorMessage);

            return base.IsValid(value, validationContext);
        }
    }
}
