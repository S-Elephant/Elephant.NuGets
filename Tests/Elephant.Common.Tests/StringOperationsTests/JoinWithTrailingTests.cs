namespace Elephant.Common.Tests.StringOperationsTests
{
	/// <summary>
	/// <see cref="StringOperations"/> tests.
	/// </summary>
	public class JoinWithTrailingTests
	{
		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> empty tests.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfEmptyReturnsSeparator()
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/');

			// Assert.
			Assert.Equal("/", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> separator count tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A", "B", "C")]
		[InlineData("blue", "Blue", "blue")]
		public void TestIf3StringsReturn3Separators(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(3, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> separator count tests when the strings contain the separator inside.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A/A", "B/B", "C")]
		[InlineData("A", "B/B", "C/C")]
		public void TestIf3StringsReturn5Separators(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(5, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> separator count tests when the strings contain the separator on the outsides.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A/", "/B", "C")]
		[InlineData("A", "/B", "C/")]
		public void TestIf3StringsWithSeparatorsReturn3Separators(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(3, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> separator count tests with 1 string input.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A")]
		[InlineData("//A//")]
		public void TestIf1StringHasOneSeparatorOccurance(string stringA)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA);

			// Assert.
			Assert.Equal(1, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> 1 null value test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIf1NullValueReturnsOneSeparator()
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', [null]);

			// Assert.
			Assert.Equal(1, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> 3 null value test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIf3NullValuesReturnOneSeparator()
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', null, null, null);

			// Assert.
			Assert.Equal(1, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> null value combined with non-null values tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A/", null, "A", null)]
		[InlineData("a/", null, "a", null)]
		[InlineData("a/A/", "a", null, "A")]
		[InlineData("A/", null, null, "A")]
		public void TestIfNullWithNonNullValuesIgnoresNullValues(string expected, string? stringA, string? stringB, string? stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> test with empty strings.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("", "A", "B", "/A/B/")]
		[InlineData("A", "", "B", "A//B/")]
		[InlineData("A", "B", "", "A/B//")]
		public void TestWithEmptyStrings(string stringA, string stringB, string stringC, string expected)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> test with only empty strings.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestWithOnlyEmptyStrings()
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', "", "", "");

			// Assert.
			Assert.Equal("///", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> test with whitespace strings.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(" ", "A", " /A/")]
		[InlineData("A", " ", "A/ /")]
		[InlineData(" A ", " B ", " A / B /")]
		public void TestWithWhitespaceStrings(string stringA, string stringB, string expected)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> test with mixed null and empty strings.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("", null, "A", "/A/")]
		[InlineData("A", "", null, "A//")]
		[InlineData(null, "", "", "//")]
		public void TestWithMixedNullAndEmptyStrings(string? stringA, string? stringB, string? stringC, string expected)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> test with different separators.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData('-', "test", "test-")]
		[InlineData('.', "test", "test.")]
		[InlineData('\\', "test", "test\\")]
		[InlineData('|', "test", "test|")]
		public void TestWithDifferentSeparators(char separator, string input, string expected)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing(separator, input);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> test with multiple consecutive separators.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A//", "///B", "A/B/")]
		[InlineData("///", "B", "/B/")]
		[InlineData("A", "///", "A//")]
		public void TestWithMultipleConsecutiveSeparators(string stringA, string stringB, string expected)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> test with strings containing only separators.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("/", "A", "/A/")]
		[InlineData("A", "/", "A//")]
		[InlineData("///", "///", "//")]
		public void TestWithOnlySeparatorStrings(string stringA, string stringB, string expected)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> test with large number of strings.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestWithManyStrings()
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', "A", "B", "C", "D", "E", "F", "G", "H");

			// Assert.
			Assert.Equal("A/B/C/D/E/F/G/H/", joinedString);
			Assert.Equal(8, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> test with special characters in strings.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("@test", "#value", "@test/#value/")]
		[InlineData("hello world", "foo bar", "hello world/foo bar/")]
		[InlineData("123", "456", "123/456/")]
		public void TestWithSpecialCharactersInStrings(string stringA, string stringB, string expected)
		{
			// Act.
			string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB);

			// Assert.
			Assert.Equal(expected, joinedString);
		}
	}
}
