using System.Globalization;

namespace Elephant.Texts.Tests.Truncators
{
	/// <summary>
	/// <see cref="Truncator.TruncateNullableWithEllipsis"/> tests.
	/// </summary>
	public class TruncateNullableWithEllipsisTests
	{
		/// <summary>
		/// Returns null when the input value is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullValue_ReturnsNull()
		{
			// Arrange.
			string? value = null;
			int maxLength = 5;

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength);

			// Assert.
			Assert.Null(result);
		}

		/// <summary>
		/// Returns an empty string when maxLength is non-positive.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NonPositiveMaxLength_ReturnsEmptyString()
		{
			// Arrange.
			string value = "abc";
			int maxLength = 0;

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength);

			// Assert.
			Assert.Equal(string.Empty, result);
		}

		/// <summary>
		/// Returns the original string when no truncation is required.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NoTruncation_ReturnsOriginal()
		{
			// Arrange.
			string value = "pikapika";
			int maxLength = 10;

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength);

			// Assert.
			Assert.Equal(value, result);
		}

		/// <summary>
		/// Truncates and appends the default ellipsis when the string is too long.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TruncatesAndAppendsDefaultEllipsis()
		{
			// Arrange.
			string value = "HelloWorld";
			int maxLength = 5; // Default ellipsis is ".." so keep = 3.
			string expected = "Hel..";

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Returns a truncated ellipsis when the ellipsis length is greater than or equal to maxLength.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EllipsisLongerThanMaxLength_ReturnsTruncatedEllipsis()
		{
			// Arrange.
			string value = "abcdef";
			int maxLength = 1;
			string expected = "."; // Default ellipsis ".." truncated to length 1.

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Does not split Unicode grapheme clusters when truncating.
		/// This ensures composed characters (e.g. 'e' + combining acute) remain intact.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void DoesNotSplitGraphemeClusters()
		{
			// Arrange.
			// Compose an 'é' using 'e' + combining acute accent so it is a multi-code-unit grapheme.
			string value = "e\u0301BC"; // Visually "éBC".
			int maxLength = 2; // Use a small max length.
			string ellipsis = "…"; // Single-character ellipsis to allow keeping at least one grapheme.
			string expected = "e\u0301…"; // Should keep the full 'é' grapheme, not just 'e'.

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength, ellipsis, allowGraphemeOverflow: true);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Surrogate-pair emoji are not split by the truncator.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmojiSurrogatePair_NotSplit()
		{
			// Arrange.
			string value = "😀😀😀";
			int maxLength = 2;
			string ellipsis = "…";
			string expected = string.Concat(value.AsSpan(0, 2), ellipsis); // First emoji + ellipsis.

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength, ellipsis, allowGraphemeOverflow: true);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// ZWJ emoji family sequences are preserved as a whole when truncating.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TruncateWithEllipsis_ZwjSequence_Preserved()
		{
			// Arrange.
			string value = "👩‍👩‍👧‍👦ABC";
			int maxLength = 2;
			string ellipsis = "…";

			// Determine the first text element boundary to build expected value.
			int[] indexes = StringInfo.ParseCombiningCharacters(value);
			int firstElementEnd = indexes.Length > 1 ? indexes[1] : value.Length;
			string expectedPrefix = value.Substring(0, firstElementEnd);
			string expected = expectedPrefix + ellipsis;

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength, ellipsis, allowGraphemeOverflow: true);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Passing a null ellipsis behaves like an empty ellipsis (no exception and truncation works).
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullEllipsis_TreatedAsEmpty()
		{
			// Arrange.
			string value = "abcdef";
			int maxLength = 4;

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength, ellipsis: null!);

			// Assert.
			Assert.Equal("abcd", result);
		}

		/// <summary>
		/// Custom multi-character ellipsis is respected in keep calculation.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CustomMultiCharEllipsis_Works()
		{
			// Arrange.
			string value = "HelloWorld";
			string ellipsis = "...";
			int maxLength = 5; // Keep = 2 (strict mode, default).
			string expected = "He..."; // Strict mode: only 2 chars from value + 3 char ellipsis = 5 total.

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength, ellipsis);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// When maxLength equals the ellipsis length, a truncated/complete ellipsis is returned.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MaxLengthEqualsEllipsis_ReturnsEllipsis()
		{
			// Arrange.
			string value = "abcdef";
			int maxLength = 2; // Default ellipsis is "..".
			string expected = "..";

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// When maxLength is smaller than a custom ellipsis length, the ellipsis is truncated to fit.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MaxLengthLessThanEllipsis_TruncatesEllipsis()
		{
			// Arrange.
			string value = "abcdef";
			string ellipsis = "***";
			int maxLength = 2;
			string expected = "**";

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength, ellipsis);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// When allowGraphemeOverflow is true, including the whole grapheme causes the returned string to exceed maxLength and the grapheme is preserved intact.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void PreserveGrapheme_MayExceedMaxLength()
		{
			// Arrange.
			string value = "A" + "e\u0301" + "C"; // "AéC".
			string ellipsis = "…";
			int maxLength = 3; // Keep = 2 -> last grapheme (é) crosses keep boundary.
			string expectedGrapheme = "A" + "e\u0301";

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength, ellipsis, allowGraphemeOverflow: true);

			// Assert.
			Assert.StartsWith(expectedGrapheme, result);
			Assert.EndsWith(ellipsis, result);
			Assert.True(result!.Length > maxLength, "Expected returned string to exceed maxLength when preserving grapheme.");
		}

		/// <summary>
		/// Empty input string returns the empty string.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyString_ReturnsEmpty()
		{
			// Arrange.
			string value = string.Empty;
			int maxLength = 5;

			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength);

			// Assert.
			Assert.Equal(string.Empty, result);
		}

		/// <summary>
		/// <see cref="Truncator.TruncateNullableWithEllipsis"/>
		/// edge case tests covering various Unicode, boundary and overflow scenarios.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("", "abc", -5, "..", false)] // Negative maxLength returns empty string.
		[InlineData("", "abc", 0, "..", false)] // Zero maxLength returns empty string.
		[InlineData("a..", "abc", 3, "..", false)] // Value longer than maxLength gets truncated.
		[InlineData("AB", "AB", 3, "…", false)] // Value fits within keep, no truncation.
		[InlineData("..", "😀", 2, "..", false)] // Emoji doesn't fit in strict mode (keep=0), returns ellipsis.
		[InlineData("....", "abc", 4, "......", false)] // Long ellipsis with small maxLength, ellipsis truncated.
		[InlineData("ab......", "abc", 7, "......", true)] // Overflow mode: 'b' crosses boundary but included.
		[InlineData("", "", 5, "..", false)] // Empty string returns empty.
		[InlineData("🇺🇸🇬🇧A…", "🇺🇸🇬🇧ABC", 9, "…", true)] // Regional indicator flags (2 flags = 8 units) + 'A' (1 unit) + ellipsis.
		[InlineData("👋🏻A…", "👋🏻ABC", 5, "…", true)] // Emoji with skin tone modifier + 'A', overflow mode.
		[InlineData("é̃ñABC", "é̃ñABC", 7, "…", true)] // Value fits within keep (6), returns original without ellipsis.
		[InlineData("…", "😀", 2, "…", false)] // Surrogate pair doesn't fit strict mode (keep=1), returns ellipsis.
		[InlineData("😀😀", "😀😀", 5, "…", true)] // Two emojis (4 units) fit within keep (4), returns original without ellipsis.
		[InlineData("Red c", "Red cars are awesome!", 5, "", false)] // Empty ellipsis, truncates to maxLength.
		public void EdgeCases(string expected, string value, int maxLength, string ellipsis, bool allowGraphemeOverflow)
		{
			// Act.
			string? result = Truncator.TruncateNullableWithEllipsis(value, maxLength, ellipsis, allowGraphemeOverflow);

			// Assert.
			Assert.Equal(expected, result);
		}
	}
}