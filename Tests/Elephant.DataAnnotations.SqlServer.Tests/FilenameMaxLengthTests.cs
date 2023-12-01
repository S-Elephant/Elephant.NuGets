namespace Elephant.DataAnnotations.SqlServer.Tests
{
	/// <summary>
	/// <see cref="FilenameMaxLengthAttribute"/> tests.
	/// </summary>
	public class FilenameMaxLengthTests
	{
		/// <summary>
		/// Test class to validate.
		/// </summary>
		private class ValidationTarget
		{
			/// <summary>
			/// Items to validate.
			/// </summary>
			[FilenameMaxLength(2)]
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
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
		/// <see cref="FilenameMaxLengthAttribute"/> test without data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ShouldReturnTrueIfDataIsNull()
		{
			// Arrange.
			ValidationTarget target = new(null);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="FilenameMaxLengthAttribute"/> test with data.
		/// </summary>
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("a")]
		[InlineData("ab")]
		[SpeedVeryFast, UnitTest]
		public void ShouldReturnTrueIfDataDataIsWithinBoundaries(string? data)
		{
			// Arrange.
			ValidationTarget target = new(data);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="FilenameMaxLengthAttribute"/> test with data.
		/// </summary>
		[Theory]
		[InlineData("abc")]
		[InlineData("Something.")]
		[InlineData("Lorem ipsum./n Line 2.")]
		[SpeedVeryFast, UnitTest]
		public void ShouldReturnFalseIfDataDataIsTooLong(string data)
		{
			// Arrange.
			ValidationTarget target = new(data);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}
	}
}