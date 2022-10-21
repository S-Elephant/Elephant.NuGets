using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Elephant.ApiControllers.Attributes.Tests
{
    /// <summary>
    /// <see cref="FileSizeMaxAttribute"/> tests.
    /// </summary>
    public class FileSizeMaxTests
    {
        private static IFormFile CreateIFormFileMock(string content)
        {
            using MemoryStream stream = new();
            using StreamWriter writer = new(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            return new FormFile(stream, 0, stream.Length, "name", "Mock.txt");
        }

        /// <summary>
        /// <see cref="FileSizeMaxAttribute"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("A", true)]
        [InlineData("Some random string in here as the data.", false)]
        public void Validate(string content, bool expectedIsValid)
        {
            // Arrange.
            ValidationTarget target = new(CreateIFormFileMock(content));

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.Equal(expectedIsValid, isValid);
        }

        /// <summary>
        /// Test class for <see cref="Validate(string, bool)"/> tests.
        /// </summary>
        private class ValidationTarget
        {
            /// <summary>
            /// <see cref="IFormFile"/> to validate.
            /// </summary>
            [FileSizeMax(5)]
            public IFormFile? FormFile { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTarget(IFormFile? formFile)
            {
                FormFile = formFile;
            }
        }

        /// <summary>
        /// <see cref="FileSizeMaxAttribute"/> should be invalid if the base type is wrong.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void IsInvalidIfWrongType()
        {
            // Arrange.
            WrongType target = new();

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.False(isValid);
        }

        /// <summary>
        /// Class that is always the wrong type for validation testing.
        /// </summary>
        private class WrongType
        {
            /// <summary>
            /// Item to validate.
            /// </summary>
            [GreaterThanZeroAndRequired]
            public AlwaysWrong Item { get; set; } = new();
        }

        /// <summary>
        /// Class that is always the wrong type for validation testing.
        /// Used as a property in <see cref="WrongType"/>.
        /// </summary>
        private class AlwaysWrong
        {
        }
    }
}