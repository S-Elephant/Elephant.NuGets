using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Elephant.ApiControllers.Attributes
{
    /// <summary>
    /// Validation attribute to check the minimum file size of a IFormFile object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileSizeMinAttribute : ValidationAttribute
    {
        /// <summary>
        /// Minimum file size in bytes.
        /// </summary>
        private readonly int _minFileSize;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minFileSize">Minimum file size in bytes. Must be equal or greater than 0.</param>
        public FileSizeMinAttribute(int minFileSize)
        {
            _minFileSize = Math.Max(0, minFileSize);
        }

        /// <summary>
        /// Validate that the file has a file size equal or smaller than the specified minimum (in bytes).
        /// If the value is null and the <see cref="_minFileSize"/> is 0, then the validation also passes.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if the file size is at least <see cref="_minFileSize"/> bytes or if it is null.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is IFormFile formFile)
            {
                if (formFile.Length < _minFileSize)
                    return new ValidationResult($"Minimum allowed file size: {_minFileSize} bytes. Actual: {formFile.Length}.");

                return ValidationResult.Success;
            }

            return new ValidationResult($"Unknown type.");
        }
    }
}
