using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml;
using Microsoft.AspNetCore.Http;

namespace Elephant.ApiControllers.Attributes
{
    /// <summary>
    /// Validate <see cref="IFormFile"/> file signature.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileSignatureAttribute : ValidationAttribute
    {
        /// <summary>
        /// Data container class.
        /// </summary>
        [DebuggerDisplay("{Extension}, {ContentType}")]
        private class Data
        {
            /// <summary>
            /// File extension.
            /// </summary>
            public string Extension { get; set; }

            /// <summary>
            /// File content type.
            /// </summary>
            public string ContentType { get; set; }

            /// <summary>
            /// File signature matching.
            /// </summary>
            public List<byte[]> Signature { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public Data(string extension, string contentType, List<byte[]> signature)
            {
                Extension = extension;
                ContentType = contentType;
                Signature = signature;
            }
        }

        #region Constants

        /// <summary>
        /// SVG node name to search for to ensure that the file is an SVG file.
        /// </summary>
        private const string RequiredSvgNodeName = "svg";

        /// <summary>
        /// SVG extension in all-lower-case and including the dot.
        /// </summary>
        private const string FullSvgExtension = ".svg";

        /// <summary>
        /// Dot as char.
        /// </summary>
        private const char DotChar = '.';

        /// <summary>
        /// Dot as string.
        /// </summary>
        private const string DotString = ".";

        #endregion

        /// <summary>
        /// File signature data.
        /// For more info see: https://en.wikipedia.org/wiki/List_of_file_signatures,
        /// https://web.archive.org/web/20220804073058/https://filesignatures.net/index.php?page=all and
        /// https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types.
        /// </summary>
        /// <remarks>
        /// Note that some bytes in <see cref="FileSignatureData"/> are shorter than shown on WikiPedia. This is because some editors use different bytes at the end
        /// and it would cause this <see cref="FileSignatureAttribute"/> to wrongly invalidate those files.
        /// </remarks>
        private static readonly List<Data> FileSignatureData = new()
        {
            new(".doc", "application/msword", new() { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } }),
            new(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", new() { new byte[] { 0x50, 0x4B, 0x03, 0x04 } }),
            new(".gif", "image/gif", new() { new byte[] { 0x47, 0x49, 0x46, 0x38 } }),
            new(".ico", "image/vnd.microsoft.icon", new() { new byte[] { 0x00, 0x00, 0x01, 0x00 } }),
            new(".jpeg", "image/jpeg", new() { new byte[] { 0xFF, 0xD8, 0xFF } }),
            new(".jpg", "image/jpeg", new() { new byte[] { 0xFF, 0xD8, 0xFF } }),
            new(".rtf", "application/rtf", new() { new byte[] { 0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31 } }),
            new(".pdf", "application/pdf", new() { new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D } }),
            new(".png", "image/png", new() { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } }),
            new(".svg", "image/svg+xml", new()), // Don't use byte-signature, instead, validate using SVG XML reader instead.
            new(".tif", "image/tiff", new() { new byte[] { 0x49, 0x49, 0x2A, 0x00 }, new byte[] { 0x4D, 0x4D, 0x00, 0x2A } }),
            new(".tiff", "image/tiff", new() { new byte[] { 0x49, 0x49, 0x2A, 0x00 }, new byte[] { 0x4D, 0x4D, 0x00, 0x2A } }),
            new(".txt", "text/plain", new()), // Don't use byte-signature because an empty plain text file has no bytes.
        };

        /// <summary>
        /// String containing all currently accepted allowed file extensions.
        /// </summary>
        private static string _acceptedExtensions = FileSignatureData
            .Select(x => x.Extension)
            .Aggregate((current, next) => current + ", " + next);

        /// <summary>
        /// Contains all (all-lower-case) file extensions that are allowed. Any file that does not have an extension on this list will always fail validation, no matter what.
        /// </summary>
        private readonly string[] _allowedFileExtensions;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="allowedFileExtensions">Allowed file extensions (all-lower-case, with or without the leading dot). Case-insensitive.</param>
        /// <exception cref="ArgumentException">Thrown if an allowed extension isn't defined in <see cref="FileSignatureData"/>.</exception>
        public FileSignatureAttribute(params AllowedFileExtensionType[] allowedFileExtensions)
        {
            _allowedFileExtensions = allowedFileExtensions.Select(allowedFileExtension => DotString + allowedFileExtension.ToString().ToLowerInvariant()).ToArray(); ;

            foreach (string allowedFileExtension in _allowedFileExtensions)
            {
                if (FileSignatureData.All(x => x.Extension != allowedFileExtension))
                    throw new IndexOutOfRangeException($"Requested allowed extension \"{allowedFileExtension}\" is not defined in signature list. Currently accepted extensions: {_acceptedExtensions}.");
            }
        }

        /// <summary>
        /// Check if the specified file (<paramref name="value"/>) has a valid signature matching it's extension.
        /// </summary>
        /// <param name="value">File to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if valid.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile? file = value as IFormFile;

            // If there's no data then it must be invalid.
            if (file == null)
                return new ValidationResult(file == null ? "No data and no filename" : $"{file.FileName} contains no data.");

            // Get extension.
            string? extensionLower = Path.GetExtension(file.FileName)?.ToLowerInvariant();

            // If there's no extension then the data can only be invalid.
            if (string.IsNullOrWhiteSpace(extensionLower))
                return new ValidationResult($"Invalid extension or no extension.");

            // If we don't have any data about the extension that we have, then we consider it to be invalid.
            if (!_allowedFileExtensions.Contains(extensionLower))
                return new ValidationResult($"Unknown extension. Got \"{extensionLower}\".");

            if (extensionLower == FullSvgExtension)
            {
                try
                {
                    using (XmlReader xmlReader = XmlReader.Create(file.OpenReadStream()))
                    {
                        // MoveToContent() skips to passed: <?xml version="1.0" encoding="UTF-8"?>
                        if (xmlReader.MoveToContent() != XmlNodeType.Element || !RequiredSvgNodeName.Equals(xmlReader.Name, StringComparison.OrdinalIgnoreCase))
                            return new ValidationResult("Invalid SVG file.");
                    }
                }
                catch
                {
                    return new ValidationResult("Invalid SVG file.");
                }
            }
            else
            {
                // Note that First() should never crash because we check its existence already in the constructor.
                Data selectedSignature = FileSignatureData.First(x => x.Extension == extensionLower);

                // Try-catch is required because requesting file.ContentType can throw an internal null reference exception.
                try
                {
                    if (file.ContentType == null)
                        return new ValidationResult("File has no content type.");
                }
                catch
                {
                    return new ValidationResult("File has no content type.");
                }

                if (selectedSignature.ContentType != file.ContentType)
                    return new ValidationResult($"Content type \"{file.ContentType}\" doesn't match the extension \"{extensionLower}\".");

                // If there's no byte data for the extension then always return success.
                if (!selectedSignature.Signature.Any())
                    return ValidationResult.Success;

                using (BinaryReader reader = new(file.OpenReadStream()))
                {
                    byte[] headerBytes = reader.ReadBytes(selectedSignature.Signature.Max(x => x.Length));

                    if (!selectedSignature.Signature.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature)))
                        return new ValidationResult("Invalid file signature");
                }
            }

            return ValidationResult.Success;
        }
    }
}
