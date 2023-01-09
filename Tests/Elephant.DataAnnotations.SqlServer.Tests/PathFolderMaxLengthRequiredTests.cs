namespace Elephant.DataAnnotations.SqlServer.Tests
{
    /// <summary>
    /// <see cref="PathFolderMaxLengthRequiredAttribute"/> tests.
    /// </summary>
    public class PathFolderMaxLengthRequiredTests
    {
        /// <summary>
        /// Test class to validate.
        /// </summary>
        private class ValidationTarget
        {
            /// <summary>
            /// Items to validate.
            /// </summary>
            [PathFolderMaxLengthRequired(2)]
            public string? Value { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTarget(string? value)
            {
                Value = value;
            }
        }

        /// <summary>
        /// <see cref="PathFolderMaxLengthRequiredAttribute"/> test without data.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void ShouldReturnFalseIfDataIsNull()
        {
            // Arrange.
            ValidationTarget target = new(null);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.False(isValid);
        }

        /// <summary>
        /// <see cref="PathFolderMaxLengthRequiredAttribute"/> test with data.
        /// </summary>
        [Theory]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("Lorem ipsum./n Line 2.")]
        [SpeedVeryFast, UnitTest]
        public void ShouldReturnTrueIfDataDataIsWithinBoundaries(string data)
        {
            // Arrange.
            ValidationTarget target = new(data);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.True(isValid);
        }

        /// <summary>
        /// <see cref="PathFolderMaxLengthRequiredAttribute"/> test with data.
        /// </summary>
        [Theory]
        [InlineData("a")]
        [InlineData("")]
        [SpeedVeryFast, UnitTest]
        public void ShouldReturnFalseIfDataDataIsTooShort(string data)
        {
            // Arrange.
            ValidationTarget target = new(data);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.False(isValid);
        }

        /// <summary>
        /// <see cref="PathFolderMaxLengthRequiredAttribute"/> test with data.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void ShouldReturnFalseIfDataDataIsTooLong()
        {
            // Arrange.
            ValidationTarget target = new(@"C:\Lorem ipsum dolor sit amet\consectetur adipiscing elit\Aenean dapibus metus vel metus tincidunt molestie\Vivamus velit elit\placerat in iaculis ac\commodo sit amet tortor\Fusce vestibulum pulvinar ullamcorper\Praesent dignissim dui eget posuere venenatis\Etiam volutpat malesuada condimentum\Mauris ultricies arcu vitae tortor tempus.jpg");

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.False(isValid);
        }
    }
}