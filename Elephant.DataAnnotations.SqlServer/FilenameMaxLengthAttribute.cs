using Elephant.Constants.SqlServer;
using System.ComponentModel.DataAnnotations;

namespace Elephant.DataAnnotations.SqlServer
{
    /// <summary>
    /// Validation attribute to check if the <see cref="string"/> length is between <see cref="MinLength"/> (defaults to 0) and <see cref="DbTypes.FolderPath"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FilenameMaxLengthAttribute : MaxLengthAttribute
    {
        /// <summary>
        /// Minimum length.
        /// </summary>
        protected readonly int MinLength;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minLength">Minimum length. Must be equal or greater than 0.</param>
        public FilenameMaxLengthAttribute(int minLength = 0)
            : base(DbLengths.Filename)
        {
            MinLength = Math.Max(0, minLength);
        }

        /// <summary>
        /// Validate that the <paramref name="value"/> is either null or is between <see cref="MinLength"/> and <see cref="DbTypes.FolderPath"/>.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if the minimum length is at least <see cref="MinLength"/> or if it is null.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Allow null.
            if (value == null)
                return ValidationResult.Success;

            // Process base.
            ValidationResult baseBValidationResult = base.IsValid(value, validationContext);
            if (baseBValidationResult != ValidationResult.Success)
                return baseBValidationResult;

            // If MinLength is smaller or equal than zero then it can never be invalidated based on this.
            if (MinLength <= 0)
                return ValidationResult.Success;

            if (value is string stringValue)
                return stringValue.Length < MinLength ? new ValidationResult($"Length must be at least {MinLength}. Actual: {stringValue.Length}.") : ValidationResult.Success;

            return new ValidationResult($"Unknown type. {nameof(PathFolderMaxLengthAttribute)} is only allowed on type of string.");
        }
    }
}
