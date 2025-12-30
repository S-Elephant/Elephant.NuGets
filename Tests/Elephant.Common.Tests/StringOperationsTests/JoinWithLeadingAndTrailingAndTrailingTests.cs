namespace Elephant.Common.Tests.StringOperationsTests
{
	/// <summary>
	/// <see cref="StringOperations"/> tests.
	/// </summary>
	public class JoinWithLeadingAndTrailingAndTrailingTests
	{
		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> empty tests.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfEmptyReturnsTwoSeparators()
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/');

			// Assert.
			Assert.Equal("//", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> separator count tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A", "B", "C")]
		[InlineData("blue", "Blue", "blue")]
		public void TestIf4StringsHave3SeparatorOccurances(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(4, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> separator count tests when the strings contain the separator inside.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A/A", "B/B", "C")]
		[InlineData("A", "B/B", "C/C")]
		public void TestIf3StringsHave6SeparatorOccurances(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(6, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> separator count tests when the strings contain the separator on the outsides.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A/", "/B", "C")]
		[InlineData("A", "/B", "C/")]
		public void TestIf3StringsHave4SeparatorOccurances(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(4, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> separator count tests with 1 string input.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A")]
		[InlineData("//A//")]
		public void TestIf1StringReturnsTwoSeparators(string stringA)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA);

			// Assert.
			Assert.Equal(2, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> 1 null value test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIf1NullValueReturnsTwoSeparators()
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', [null]);

			// Assert.
			Assert.Equal(2, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> 3 null value test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIf3NullValuesReturnTwoSeparators()
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', null, null, null);

			// Assert.
			Assert.Equal(2, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> null value combined with non-null values tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("/A/", null, "A", null)]
		[InlineData("/a/", null, "a", null)]
		[InlineData("/a/A/", "a", null, "A")]
		[InlineData("/A/", null, null, "A")]
		[InlineData("/A/", null, null, "/A/")]
		public void TestIfNullWithNonNullValuesIgnoresNullValues(string expected, string? stringA, string? stringB, string? stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> empty string tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("", "", "")]
		[InlineData("A", "", "B")]
		[InlineData("", "A", "")]
		public void TestIfEmptyStringsAreHandledCorrectly(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.StartsWith("/", joinedString);
			Assert.EndsWith("/", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> single empty string test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfSingleEmptyStringReturnsTwoSeparators()
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', "");

			// Assert.
			Assert.Equal("//", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> different separator character tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData('-', "A", "B", "-A-B-")]
		[InlineData('|', "test", "case", "|test|case|")]
		[InlineData('\\', "path", "to", "\\path\\to\\")]
		[InlineData(' ', "hello", "world", " hello world ")]
		public void TestIfDifferentSeparatorsWorkCorrectly(char separator, string stringA, string stringB, string expected)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing(separator, stringA, stringB);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> whitespace string tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(" ", "A", "B")]
		[InlineData("A", " ", "B")]
		[InlineData("  ", "  ", "  ")]
		public void TestIfWhitespaceStringsArePreserved(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.StartsWith("/", joinedString);
			Assert.EndsWith("/", joinedString);
			Assert.Contains(stringA, joinedString);
			Assert.Contains(stringB, joinedString);
			Assert.Contains(stringC, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> large number of strings test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfManyStringsAreJoinedCorrectly()
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', "A", "B", "C", "D", "E", "F", "G");

			// Assert.
			Assert.Equal(8, joinedString.Count(x => x == '/'));
			Assert.StartsWith("/", joinedString);
			Assert.EndsWith("/", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> all strings have leading and trailing separators test.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("/A/", "/B/", "/C/")]
		[InlineData("//A//", "//B//", "//C//")]
		public void TestIfAllStringsWithSeparatorsAreNormalized(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(4, joinedString.Count(x => x == '/'));
			Assert.Equal("/A/B/C/", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> special characters in strings test.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A@B", "C#D", "E&F")]
		[InlineData("test123", "456test", "789")]
		public void TestIfSpecialCharactersArePreserved(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Contains(stringA, joinedString);
			Assert.Contains(stringB, joinedString);
			Assert.Contains(stringC, joinedString);
			Assert.Equal(4, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> mixed null and empty strings test.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("/A//", null, "A", "")]
		[InlineData("//B/", "", null, "B")]
		[InlineData("///", "", null, "")]
		public void TestIfMixedNullAndEmptyStringsAreHandled(string expected, string? stringA, string? stringB, string? stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> unicode characters test.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("café", "naïve", "résumé")]
		[InlineData("你好", "世界", "测试")]
		[InlineData("😀", "🎉", "✨")]
		public void TestIfUnicodeCharactersAreHandled(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Contains(stringA, joinedString);
			Assert.Contains(stringB, joinedString);
			Assert.Contains(stringC, joinedString);
			Assert.Equal(4, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> two strings test.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A", "B")]
		[InlineData("hello", "world")]
		public void TestIfTwoStringsHaveThreeSeparators(string stringA, string stringB)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB);

			// Assert.
			Assert.Equal(3, joinedString.Count(x => x == '/'));
			Assert.StartsWith("/", joinedString);
			Assert.EndsWith("/", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> very long string test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfVeryLongStringsAreHandled()
		{
			// Arrange.
			string longString = new string('A', 1000);

			// Act.
			string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', longString, "B", "C");

			// Assert.
			Assert.Contains(longString, joinedString);
			Assert.Equal(4, joinedString.Count(x => x == '/'));
		}
	}
}
