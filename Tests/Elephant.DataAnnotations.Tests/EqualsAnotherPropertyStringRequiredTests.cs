// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Elephant.DataAnnotations.Tests
{
	/// <summary>
	/// <see cref="GreaterThanZeroAttribute"/> tests.
	/// </summary>
	public class EqualsAnotherPropertyStringRequiredTests
	{
		/// <summary>
		/// <see cref="EqualsAnotherPropertyStringRequiredAttribute"/> case-sensitive tests.
		/// </summary>
		[Theory]
		[InlineData(null, null, null, false)]
		[InlineData(null, "", null, false)]
		[InlineData(null, "null", null, false)]
		[InlineData("a", "a", "a", true)]
		[InlineData("a", "b", "a", false)]
		[InlineData("a", "a", "b", false)]
		[InlineData("a", "A", "b", false)]
		[SpeedVeryFast, UnitTest]
		public void CaseSensitiveTests(string? a, string? b, string? c, bool expectedEqual)
		{
			// Arrange.
			ValidationTargetString target = new(a, b, c);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.Equal(expectedEqual, isValid);
		}

		/// <summary>
		/// <see cref="EqualsAnotherPropertyStringRequiredAttribute"/> case-insensitive tests.
		/// </summary>
		[Theory]
		[InlineData(null, null, null, false)]
		[InlineData(null, "", null, false)]
		[InlineData(null, "null", null, false)]
		[InlineData("a", "a", "a", true)]
		[InlineData("a", "b", "a", false)]
		[InlineData("a", "a", "b", false)]
		[InlineData("a", "A", "a", true)]
		[InlineData("A", "a", "A", true)]
		[SpeedVeryFast, UnitTest]
		public void CaseInsensitiveTests(string? a, string? b, string? c, bool expectedEqual)
		{
			// Arrange.
			ValidationTargetStringCaseInsensitive target = new(a, b, c);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.Equal(expectedEqual, isValid);
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private class ValidationTargetString
		{
			/// <summary>
			/// Property to validate.
			/// </summary>
			[EqualsAnotherPropertyStringRequired("B")]
			[EqualsAnotherPropertyStringRequired("C")]
			public string? A { get; set; }

			[EqualsAnotherPropertyStringRequired("A")]
			public string? B { get; set; }

			public string? C { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTargetString(string? a, string? b, string? c)
			{
				A = a;
				B = b;
				C = c;
			}
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private class ValidationTargetStringCaseInsensitive
		{
			/// <summary>
			/// Property to validate.
			/// </summary>
			[EqualsAnotherPropertyStringRequired("B", false)]
			[EqualsAnotherPropertyStringRequired("C", false)]
			public string? A { get; set; }

			[EqualsAnotherPropertyStringRequired("A", false)]
			public string? B { get; set; }

			public string? C { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTargetStringCaseInsensitive(string? a, string? b, string? c)
			{
				A = a;
				B = b;
				C = c;
			}
		}
	}
}