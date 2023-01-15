using Elephant.DataAnnotations;

namespace Elephant.DataAnnotations.Tests
{
    /// <summary>
    /// <see cref="GreaterThanZeroAttribute"/> tests.
    /// </summary>
    public class GreaterThanZeroTests
    {
        /// <summary>
        /// <see cref="GreaterThanZeroAttribute"/> non-required test without data.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void ShouldReturnSuccessIfDataIsNull()
        {
            // Arrange.
            ValidationTargetInt target = new (null);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.True(isValid);
        }

        /// <summary>
        /// <see cref="GreaterThanZeroAttribute"/> tests (<see cref="int"/>).
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(null, true)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(int.MinValue, false)]
        [InlineData(int.MaxValue, true)]
        public void ValidateInt(int? value, bool expectedIsValid)
        {
            // Arrange.
            ValidationTargetInt target = new (value);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.Equal(expectedIsValid, isValid);
        }

        /// <summary>
        /// Test class for <see cref="ValidateInt(int?, bool)"/> tests.
        /// </summary>
        private class ValidationTargetInt
        {
            /// <summary>
            /// Property to validate.
            /// </summary>
            [GreaterThanZero]
            public int? A { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTargetInt(int? a)
            {
                A = a;
            }
        }

        /// <summary>
        /// <see cref="GreaterThanZeroAttribute"/> tests (<see cref="float"/>).
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(null, true)]
        [InlineData(-1f, false)]
        [InlineData(0f, false)]
        [InlineData(1f, true)]
        [InlineData(float.MinValue, false)]
        [InlineData(float.MaxValue, true)]
        public void ValidateFloat(float? value, bool expectedIsValid)
        {
            // Arrange.
            ValidationTargetFloat target = new (value);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.Equal(expectedIsValid, isValid);
        }

        /// <summary>
        /// Test class for <see cref="ValidateFloat(float?, bool)"/> tests.
        /// </summary>
        private class ValidationTargetFloat
        {
            /// <summary>
            /// Property to validate.
            /// </summary>
            [GreaterThanZero]
            public float? A { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTargetFloat(float? a)
            {
                A = a;
            }
        }

        /// <summary>
        /// <see cref="GreaterThanZeroAttribute"/> tests (<see cref="decimal"/>).
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(null, true)]
        [InlineData("-1.00", false)]
        [InlineData("0.00", false)]
        [InlineData("1.00", true)]
        [InlineData("-1000000.00", false)]
        [InlineData("+1000000.00", true)]
        public void ValidateDecimal(string? value, bool expectedIsValid)
        {
            // Arrange.
            ValidationTargetDecimal target = new (value == null ? null : Convert.ToDecimal(value));

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.Equal(expectedIsValid, isValid);
        }

        /// <summary>
        /// Test class for <see cref="ValidateDecimal(string?, bool)"/> tests.
        /// </summary>
        private class ValidationTargetDecimal
        {
            /// <summary>
            /// Property to validate.
            /// </summary>
            [GreaterThanZero]
            public decimal? A { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTargetDecimal(decimal? a)
            {
                A = a;
            }
        }

        /// <summary>
        /// <see cref="GreaterThanZeroAttribute"/> tests (<see cref="byte"/>).
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(null, true)]
        [InlineData((byte)1, true)]
        [InlineData(byte.MinValue, false)]
        [InlineData(byte.MaxValue, true)]
        public void ValidateByte(byte? value, bool expectedIsValid)
        {
            // Arrange.
            ValidationTargetFloat target = new (value);

            // Act.
            bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

            // Assert.
            Assert.Equal(expectedIsValid, isValid);
        }

        /// <summary>
        /// Test class for <see cref="ValidateByte(byte?, bool)"/> tests.
        /// </summary>
        private class ValidationTargetByte
        {
            /// <summary>
            /// Property to validate.
            /// </summary>
            [GreaterThanZero]
            public byte? A { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public ValidationTargetByte(byte? a)
            {
                A = a;
            }
        }

        /// <summary>
        /// <see cref="GreaterThanZeroAttribute"/> should be invalid if the base type is wrong.
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
            [GreaterThanZero]
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