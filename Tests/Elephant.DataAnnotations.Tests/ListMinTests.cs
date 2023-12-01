// ReSharper disable UnusedMember.Local
namespace Elephant.DataAnnotations.Tests
{
    /// <summary>
    /// <see cref="ListMinAttribute"/> tests.
    /// </summary>
    public class ListMinTests
    {
        /// <summary>
        /// <see cref="ListMinAttribute"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(9, false)]
        [InlineData(10, true)]
        [InlineData(11, true)]
        [InlineData(100, true)]
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
            [ListMin(10)]
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
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
        /// <see cref="ListMinAttribute"/> null test.
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
            [ListMin(10)]
            public List<int>? Items { get; set; } = null;
        }

        /// <summary>
        /// <see cref="ListMinAttribute"/> tests.
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
            [ListMin(0)]
            public List<int>? Items { get; set; } = null;
        }

        /// <summary>
        /// <see cref="ListMinAttribute"/> should be invalid if the base type is wrong.
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
            [ListMin(0)]
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