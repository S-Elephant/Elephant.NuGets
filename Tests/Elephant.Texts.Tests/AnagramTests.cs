namespace Elephant.Texts.Tests
{
	/// <summary>
	/// <see cref="Anagram"/> tests.
	/// </summary>
	public class AnagramTests
	{
		/// <summary>
		/// System under test.
		/// </summary>
		private static readonly IAnagram _anagram = new Anagram();

		/// <summary>
		/// <see cref="Anagram.IsValid(string?, string?, bool)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("x", "", true, false)]
		[InlineData(null, null, true, false)]
		[InlineData(null, "test", true, false)]
		[InlineData("test", null, true, false)]
		// Standard anagrams.
		[InlineData("listen", "silent", true, true)]
		[InlineData("dormitory", "dirty room", true, true)]
		[InlineData("astronomer", "moon starer", true, true)]
		[InlineData("debit card", "bad credit", true, true)]
		[InlineData("new york times", "monkeys write", true, true)]
		[InlineData("eleven plus two", "twelve plus one", true, true)]
		// Case insensitivity.
		[InlineData("Tea", "Eat", true, true)]
		[InlineData("Clint Eastwood", "old west action", true, true)]
		[InlineData("PRESIDENT", "PREDESTIN", true, true)]
		// Partial anagrams (useAllLetters = false)
		[InlineData("cat", "tact", true, false)]
		[InlineData("cat", "tact", false, true)]
		[InlineData("bag", "garbage", true, false)]
		// Empty strings.
		[InlineData("", "", true, true)]
		[InlineData("", "a", true, false)]
		[InlineData("a", "", true, false)]
		[InlineData(" ", " ", true, true)] // Whitespace becomes empty after normalization.
		[InlineData("\t", "\n", true, true)] // Different whitespace becomes empty.
											 // Non-letter characters.
		[InlineData("a!", "!a", true, true)]
		[InlineData("a1b", "b1a", true, true)]
		[InlineData("a.b.c", "c.b.a", true, true)]
		[InlineData("a-b-c", "c-b-a", true, true)]
		[InlineData("a$b$c", "c$b$a", true, true)]
		// Unicode/special characters.
		[InlineData("café", "éfac", true, true)]
		[InlineData("résumé", "mésuré", true, true)]
		[InlineData("Schloß", "ßloSc", true, false)]
		// Different lengths.
		[InlineData("a", "aa", true, false)] // Partial anagram.
		[InlineData("abc", "abcd", true, false)]
		// Real-world examples.
		[InlineData("William Shakespeare", "I am a weakish speller", true, true)]
		[InlineData("Madam Curie", "Radium came", true, true)]
		[InlineData("The Morse Code", "Here come dots", true, true)]
		[InlineData("Slot machines", "Cash lost in me", true, true)]
		[InlineData("Snooze alarms", "Alas, no more z's", true, true)]
		[InlineData("A gentleman", "Elegant man", true, true)]
		[InlineData("Funeral", "Real fun", true, true)]
		[InlineData("The eyes", "They see", true, true)]
		[InlineData("A decimal point", "I'm a dot in place", true, true)]
		[InlineData("Eleven plus two", "Twelve plus one", true, true)]
		// False positives.
		[InlineData("hello", "world", true, false)]
		[InlineData("anagram", "pangram", true, false)]
		[InlineData("test", "tests", true, false)]
		[InlineData("apple", "peach", true, false)]
		[InlineData("abc", "def", true, false)]
		[InlineData("aabb", "abbb", true, false)] // Different letter counts.
		[InlineData("aab", "abb", true, false)] // Different letter counts.
		public void IsValidAnagramTests(string? source, string? target, bool useAllLetters, bool expected)
		{
			Assert.Equal(expected, _anagram.IsValid(source, target, useAllLetters));
		}
	}
}
