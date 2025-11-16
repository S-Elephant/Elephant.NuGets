namespace Elephant.Common.Tests.StringOperationsTests
{
	/// <summary>
	/// <see cref="StringOperations"/> capitalization tests.
	/// </summary>
	public class CapitalizationTests
	{
		/// <summary>
		/// <see cref="StringOperations.CapitalizeFirstChar(string?)"/> tests.
		/// Tests basic capitalization behavior: empty strings, single characters,
		/// and multi-word sentences where only the first character should be affected.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("", "")]
		[InlineData("a", "A")]
		[InlineData("A", "A")]
		[InlineData("The long dog.", "The long dog.")]
		[InlineData("the long dog.", "The long dog.")]
		public void CapitalizeFirstCharTests(string source, string expected)
		{
			// Act.
			string result = StringOperations.CapitalizeFirstChar(source);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="StringOperations.CapitalizeFirstChar(string?)"/> edge case tests.
		/// Tests special scenarios: numbers, special characters, whitespace,
		/// unicode characters, and strings that are already capitalized.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("1", "1")]
		[InlineData("123abc", "123abc")]
		[InlineData("!", "!")]
		[InlineData("!hello", "!hello")]
		[InlineData(" test", " test")]
		[InlineData("  leading spaces", "  leading spaces")]
		[InlineData(" ", " ")]
		[InlineData("\t", "\t")]
		[InlineData("\ttab", "\ttab")]
		[InlineData("über", "Über")]
		[InlineData("HELLO", "HELLO")]
		[InlineData("HeLLo", "HeLLo")]
		[InlineData("TheLongDog", "TheLongDog")]
		public void CapitalizeFirstCharEdgeCaseTests(string source, string expected)
		{
			// Act.
			string result = StringOperations.CapitalizeFirstChar(source);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="StringOperations.CapitalizeFirstCharNullable(string?)"/> tests.
		/// Tests nullable string handling: null values, empty strings, single characters,
		/// and multi-word sentences where only the first character should be affected.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(null, null)]
		[InlineData("", "")]
		[InlineData("a", "A")]
		[InlineData("A", "A")]
		[InlineData("The long dog.", "The long dog.")]
		[InlineData("the long dog.", "The long dog.")]
		public void CapitalizeFirstCharNullableTests(string? source, string? expected)
		{
			// Act.
			string? result = StringOperations.CapitalizeFirstCharNullable(source);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="StringOperations.CapitalizeFirstCharNullable(string?)"/> edge case tests.
		/// Tests special scenarios with nullable strings: numbers, special characters,
		/// whitespace, unicode characters, and strings that are already capitalized.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("1", "1")]
		[InlineData("123abc", "123abc")]
		[InlineData("!", "!")]
		[InlineData("!hello", "!hello")]
		[InlineData(" test", " test")]
		[InlineData("  leading spaces", "  leading spaces")]
		[InlineData(" ", " ")]
		[InlineData("\t", "\t")]
		[InlineData("\ttab", "\ttab")]
		[InlineData("über", "Über")]
		[InlineData("HELLO", "HELLO")]
		[InlineData("HeLLo", "HeLLo")]
		[InlineData("TheLongDog", "TheLongDog")]
		public void CapitalizeFirstCharNullableEdgeCaseTests(string? source, string? expected)
		{
			// Act.
			string? result = StringOperations.CapitalizeFirstCharNullable(source);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="StringOperations.ToTitleCase(string)"/> tests.
		/// Tests title case conversion behavior: empty strings, single characters,
		/// and multi-word sentences where each word's first character should be capitalized.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("", "")]
		[InlineData("a", "A")]
		[InlineData("A", "A")]
		[InlineData("The LONG dog.", "The Long Dog.")]
		[InlineData("the long dog.", "The Long Dog.")]
		public void ToTitleCaseTests(string source, string expected)
		{
			// Act.
			string result = StringOperations.ToTitleCase(source);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="StringOperations.ToTitleCase(string)"/> edge case tests.
		/// Tests special scenarios: numbers, special characters, whitespace,
		/// unicode characters, all caps, and mixed case strings.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("1", "1")]
		[InlineData("123 abc", "123 Abc")]
		[InlineData("!", "!")]
		[InlineData("!hello world", "!Hello World")]
		[InlineData(" test case", " Test Case")]
		[InlineData("  multiple  spaces", "  Multiple  Spaces")]
		[InlineData(" ", " ")]
		[InlineData("HELLO WORLD", "Hello World")]
		[InlineData("über den berg", "Über Den Berg")]
		[InlineData("mIxEd CaSe", "Mixed Case")]
		public void ToTitleCaseEdgeCaseTests(string source, string expected)
		{
			// Act.
			string result = StringOperations.ToTitleCase(source);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="StringOperations.ToTitleCaseNullable(string?)"/> tests.
		/// Tests nullable string handling with title case conversion: null values,
		/// empty strings, single characters, and multi-word sentences where each
		/// word's first character should be capitalized.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(null, null)]
		[InlineData("", "")]
		[InlineData("a", "A")]
		[InlineData("A", "A")]
		[InlineData("The LONG dog.", "The Long Dog.")]
		[InlineData("the long dog.", "The Long Dog.")]
		public void ToTitleCaseNullableTests(string? source, string? expected)
		{
			// Act.
			string? result = StringOperations.ToTitleCaseNullable(source);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="StringOperations.ToTitleCaseNullable(string?)"/> edge case tests.
		/// Tests special scenarios with nullable strings: numbers, special characters,
		/// whitespace, unicode characters, all caps, and mixed case strings.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("1", "1")]
		[InlineData("123 abc", "123 Abc")]
		[InlineData("!", "!")]
		[InlineData("!hello world", "!Hello World")]
		[InlineData(" test case", " Test Case")]
		[InlineData("  multiple  spaces", "  Multiple  Spaces")]
		[InlineData(" ", " ")]
		[InlineData("HELLO WORLD", "Hello World")]
		[InlineData("über den berg", "Über Den Berg")]
		[InlineData("mIxEd CaSe", "Mixed Case")]
		public void ToTitleCaseNullableEdgeCaseTests(string? source, string? expected)
		{
			// Act.
			string? result = StringOperations.ToTitleCaseNullable(source);

			// Assert.
			Assert.Equal(expected, result);
		}
	}
}
