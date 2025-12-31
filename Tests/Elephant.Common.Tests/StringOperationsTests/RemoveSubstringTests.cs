namespace Elephant.Common.Tests.StringOperationsTests
{
	/// <summary>
	/// <see cref="StringOperations"/> remove substrings tests.
	/// </summary>
	public class RemoveSubstringTests
	{
		/// <summary>
		/// Reusable source string for tests.
		/// </summary>
		private const string Source = "The big white dog walked around the block and then around the house.";

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringFromString(string, string)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("", "", "")]
		[InlineData(Source, "Non-existing", Source)]
		[InlineData(Source, "The", " big white dog walked around the block and then around the house.")]
		[InlineData(Source, "the", "The big white dog walked around  block and then around the house.")]
		[InlineData(Source, Source, "")]
		public void RemoveSubstringFromStringTests(string source, string substringToRemove, string expected)
		{
			// Act.
			string result = StringOperations.RemoveSubstringFromString(source, substringToRemove);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringsFromStringTestCaseSensitivity()
		{
			// Act.
			string result = StringOperations.RemoveSubstringsFromString("The big white dog.", ["the", "white"]);

			// Assert.
			Assert.Equal("The big  dog.", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringsFromStringTestFirstOccuranceOnly()
		{
			// Act.
			string result = StringOperations.RemoveSubstringsFromString("The very very big dog.", ["very"]);

			// Assert.
			Assert.Equal("The  very big dog.", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringsFromStringTestNonExisting()
		{
			// Act.
			string result = StringOperations.RemoveSubstringsFromString("The dog.", ["Small"]);

			// Assert.
			Assert.Equal("The dog.", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringFromString(string, string)"/> test with null substring.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringFromStringTestNullSubstring()
		{
			// Act.
			string result = StringOperations.RemoveSubstringFromString("test string", null!);

			// Assert.
			Assert.Equal("test string", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringFromString(string, string)"/> test with whitespace substring.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringFromStringTestWhitespace()
		{
			// Act.
			string result = StringOperations.RemoveSubstringFromString("The big dog", " ");

			// Assert.
			Assert.Equal("Thebig dog", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> test with empty collection.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringsFromStringTestEmptyCollection()
		{
			// Act.
			string result = StringOperations.RemoveSubstringsFromString("The big dog.", []);

			// Assert.
			Assert.Equal("The big dog.", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> should ignore a null value.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringsFromStringTestNullCollection()
		{
			// Act.
			string result = StringOperations.RemoveSubstringsFromString("The big dog.", [null!]);

			// Assert.
			Assert.Equal("The big dog.", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> test with multiple substrings.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringsFromStringTestMultipleSubstrings()
		{
			// Act.
			string result = StringOperations.RemoveSubstringsFromString(Source, ["big", "white", "around"]);

			// Assert.
			Assert.Equal("The   dog walked  the block and then around the house.", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> test with empty string in collection.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringsFromStringTestEmptyStringInCollection()
		{
			// Act.
			string result = StringOperations.RemoveSubstringsFromString("The big dog.", ["", "big"]);

			// Assert.
			Assert.Equal("The  dog.", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringFromString(string, string)"/> test with special characters.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringFromStringTestSpecialCharacters()
		{
			// Act.
			string result = StringOperations.RemoveSubstringFromString("Hello, World! How are you?", ", World!");

			// Assert.
			Assert.Equal("Hello How are you?", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringFromString(string, string)"/> test with substring at end.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringFromStringTestSubstringAtEnd()
		{
			// Act.
			string result = StringOperations.RemoveSubstringFromString("The big dog barks", " barks");

			// Assert.
			Assert.Equal("The big dog", result);
		}

		/// <summary>
		/// <see cref="StringOperations.RemoveSubstringFromString(string, string)"/> test with substring at beginning.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveSubstringFromStringTestSubstringAtBeginning()
		{
			// Act.
			string result = StringOperations.RemoveSubstringFromString("The big dog", "The ");

			// Assert.
			Assert.Equal("big dog", result);
		}
	}
}
