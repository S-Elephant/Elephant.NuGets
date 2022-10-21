namespace Elephant.ApiControllers.Attributes.Tests
{
    /// <summary>
    /// <see cref="ListNotEmptyAndRequiredAttribute"/> tests.
    /// </summary>
    public class ListNotEmptyAndRequiredTests
    {
        /// <summary>
        /// <see cref="ListNotEmptyAndRequiredAttribute"/> tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void IsInvalidIfIsNull()
        {
            // Arrange.
            ValidationTargetNull target = new();

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.False(isValid);
        }

        /// <summary>
        /// Test class for <see cref="IsInvalidIfIsNull"/> tests.
        /// </summary>
        private class ValidationTargetNull
        {
            /// <summary>
            /// Items to validate.
            /// </summary>
            [ListNotEmptyAndRequired]
            public List<int>? Items { get; set; } = null;
        }

        /// <summary>
        /// <see cref="ListNotEmptyAndRequiredAttribute"/> tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void IsInvalidIfHasZeroItems()
        {
            // Arrange.
            ValidationTargetZeroItems target = new();

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.False(isValid);
        }

        /// <summary>
        /// Test class for <see cref="IsInvalidIfHasZeroItems"/> tests.
        /// </summary>
        private class ValidationTargetZeroItems
        {
            /// <summary>
            /// Items to validate.
            /// </summary>
            [ListNotEmptyAndRequired]
            public List<int>? Items { get; set; } = new();
        }

        /// <summary>
        /// <see cref="ListNotEmptyAndRequiredAttribute"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(20)]
        public void IsValidIfHasAtLeastOneItem(int listSize)
        {
            // Arrange.
            ValidationTargetWithItems target = new(listSize);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.True(isValid);
        }

        /// <summary>
        /// Test class for <see cref="IsValidIfHasAtLeastOneItem(int)"/> tests.
        /// </summary>
        private class ValidationTargetWithItems
        {
            /// <summary>
            /// Items to validate.
            /// </summary>
            [ListNotEmptyAndRequired]
            public List<int>? Items { get; set; } = new();

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTargetWithItems(int listSize)
            {
                Items = new List<int>(new int[listSize]);
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
            [ListNotEmptyAndRequired]
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