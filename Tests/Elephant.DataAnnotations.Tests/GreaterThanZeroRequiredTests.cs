// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Elephant.DataAnnotations.Tests
{
	/// <summary>
	/// <see cref="GreaterThanZeroRequiredAttribute"/> tests.
	/// </summary>
	public class GreaterThanZeroRequiredTests
	{
		/// <summary>
		/// <see cref="GreaterThanZeroRequiredAttribute"/> non-required test without data.
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
		/// <see cref="GreaterThanZeroRequiredAttribute"/> tests.
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
		private sealed class ValidationTarget
		{
			/// <summary>
			/// Items to validate.
			/// </summary>
			[GreaterThanZeroRequired]
			public int? A { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTarget(int? a)
			{
				A = a;
			}
		}

		/// <summary>
		/// <see cref="GreaterThanZeroRequiredAttribute"/> should be invalid if the base type is wrong.
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
		private sealed class WrongType
		{
			/// <summary>
			/// Item to validate.
			/// </summary>
			[GreaterThanZeroRequired]
			// ReSharper disable once UnusedMember.Local
			public AlwaysWrong Item { get; set; } = new();
		}

		/// <summary>
		/// Class that is always the wrong type for validation testing.
		/// Used as a property in <see cref="WrongType"/>.
		/// </summary>
		private sealed class AlwaysWrong
		{
		}
	}
}