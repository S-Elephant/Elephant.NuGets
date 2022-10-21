namespace Elephant.ApiControllers.Attributes.Tests
{
    /// <summary>
    /// <see cref="GreaterThanZeroAndRequiredAttribute"/> tests.
    /// </summary>
    public class GreaterThanZeroAndRequiredTests
    {
        /// <summary>
        /// <see cref="GreaterThanZeroAndRequiredAttribute"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(null, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(int.MinValue, false)]
        [InlineData(int.MaxValue, true)]
        public void Validate(int? value, bool expectedIsValid)
        {
            // Arrange.
            ValidationTarget target = new(value);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.Equal(expectedIsValid, isValid);
        }

        /// <summary>
        /// Test class for <see cref="Validate(int?, bool)"/> tests.
        /// </summary>
        private class ValidationTarget
        {
            /// <summary>
            /// Items to validate.
            /// </summary>
            [GreaterThanZeroAndRequired]
            public int? A { get; set; } = 2;

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTarget(int? a)
            {
                A = a;
            }
        }

        /// <summary>
        /// <see cref="GreaterThanZeroAndRequiredAttribute"/> tests.
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