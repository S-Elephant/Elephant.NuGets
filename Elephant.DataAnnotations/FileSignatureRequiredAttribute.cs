using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Elephant.DataAnnotations
{
    /// <summary>
    /// Validate <see cref="IFormFile"/> file signature.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileSignatureRequiredAttribute : FileSignatureAttribute
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="allowedFileExtensions">Allowed file extensions (all-lower-case, with or without the leading dot). Case-insensitive.</param>
        public FileSignatureRequiredAttribute(params AllowedFileExtensionType[] allowedFileExtensions) :
            base(allowedFileExtensions)
        {
        }

        /// <summary>
        /// Check if the specified file (<paramref name="value"/>) has a valid signature matching it's extension.
        /// </summary>
        /// <param name="value">File to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if the file-signature matches the file content and the extension. Also returns true if it is null.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile? file = value as IFormFile;

            // If there's no data then it must be invalid.
            if (file == null)
                return new ValidationResult($"{DataAnnotationConstants.ValidationNullErrorMessage} Expected one of these extensions: {string.Join(',', AllowedFileExtensions)}.");

            return base.IsValid(value, validationContext);
        }
    }
}
