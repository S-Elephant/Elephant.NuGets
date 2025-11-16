namespace Elephant.Common.Tests.StringOperationsTests
{
	/// <summary>
	/// <see cref="StringOperations.EncloseByIfNotAlready"/> tests.
	/// </summary>
	public class EncloseByIfNotAlreadyTests
	{
		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should enclose using double quotes.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseInDoubleQuotes()
		{
			// Arrange.
			const string originalString = "This is a sentence that must be enclosed in double quotes.";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal($"\"{originalString}\"", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should NOT enclose using double quotes because it already was.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseInDoubleQuotesShouldDoNothing()
		{
			// Arrange.
			const string originalString = "\"This is a sentence that must be enclosed in double quotes.\"";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal(originalString, enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should enclose using 'A'.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseByA()
		{
			// Arrange.
			const string originalString = "This is a sentence that must be enclosed in double quotes.";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, 'A');

			// Assert.
			Assert.Equal($"A{originalString}A", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should handle empty string.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseEmptyString()
		{
			// Arrange.
			const string originalString = "";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal("\"\"", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should handle string with only starting character.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseStringWithOnlyStartingCharacter()
		{
			// Arrange.
			const string originalString = "\"This has no ending quote";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal($"{originalString}\"", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should handle string with only ending character.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseStringWithOnlyEndingCharacter()
		{
			// Arrange.
			const string originalString = "This has no starting quote\"";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal($"\"{originalString}", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should handle single character string.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseSingleCharacter()
		{
			// Arrange.
			const string originalString = "X";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal("\"X\"", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should handle string that is only the enclosing character.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseStringThatIsOnlyTheEnclosingCharacter()
		{
			// Arrange.
			const string originalString = "\"";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal("\"\"\"", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should handle string with enclosing characters in the middle.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseStringWithCharacterInMiddle()
		{
			// Arrange.
			const string originalString = "This has a \" in the middle";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal($"\"{originalString}\"", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should handle whitespace-only string.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseWhitespaceString()
		{
			// Arrange.
			const string originalString = "   ";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal($"\"{originalString}\"", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should handle special characters as enclosing character.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseWithSpecialCharacter()
		{
			// Arrange.
			const string originalString = "test";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '*');

			// Assert.
			Assert.Equal($"*{originalString}*", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should handle string with multiple enclosing characters already present.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseStringWithMultipleEnclosingCharacters()
		{
			// Arrange.
			const string originalString = "\"\"test\"\"";

			// Act.
			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			// Assert.
			Assert.Equal(originalString, enclosedValue);
		}
	}
}
