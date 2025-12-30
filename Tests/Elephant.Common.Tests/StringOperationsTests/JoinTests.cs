using System.Globalization;

namespace Elephant.Common.Tests.StringOperationsTests
{
	/// <summary>
	/// <see cref="StringOperations"/> tests.
	/// </summary>
	public class JoinTests
	{
		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> empty tests.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfEmptyReturnsEmpty()
		{
			// Act.
			string joinedString = StringOperations.Join('/');

			// Assert.
			Assert.Equal(string.Empty, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> separator count tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A", "B", "C")]
		[InlineData("blue", "Blue", "blue")]
		public void TestIf3StringsReturn2Separators(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.Join('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(2, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> separator count tests when the strings contain the separator inside.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A/A", "B/B", "C")]
		[InlineData("A", "B/B", "C/C")]
		public void TestIf3StringsReturns4InsideSeparators(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.Join('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(4, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> separator count tests when the strings contain the separator on the outsides.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A/", "/B", "C")]
		[InlineData("A", "/B", "C/")]
		public void TestIf3StringsReturn2OutsideSeparators(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.Join('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(2, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> separator count tests with 1 string input.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A")]
		[InlineData("//A//")]
		public void TestIf1StringReturnsNoSeparators(string stringA)
		{
			// Act.
			string joinedString = StringOperations.Join('/', stringA);

			// Assert.
			Assert.Equal(0, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> 1 null value test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIf1NullValueReturnEmpty()
		{
			// Act.
			string joinedString = StringOperations.Join('/', [null]);

			// Assert.
			Assert.Equal(0, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> 3 null value test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIf3NullValuesReturnEmpty()
		{
			// Act.
			string joinedString = StringOperations.Join('/', null, null, null);

			// Assert.
			Assert.Equal(0, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> null value combined with non-null values tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A", null, "A", null)]
		[InlineData("a", null, "a", null)]
		[InlineData("a/A", "a", null, "A")]
		[InlineData("A", null, null, "A")]
		public void TestIfNullWithNonNullValuesIgnoresNullValues(string expected, string? stringA, string? stringB, string? stringC)
		{
			// Act.
			string joinedString = StringOperations.Join('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> test with empty strings.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfEmptyStringsReturnSeparators()
		{
			// Act.
			string joinedString = StringOperations.Join('/', "", "", "");

			// Assert.
			Assert.Equal(2, joinedString.Count(x => x == '/'));
			Assert.Equal("//", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> test with mix of empty and non-empty strings.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A//C", "A", "", "C")]
		[InlineData("/B/C", "", "B", "C")]
		[InlineData("A/B/", "A", "B", "")]
		public void TestIfMixedEmptyAndNonEmptyStringsReturnExpected(string expected, string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.Join('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> test with special characters.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A@B", '@', "A", "B")]
		[InlineData("Path\\To\\File", '\\', "Path", "To", "File")]
		[InlineData("Column1|Column2|Column3", '|', "Column1", "Column2", "Column3")]
		public void TestIfSpecialSeparatorsWork(string expected, char separator, params string[] strings)
		{
			// Act.
			string joinedString = StringOperations.Join(separator, strings);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> test with whitespace strings.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(" / / ", " ", " ", " ")]
		[InlineData("\t/\t", "\t", "\t")]
		public void TestIfWhitespaceStringsAreJoined(string expected, params string[] strings)
		{
			// Act.
			string joinedString = StringOperations.Join('/', strings);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> test with multiple consecutive separators at boundaries.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("/A", "///", "A")]
		[InlineData("A/", "A", "///")]
		[InlineData("/A/", "///", "A", "///")]
		public void TestIfMultipleConsecutiveSeparatorsAtBoundariesAreHandled(string expected, params string[] strings)
		{
			// Act.
			string joinedString = StringOperations.Join('/', strings);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> test with large number of strings.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfLargeNumberOfStringsAreJoinedCorrectly()
		{
			// Arrange.
			string[] strings = new string[100];
			for (int i = 0; i < strings.Length; i++)
				strings[i] = i.ToString(CultureInfo.InvariantCulture);

			// Act.
			string joinedString = StringOperations.Join('/', strings);

			// Assert.
			Assert.Equal(99, joinedString.Count(x => x == '/'));
			Assert.StartsWith("0/1/2", joinedString);
			Assert.EndsWith("97/98/99", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> test with alternating null and non-null values.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfAlternatingNullAndNonNullValuesWork()
		{
			// Act.
			string joinedString = StringOperations.Join('/', "A", null, "B", null, "C");

			// Assert.
			Assert.Equal("A/B/C", joinedString);
			Assert.Equal(2, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> test with unicode characters.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("Hello/世界")]
		[InlineData("Привет/🌍/World")]
		public void TestIfUnicodeStringsAreJoinedCorrectly(string expected)
		{
			// Arrange.
			string[] parts = expected.Split('/');

			// Act.
			string joinedString = StringOperations.Join('/', parts);

			// Assert.
			Assert.Equal(expected, joinedString);
		}
	}
}
