namespace Elephant.Texts.Tests
{
	/// <summary>
	/// <see cref="Pangram"/> tests.
	/// </summary>
	public class PangramTests
	{
		/// <inheritdoc cref="IPangram"/>
		private static readonly IPangram _pangram = new Pangram();

		/// <summary>Classic "quick brown fox" pangram.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void ClassicPangram() => Assert.True(_pangram.IsValid("The quick brown fox jumps over the lazy dog"));

		/// <summary>Classic "quick brown fox" pangram with a dot at the end.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void ClassicPangramWithDot() => Assert.True(_pangram.IsValid("The quick brown fox jumps over the lazy dog."));

		/// <summary>Perfect pangram (contains each letter exactly once).</summary>
		[Fact][SpeedVeryFast, UnitTest] public void PerfectPangram() => Assert.True(_pangram.IsValid("Mr Jock, TV quiz PhD, bags few lynx"));

		/// <summary>Pangram containing numbers and punctuation.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void NumericPangram() => Assert.True(_pangram.IsValid("123abcdefghijklm nopqrstuvwxyz!!!"));

		/// <summary>Empty string input.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void EmptyString() => Assert.False(_pangram.IsValid(""));

		/// <summary>Input shorter than 26 characters.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void ShortString() => Assert.False(_pangram.IsValid("abc"));

		/// <summary>Input with exactly 26 letters.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void Exactly26Chars() => Assert.True(_pangram.IsValid("abcdefghijklmnopqrstuvwxyz"));

		/// <summary>Uppercase letters.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void AllUpper() => Assert.True(_pangram.IsValid("ABCDEFGHIJKLMNOPQRSTUVWXYZ"));

		/// <summary>Mixed case letters.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void MixedCase() => Assert.True(_pangram.IsValid("AbCdEfGhIjKlMnOpQrStUvWxYz"));

		/// <summary>Input missing one letter (a).</summary>
		[Fact][SpeedVeryFast, UnitTest] public void MissingOneLetter() => Assert.False(_pangram.IsValid("bcdefghijklmnopqrstuvwxyz"));

		/// <summary>Input with only one repeated letter.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void AllAs() => Assert.False(_pangram.IsValid(new string('a', 100)));

		/// <summary>Non-Latin Unicode characters.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void UnicodeNonLetters() => Assert.False(_pangram.IsValid("你好世界 αβγδεζηθ ικλμνξο"));

		/// <summary>Unicode mixed with valid pangram.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void UnicodeWithPangram() => Assert.True(_pangram.IsValid("你好The quick brown fox jumps over the lazy dog世界"));

		/// <summary>Early termination after finding all letters.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void EarlyTermination() => Assert.True(_pangram.IsValid("abcdefghijklmnopqrstuvwxyz" + new string('x', 1000)));

		/// <summary>Long non-pangram input.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void LongNonPangram() => Assert.False(_pangram.IsValid(new string('x', 10000)));

		/// <summary>Input containing all letters except one.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void AllCharsExceptOne() => Assert.False(_pangram.IsValid("abcdefghijklmnoprstuvwxyz"));

		/// <summary>All ASCII characters.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void EveryAsciiChar() => Assert.True(_pangram.IsValid(new string(Enumerable.Range(0, 128).Select(i => (char)i).ToArray())));

		/// <summary>All ASCII characters excluding a-z and A-Z.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void NonLetterAscii() => Assert.False(_pangram.IsValid(string.Concat(Enumerable.Range(0, 128).Where(i => i < 'A' || (i > 'Z' && i < 'a') || i > 'z').Select(i => (char)i))));

		/// <summary>All control characters.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void ControlCharsOnly() => Assert.False(_pangram.IsValid(new string(Enumerable.Range(0, 32).Select(i => (char)i).ToArray())));

		/// <summary>All characters except the 'z'.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void MissingLetters() => Assert.False(_pangram.IsValid("abcdefghijklmnopqrstuvwxy"));

		/// <summary>Very long string with valid pangram prefix.</summary>
		[Fact][SpeedVeryFast, UnitTest] public void LongTextWithPangramPrefix() => Assert.True(_pangram.IsValid(new System.Text.StringBuilder().Append("abcdefghijklmnopqrstuvwxyz").Append('x', 1_000_000).ToString()));

		/// <summary>Very long string with valid pangram suffix.</summary>
		[Fact][SpeedFast, UnitTest] public void LongTextWithPangramSuffix() => Assert.True(_pangram.IsValid(new System.Text.StringBuilder().Append('x', 1_000_000).Append("abcdefghijklmnopqrstuvwxyz").ToString()));

		/// <summary>Very long non-pangram string.</summary>
		[Fact][SpeedFast, UnitTest] public void MillionCharsNonPangram() => Assert.False(_pangram.IsValid(new string('x', 1_000_000)));
	}
}