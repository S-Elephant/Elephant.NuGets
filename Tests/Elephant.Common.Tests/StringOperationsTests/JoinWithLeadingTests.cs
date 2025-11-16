namespace Elephant.Common.Tests.StringOperationsTests
{
	/// <summary>
	/// <see cref="StringOperations"/> tests.
	/// </summary>
	public class JoinWithLeadingTests
	{
		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> empty tests.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfEmptyReturnsSeparator()
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/');

			// Assert.
			Assert.Equal("/", joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> separator count tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A", "B", "C")]
		[InlineData("blue", "Blue", "blue")]
		public void TestIf3StringsReturn3Separators(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(3, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> separator count tests when the strings contain the separator inside.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A/A", "B/B", "C")]
		[InlineData("A", "B/B", "C/C")]
		public void TestIf3StringsReturn5Separators(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(5, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> separator count tests when the strings contain the separator on the outsides.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A/", "/B", "C")]
		[InlineData("A", "/B", "C/")]
		public void TestIf3StringsWithSeparatorsReturn3Separators(string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(3, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> separator count tests with 1 string input.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("A")]
		[InlineData("//A//")]
		public void TestIf1StringHasOneSeparatorOccurance(string stringA)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', stringA);

			// Assert.
			Assert.Equal(1, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> 1 null value test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIf1NullValueReturnsOneSeparator()
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', new string?[] { null });

			// Assert.
			Assert.Equal(1, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> 3 null value test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIf3NullValuesReturnOneSeparator()
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', null, null, null);

			// Assert.
			Assert.Equal(1, joinedString.Count(x => x == '/'));
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> null value combined with non-null values tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("/A", null, "A", null)]
		[InlineData("/a", null, "a", null)]
		[InlineData("/a/A", "a", null, "A")]
		[InlineData("/A", null, null, "A")]
		public void TestIfNullWithNonNullValuesIgnoresNullValues(string expected, string? stringA, string? stringB, string? stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> empty string tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("//A/B", "", "A", "B")]
		[InlineData("/A//B", "A", "", "B")]
		[InlineData("/A/B/", "A", "B", "")]
		[InlineData("///", "", "", "")]
		public void TestIfEmptyStringsCreateEmptySections(string expected, string stringA, string stringB, string stringC)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> whitespace string tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("/ /A", " ", "A")]
		[InlineData("/A/ ", "A", " ")]
		[InlineData("/ / ", " ", " ")]
		public void TestIfWhitespaceStringsArePreserved(string expected, string stringA, string stringB)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', stringA, stringB);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> different separator tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("-A-B-C", '-', "A", "B", "C")]
		[InlineData("|A|B", '|', "A", "B")]
		[InlineData(".test", '.', "test")]
		public void TestIfDifferentSeparatorsWork(string expected, char separator, params string[] strings)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading(separator, strings);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> multiple consecutive separators test.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("/A/B", "///A", "B///")]
		[InlineData("/A/B/C", "A////", "////B////", "////C")]
		public void TestIfMultipleConsecutiveSeparatorsAreTrimmed(string expected, string stringA, string stringB, string? stringC = null)
		{
			// Act.
			string joinedString = stringC == null
				? StringOperations.JoinWithLeading('/', stringA, stringB)
				: StringOperations.JoinWithLeading('/', stringA, stringB, stringC);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> mixed null and empty strings test.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("//A//B", null, "", "A", null, "", "B", null)]
		[InlineData("///X/", "", null, "", "X", "", null)]
		public void TestIfMixedNullAndEmptyStringsAreHandledCorrectly(string expected, params string?[] strings)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', strings);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> single character strings test.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("/A/B/C", "A", "B", "C")]
		[InlineData("/1/2/3/4", "1", "2", "3", "4")]
		public void TestIfSingleCharacterStringsWork(string expected, params string[] strings)
		{
			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', strings);

			// Assert.
			Assert.Equal(expected, joinedString);
		}

		/// <summary>
		/// <see cref="StringOperations.JoinWithLeading(char, string?[])"/> long strings test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TestIfLongStringsAreJoinedCorrectly()
		{
			// Arrange.
			string longStringA = new string('A', 1000);
			string longStringB = new string('B', 1000);

			// Act.
			string joinedString = StringOperations.JoinWithLeading('/', longStringA, longStringB);

			// Assert.
			Assert.StartsWith("/", joinedString);
			Assert.Contains("/", joinedString.Substring(1));
			Assert.Equal(2002, joinedString.Length); // 1 leading separator + 1000 A's + 1 separator + 1000 B's
		}
	}
}
