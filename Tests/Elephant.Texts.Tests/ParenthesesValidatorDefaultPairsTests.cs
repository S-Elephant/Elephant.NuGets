namespace Elephant.Texts.Tests
{
	/// <summary>
	/// <see cref="ParenthesesValidator"/> tests using it's default pairs.
	/// </summary>
	public class ParenthesesValidatorDefaultPairsTests
	{
		/// <summary>
		/// System under test.
		/// </summary>
		private IParenthesesValidator _parenthesesValidator;

		/// <summary>
		/// Setup.
		/// </summary>
		public ParenthesesValidatorDefaultPairsTests()
		{
			_parenthesesValidator = new ParenthesesValidator();
		}

		/// <summary>
		/// Tests that simple balanced parentheses are validated correctly.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("()", true)]
		[InlineData("[]", true)]
		[InlineData("{}", true)]
		[InlineData("<>", true)]
		[InlineData("()[]", true)]
		[InlineData("([])", true)]
		[InlineData("([{}])", true)]
		public void IsValid_SimpleBalancedParentheses_ReturnsTrue(string input, bool expected)
		{
			// Act.
			bool isValid = _parenthesesValidator.IsValid(input);

			// Assert.
			Assert.Equal(expected, isValid);
		}

		/// <summary>
		/// Tests that unbalanced parentheses are detected correctly.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("(", false)]
		[InlineData(")", false)]
		[InlineData("[)", false)]
		[InlineData("([)]", false)]
		[InlineData("({[}])", false)]
		[InlineData("][", false)]
		[InlineData("}{", false)]
		[InlineData("><", false)]
		public void IsValid_UnbalancedParentheses_ReturnsFalse(string input, bool expected)
		{
			// Act.
			bool isValid = _parenthesesValidator.IsValid(input);

			// Assert.
			Assert.Equal(expected, isValid);
		}

		/// <summary>
		/// Tests that text with balanced parentheses is validated correctly.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("(text)")]
		[InlineData("[some]text")]
		[InlineData("()[]text")]
		[InlineData("({[text]})")]
		[InlineData("text(text)te!xt[text]text.")]
		public void IsValid_TextWithBalancedParentheses_ReturnsTrue(string input)
		{
			// Act.
			bool isValid = _parenthesesValidator.IsValid(input);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Tests that text with unbalanced parentheses is detected correctly.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("(text")]
		[InlineData("text)")]
		[InlineData("te[xt")]
		[InlineData("te]xt")]
		[InlineData("te(x)t]")]
		public void IsValid_TextWithUnbalancedParentheses_ReturnsFalse(string input)
		{
			// Act.
			bool isValid = _parenthesesValidator.IsValid(input);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// Tests that empty or null input is considered valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("")]
		[InlineData(null)]
		public void IsValid_EmptyOrNullInput_ReturnsTrue(string? input)
		{
			// Act.
			bool isValid = _parenthesesValidator.IsValid(input);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Tests that nested balanced parentheses are validated correctly.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("({[<>]})")]
		[InlineData("([{([{}])}])")]
		public void IsValid_ComplexNestedBalancedParentheses_ReturnsTrue(string input)
		{
			// Act.
			bool isValid = _parenthesesValidator.IsValid(input);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// Tests that complex unbalanced parentheses are detected correctly.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("()[{([]})]")]
		[InlineData("({[<}>]})")]
		[InlineData("([{([{]}])}])")]
		[InlineData("()[{([])}(])")]
		public void IsValid_ComplexNestedUnbalancedParentheses_ReturnsFalse(string input)
		{
			// Act.
			bool isValid = _parenthesesValidator.IsValid(input);

			// Assert.
			Assert.False(isValid);
		}
	}
}
