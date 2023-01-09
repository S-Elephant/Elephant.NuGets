using Elephant.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Elephant.DataAnnotations.Tests
{
    /// <summary>
    /// <see cref="FileSignatureAttribute"/> tests.
    /// </summary>
    public class FileSignatureTests : IDisposable
    {
        #region .png test data
        /// <summary>
        /// Valid .png data with .png extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> ValidPngDataWithPngExtension => new()
        {
            {  new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, "image/png", "Test.png", true },
        };

        /// <summary>
        /// No .png data with .png extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> NoDataWithPngExtension => new()
        {
            {  new byte[] { }, "image/png", "Test.png", false },
        };

        /// <summary>
        /// Valid .png data with .png extension with wrong content type.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> ValidPngDataWithPngExtensionWithBadContentType => new()
        {
            {  new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, "image/foo", "Test.png", false },
        };

        /// <summary>
        /// Valid .png data with a wrong .txt extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> ValidPngDataWithTxtExtension => new()
        {
            {  new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, "image/png", "Test.txt", false },
        };

        /// <summary>
        /// Invalid .png data with .png extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> InvalidPngDataWithPngExtension => new()
        {
            {  new byte[] { 0x80, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, "image/png", "Test.png", false },
        };

        #endregion

        #region .txt test data

        /// <summary>
        /// Valid .txt data with .txt extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> ValidTextDataWithTxtExtension => new()
        {
            {  new byte[] { 0x61, 0x62, 0x63, }, "text/plain", "Test.txt", true },
        };

        /// <summary>
        /// No data with .txt extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> NoTextDataWithTxtExtension => new()
        {
            {  Array.Empty<byte>(), "text/plain", "Test.txt", true },
        };

        #endregion

        #region .svg test data

        /// <summary>
        /// Valid .svg data with .svg extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> ValidSvgDataWithSvgExtension => new()
        {
            {  new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E,
                0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x20, 0x65, 0x6E, 0x63, 0x6F, 0x64, 0x69, 0x6E, 0x67,
                0x3D, 0x22, 0x55, 0x54, 0x46, 0x2D, 0x38, 0x22, 0x3F, 0x3E, 0x0A, 0x3C, 0x73, 0x76, 0x67,
                0x3E, 0x0A, 0x3C, 0x2F, 0x73, 0x76, 0x67, 0x3E }, "image/svg+xml", "Test.svg", true },
        };

        /// <summary>
        /// Invalid .svg data with .svg extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> InvalidSvgDataWithSvgExtension => new()
        {
            {  new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E,
                0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x20, 0x65, 0x6E, 0x63, 0x6F, 0x64, 0x69, 0x6E, 0x67,
                0x3D, 0x22, 0x55, 0x54, 0x46, 0x2D, 0x38, 0x22, 0x3F, 0x3E, 0x0A, 0x3C, 0x00, 0x76, 0x67,
                0x3E, 0x0A, 0x3C, 0x2F, 0x73, 0x70, 0x61, 0x3F }, "image/svg+xml", "Test.svg", false },
        };

        /// <summary>
        /// Valid .svg data with .txt extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> ValidSvgDataWithTxtExtension => new()
        {
            {  new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E,
                0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x20, 0x65, 0x6E, 0x63, 0x6F, 0x64, 0x69, 0x6E, 0x67,
                0x3D, 0x22, 0x55, 0x54, 0x46, 0x2D, 0x38, 0x22, 0x3F, 0x3E, 0x0A, 0x3C, 0x73, 0x76, 0x67,
                0x3E, 0x0A, 0x3C, 0x2F, 0x73, 0x76, 0x67, 0x3E }, "image/svg+xml", "Test.txt", false },
        };

        /// <summary>
        /// Invalid .svg data (but valid XML data ("abc" instead of "svg" tags)) with .svg extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> InvalidSvgDataWithSvgExtensionWithValidXml => new()
        {
            {  new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E,
                0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x20, 0x65, 0x6E, 0x63, 0x6F, 0x64, 0x69, 0x6E, 0x67,
                0x3D, 0x22, 0x55, 0x54, 0x46, 0x2D, 0x38, 0x22, 0x3F, 0x3E, 0x0A, 0x3C, 0x61, 0x62, 0x63,
                0x3E, 0x0A, 0x3C, 0x2F, 0x61, 0x62, 0x63, 0x3E }, "image/svg+xml", "Test.svg", false },
        };

        /// <summary>
        /// Invalid .svg data (has an invalid XML declaration) with .svg extension.
        /// </summary>
        public static TheoryData<byte[], string, string, bool> InvalidSvgData2WithSvgExtensionWithValidXml => new()
        {
            {  new byte[] { 0x3C, 0x3F, 0x61, 0x62, 0x63, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E,
                0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x20, 0x65, 0x6E, 0x63, 0x6F, 0x64, 0x69, 0x6E, 0x67,
                0x3D, 0x22, 0x55, 0x54, 0x46, 0x2D, 0x38, 0x22, 0x3F, 0x3E, 0x0A, 0x3C, 0x73, 0x76, 0x67,
                0x3E, 0x0A, 0x3C, 0x2F, 0x73, 0x76, 0x67, 0x3E }, "image/svg+xml", "Test.svg", true },
        };

        #endregion

        /// <inheritdoc cref="_memoryStream"/>
        private MemoryStream? _memoryStream = null;

        /// <summary>
        /// Create an <see cref="IFormFile"/> mock.
        /// </summary>
        /// <param name="content"><see cref="byte[]"/> contents to write.</param>
        /// <param name="fullFilename">Full filename including extension. Example: "Test.txt".</param>
        /// <remarks>To prevent a "System.ObjectDisposedException : Cannot access a closed Stream.", the <see cref="MemoryStream"/>
        /// <see cref="_memoryStream"/> is not closed until the test finished.
        /// This is different from a normal case where you close and dispose after uploading while the server processes it.</remarks>
        /// <returns>Mocked <see cref="IFormFile"/> with the specified contents and filename.</returns>
        private static FormFile CreateIFormFileMock(MemoryStream? stream, byte[] content, string? contentType, string fullFilename)
        {
            stream = new(content, true);
            FormFile file = new(stream, 0, stream.Length, null, fullFilename)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType,
            };

            return file;
        }

        /// <summary>
        /// <see cref="FileSignatureAttribute"/> non-required test without data.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void ShouldReturnSuccessIfDataIsNull()
        {
            // Arrange.
            ValidationTargetTxtAndPng target = new(null);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.True(isValid);
        }

        /// <summary>
        /// <see cref="FileSignatureAttribute"/> tests for .txt and .png extensions.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [MemberData(nameof(ValidPngDataWithPngExtension), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(NoDataWithPngExtension), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(ValidPngDataWithPngExtensionWithBadContentType), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(ValidPngDataWithTxtExtension), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(InvalidPngDataWithPngExtension), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(ValidTextDataWithTxtExtension), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(NoTextDataWithTxtExtension), MemberType = typeof(FileSignatureTests))]
        public void Validate(byte[] content, string contentType, string fullFilename, bool expectedIsValid)
        {
            // Arrange.
            ValidationTargetTxtAndPng target = new(CreateIFormFileMock(_memoryStream, content, contentType, fullFilename));
            List<ValidationResult> validationResults = new();

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), validationResults, true);

            // Assert.
            if (expectedIsValid)
                Assert.True(isValid, string.Join(',', validationResults));
            else
                Assert.False(isValid, string.Join(',', validationResults));
        }

        /// <summary>
        /// <see cref="FileSignatureAttribute"/> tests for .svg extension.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [MemberData(nameof(ValidSvgDataWithSvgExtension), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(InvalidSvgDataWithSvgExtension), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(ValidSvgDataWithTxtExtension), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(InvalidSvgData2WithSvgExtensionWithValidXml), MemberType = typeof(FileSignatureTests))]
        [MemberData(nameof(InvalidSvgDataWithSvgExtensionWithValidXml), MemberType = typeof(FileSignatureTests))]
        public void ValidateSvg(byte[] content, string contentType, string fullFilename, bool expectedIsValid)
        {
            // Arrange.
            ValidationTargetSvg target = new(CreateIFormFileMock(_memoryStream, content, contentType, fullFilename));
            List<ValidationResult> validationResults = new();

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), validationResults, true);

            // Assert.
            if (expectedIsValid)
                Assert.True(isValid, string.Join(',', validationResults));
            else
                Assert.False(isValid, string.Join(',', validationResults));
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            TearDown();
        }

        /// <summary>
        /// Tear down.
        /// </summary>
        private void TearDown()
        {
            _memoryStream?.Dispose();
        }

        /// <summary>
        /// Test class for <see cref="Validate(string, bool)"/> tests with .txt and .png extensions.
        /// </summary>
        private class ValidationTargetTxtAndPng
        {
            /// <summary>
            /// <see cref="IFormFile"/> to validate.
            /// </summary>
            [FileSignature(AllowedFileExtensionType.Txt, AllowedFileExtensionType.Png)]
            public FormFile? FormFile { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTargetTxtAndPng(FormFile? formFile)
            {
                FormFile = formFile;
            }
        }

        /// <summary>
        /// Test class for <see cref="Validate(string, bool)"/> tests with .svg extension.
        /// </summary>
        private class ValidationTargetSvg
        {
            /// <summary>
            /// <see cref="IFormFile"/> to validate.
            /// </summary>
            [FileSignature(AllowedFileExtensionType.Svg)]
            public FormFile? FormFile { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTargetSvg(FormFile? formFile)
            {
                FormFile = formFile;
            }
        }
    }
}