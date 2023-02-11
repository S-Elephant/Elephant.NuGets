namespace Elephant.DataAnnotations.Tests
{
    /// <summary>
    /// <see cref="ListMaxAttribute"/> tests.
    /// </summary>
    public class ListMaxTests
    {
        /// <summary>
        /// <see cref="ListMaxAttribute"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(9, true)]
        [InlineData(10, true)]
        [InlineData(11, false)]
        [InlineData(100, false)]
        public void Validate(int listSize, bool expectedIsValid)
        {
            // Arrange.
            ValidationTargetSized target = new (listSize);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.Equal(expectedIsValid, isValid);
        }

        /// <summary>
        /// Test class for <see cref="Validate(int, bool)"/> tests.
        /// </summary>
        private class ValidationTargetSized
        {
            /// <summary>
            /// Items to validate.
            /// </summary>
            [ListMax(10)]
            public List<int>? Items { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTargetSized(int listSize)
            {
                Items = new List<int>(new int[listSize]);
            }
        }

        /// <summary>
        /// <see cref="ListMaxAttribute"/> should return success if the value is null and the size is greater than zero.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void IsValidIfNullAndSizeGreaterThanZero()
        {
            // Arrange.
            ValidationTargetNullWithSize10 target = new ();

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.True(isValid);
        }

        /// <summary>
        /// Test class for <see cref="IsValidIfNullAndSizeGreaterThanZero"/> tests.
        /// </summary>
        private class ValidationTargetNullWithSize10
        {
            /// <summary>
            /// Items to validate.
            /// </summary>
            [ListMax(10)]
            public List<int>? Items { get; set; } = null;
        }

        /// <summary>
        /// <see cref="ListMaxAttribute"/> should return valid if the value is null and the size is zero.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void IsValidIfNullAndSizeIsZero()
        {
            // Arrange.
            ValidationTargetNullWithSize0 target = new ();

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.True(isValid);
        }

        /// <summary>
        /// Test class for <see cref="IsValidIfNullAndSizeIsZero"/> tests.
        /// </summary>
        private class ValidationTargetNullWithSize0
        {
            /// <summary>
            /// Items to validate.
            /// </summary>
            [ListMax(0)]
            public List<int>? Items { get; set; } = null;
        }

        /// <summary>
        /// <see cref="ListMaxAttribute"/> should be invalid if the base type is wrong.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void IsInvalidIfWrongType()
        {
            // Arrange.
            WrongType target = new ();

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
            [ListMax(10)]
            public AlwaysWrong Item { get; set; } = new ();
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