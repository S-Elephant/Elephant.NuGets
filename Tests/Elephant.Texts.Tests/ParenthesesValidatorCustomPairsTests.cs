using System.Diagnostics;

namespace Elephant.Texts.Tests
{
	/// <summary>
	/// <see cref="ParenthesesValidator"/> tests using custom pairs.
	/// </summary>
	public class ParenthesesValidatorCustomPairsTests
	{
		/// <summary>
		/// Tests that empty or null input is considered valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("")]
		[InlineData(null)]
		public void IsValid_EmptyOrNullInput_ReturnsTrue(string input)
		{
			// Arrange.
			IParenthesesValidator customValidator = new ParenthesesValidator(new() { { '«', '»' }, { '{', '}' } });

			// Act.
			bool isValid = customValidator.IsValid(input);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Tests that validator correctly validates properly nested standard bracket pairs.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsValid_WithDefaultPairs_ValidatesProperlyNestedPairs()
		{
			// Arrange.
			IParenthesesValidator customValidator = new ParenthesesValidator(new() { { '«', '»' }, { '{', '}' } });

			// Act.
			bool isValid = customValidator.IsValid("«»[]{}<>");

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Tests validation of asymmetric bracket pairs (different opening/closing characters).
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("«test»", true)]
		[InlineData("«test", false)]
		[InlineData("«test»}", false)]
		[InlineData("{«test»", false)]
		[InlineData("«t{e}st»", true)]
		[InlineData("(test)", true)]
		[InlineData("(test)((", true)]
		[InlineData("||", true)]
		[InlineData("|test|", true)]
		[InlineData("|test", true)]
		public void IsValid_WithAsymmetricPairs_ValidatesCorrectly(string input, bool expected)
		{
			// Arrange.
			IParenthesesValidator customValidator = new ParenthesesValidator(new() { { '«', '»' }, { '{', '}' } });

			// Act.
			bool isValid = customValidator.IsValid(input);

			// Assert.
			Assert.Equal(expected, isValid);
		}

		/// <summary>
		/// Tests validation of symmetric bracket pairs (same opening/closing character).
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("|test|", true)]
		[InlineData("||test||", true)]
		[InlineData("|test", false)]
		[InlineData("test|", false)]
		[InlineData("|t|e|s|t|", false)]
		[InlineData("|t|e|s|t||", true)]
		public void IsValid_WithSymmetricPairs_ValidatesCorrectly(string input, bool expected)
		{
			// Arrange.
			IParenthesesValidator customValidator = new ParenthesesValidator(new() { { '|', '|' } });

			// Act.
			bool isValid = customValidator.IsValid(input);

			// Assert.
			Assert.Equal(expected, isValid);
		}

		/// <summary>
		/// Tests validation of mixed symmetric and asymmetric pairs.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("«|testl»", true)]
		[InlineData("|«test»l", true)]
		[InlineData("|«test»", false)]
		[InlineData("«|test»", false)]
		public void IsValid_WithMixedPairs_ValidatesCorrectly(string input, bool expected)
		{
			// Arrange.
			IParenthesesValidator customValidator = new ParenthesesValidator(new() { { '«', '»' }, { '|', 'l' } });

			// Act.
			bool isValid = customValidator.IsValid(input);

			// Assert.
			Assert.Equal(expected, isValid);
		}

		/// <summary>
		/// Tests that the validator correctly handles mixed bracket pairs while enforcing proper nesting rules.
		/// Verifies that brackets of different types can be nested within each other when properly closed.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("(«)»", true)]
		[InlineData("(«test»)", true)]
		[InlineData("«(test)»", true)]
		[InlineData("«(»)", true)]
		public void IsValid_WithMixedPairs_EnforcesNesting(string input, bool expected)
		{
			// Arrange.
			IParenthesesValidator customValidator = new ParenthesesValidator(new() { { '«', '»' } });

			// Act.
			bool isValid = customValidator.IsValid(input);

			// Assert.
			Assert.Equal(expected, isValid);
		}

		/// <summary>
		/// Tests that validator ignores unspecified brackets when only custom pairs are configured.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("§test¶", true)]
		[InlineData("()", true)]
		[InlineData("§test", false)]
		public void IsValid_WithOnlyCustomPairs_IgnoresStandardBrackets(string input, bool expected)
		{
			// Arrange.
			IParenthesesValidator customValidator = new ParenthesesValidator(new() { { '§', '¶' } });

			// Act.
			bool isValid = customValidator.IsValid(input);

			// Assert.
			Assert.Equal(expected, isValid);
		}
	}
}
