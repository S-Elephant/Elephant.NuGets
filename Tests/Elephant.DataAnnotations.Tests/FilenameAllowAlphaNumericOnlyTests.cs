// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Elephant.DataAnnotations.Tests
{
	/// <summary>
	/// <see cref="FilenameAllowAlphaNumericOnly"/> tests.
	/// </summary>
	public class FilenameAllowAlphaNumericOnlyTests
	{
		/// <summary>
		/// Test class.
		/// </summary>
		private class ValidationTargetString
		{
			/// <summary>
			/// Property to validate.
			/// </summary>
			[FilenameAllowAlphaNumericOnly]
			public string? Value { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTargetString(string? value)
			{
				Value = value;
			}
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private class ValidationTargetStringAllowDot
		{
			/// <summary>
			/// Property to validate.
			/// </summary>
			[FilenameAllowAlphaNumericOnly(true)]
			public string? Value { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTargetStringAllowDot(string? value)
			{
				Value = value;
			}
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private class ValidationTargetStringAllowDotAndUnderscore
		{
			/// <summary>
			/// Property to validate.
			/// </summary>
			[FilenameAllowAlphaNumericOnly(true, true)]
			public string? Value { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTargetStringAllowDotAndUnderscore(string? value)
			{
				Value = value;
			}
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private class ValidationTargetFloat
		{
			/// <summary>
			/// Property to validate.
			/// </summary>
			[FilenameAllowAlphaNumericOnly]
			public float? Value { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTargetFloat(float? value)
			{
				Value = value;
			}
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private class ValidationTargetInt
		{
			/// <summary>
			/// Property to validate.
			/// </summary>
			[FilenameAllowAlphaNumericOnly]
			public int? Value { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTargetInt(int? value)
			{
				Value = value;
			}
		}

		/// <summary>
		/// Test class.
		/// Extra characters: \/[]{}+-
		/// </summary>
		private class ValidationTargetStringWithExtraCharacters
		{
			/// <summary>
			/// Property to validate.
			/// </summary>
			[FilenameAllowAlphaNumericOnly(extraAllowedCharacters: @"\/][{}+-")]
			public string? Value { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTargetStringWithExtraCharacters(string? value)
			{
				Value = value;
			}
		}

		/// <summary>
		/// Is valid if string contains only alphanumerics.
		/// </summary>
		[Theory]
		[InlineData("abc")]
		[InlineData("0")]
		[InlineData("54389325723553444")]
		[InlineData("0jiogfrewGREGR4235562300Eb")]
		[SpeedVeryFast, UnitTest]
		public void IsValidIfValueIsAlphanumericOnly(string value)
		{
			// Arrange.
			ValidationTargetString target = new(value);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Is invalid if string contains not just alphanumerics.
		/// </summary>
		[Theory]
		[InlineData("a.png")]
		[InlineData("a_a")]
		[InlineData("abc!")]
		[InlineData("-0")]
		[InlineData("-1")]
		[InlineData("54389325723553444 greger")]
		[InlineData("^")]
		[InlineData(@"\")]
		[InlineData(@"\a")]
		[InlineData(@"a\")]
		[InlineData("^^test")]
		[InlineData("�")]
		[InlineData("test.png")]
		[SpeedVeryFast, UnitTest]
		public void IsInvalidIfValueIsAlphanumericOnly(string value)
		{
			// Arrange.
			ValidationTargetString target = new(value);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// Is valid if string contains alphanumerics, including a dot.
		/// </summary>
		[Theory]
		[InlineData("test.png")]
		[InlineData(".")]
		[InlineData("...")]
		[InlineData(".a.")]
		[SpeedVeryFast, UnitTest]
		public void IsValidIfValueIsAlphanumericWithDot(string value)
		{
			// Arrange.
			ValidationTargetStringAllowDot target = new(value);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Is invalid if string contains alphanumerics, including a dot and underscore.
		/// </summary>
		[Theory]
		[InlineData("test_a.png")]
		[InlineData("_")]
		[InlineData("_._")]
		[InlineData("___")]
		[InlineData("_a_")]
		[SpeedVeryFast, UnitTest]
		public void IsInvalidIfValueIsAlphanumericWithDotAndUnderscore(string value)
		{
			// Arrange.
			ValidationTargetStringAllowDot target = new(value);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// Is valid if string contains alphanumerics, including a dot and underscore.
		/// </summary>
		[Theory]
		[InlineData("test_a.png")]
		[InlineData("_")]
		[InlineData("_._")]
		[InlineData("___")]
		[InlineData("_a_")]
		[SpeedVeryFast, UnitTest]
		public void IsValidIfValueIsAlphanumericWithDotAndUnderscore(string value)
		{
			// Arrange.
			ValidationTargetStringAllowDotAndUnderscore target = new(value);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Is valid if is numeric only and zero or positive.
		/// </summary>
		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(54389325)]
		[SpeedVeryFast, UnitTest]
		public void IsValidIfValueIsNumericOnly(int value)
		{
			// Arrange.
			ValidationTargetInt target = new(value);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Is valid if is numeric only but smaller than zero.
		/// </summary>
		[Theory]
		[InlineData(-1)]
		[InlineData(-1543)]
		[InlineData(-1000000)]
		[SpeedVeryFast, UnitTest]
		public void IsInvalidIfValueIsNumericOnlyButLessThanZero(int value)
		{
			// Arrange.
			ValidationTargetInt target = new(value);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// Is invalid if used on an unsupported type.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsInvalidIfUsedOnUnsupportedType()
		{
			// Arrange.
			ValidationTargetFloat target = new(1);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// Is valid if string contains alphanumerics and/or characters included in the <see cref="FilenameAllowAlphaNumericOnly"/> extraCharacters parameter.
		/// </summary>
		[Theory]
		[InlineData("1+2-3")]
		[InlineData("[a]")]
		[InlineData("--")]
		[InlineData(@"Home/page1/page2")]
		[InlineData(@"Directory\directory\File")]
		[SpeedVeryFast, UnitTest]
		public void IsValidIfValueIsWithinExtraCharacters(string value)
		{
			// Arrange.
			ValidationTargetStringWithExtraCharacters target = new(value);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Is invalid if string contains characters not included in the <see cref="FilenameAllowAlphaNumericOnly"/> extraCharacters parameter.
		/// </summary>
		[Theory]
		[InlineData("a&a")]
		[InlineData("^b&%$")]
		[InlineData("|f")]
		[InlineData(@"_")]
		[SpeedVeryFast, UnitTest]
		public void IsInvalidIfValueIsWithinExtraCharacters(string value)
		{
			// Arrange.
			ValidationTargetStringWithExtraCharacters target = new(value);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}
	}
}