using Elephant.Constants.SqlServer;
using System.ComponentModel.DataAnnotations;

namespace Elephant.DataAnnotations.SqlServer
{
    /// <summary>
    /// Validation attribute to check if the <see cref="string"/> length is between <see cref="PathFolderMaxLengthAttribute.MinLength"/> (defaults to 0) and <see cref="DbTypes.FolderPath"/>
    /// and also checks if it is not null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PathFolderMaxLengthRequiredAttribute : PathFolderMaxLengthAttribute
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minLength">Minimum length. Must be equal or greater than 0.</param>
        public PathFolderMaxLengthRequiredAttribute(int minLength = 0)
            : base(minLength)
        {
        }

        /// <summary>
        /// Validate that the <paramref name="value"/> is not null and is between <see cref="PathFolderMaxLengthAttribute.MinLength"/> and <see cref="DbTypes.FolderPath"/>.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if the minimum length is at least <see cref="PathFolderMaxLengthAttribute.MinLength"/> and if it is not null.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Disallow null.
            if (value == null)
                return new ValidationResult($"Value cannot be null.");

            // Process base.
            ValidationResult baseBValidationResult = base.IsValid(value, validationContext);
            if (baseBValidationResult != ValidationResult.Success)
                return baseBValidationResult;

            return ValidationResult.Success;
        }
    }
}
