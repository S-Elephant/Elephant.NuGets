namespace Elephant.Texts.Abstractions
{
	/// <summary>
	/// A palindrome is a sequence of characters, such as a word, phrase,
	/// or series of symbols, that reads the same backward as forward.
	/// </summary>
	public interface IPalindrome
	{
		/// <summary>
		/// Checks if a sentence is a valid palindrome (ignores case, spaces, and non-alphanumeric characters).
		/// </summary>
		/// <param name="sentence">Sentence to check.</param>
		/// <returns>True if <paramref name="sentence"/> reads the same forwards and backwards or if <paramref name="sentence"/> is null or empty.</returns>
		/// <example>
		/// <![CDATA[IsValidPalindromeSentence("A man, a plan, a canal: Panama.") // returns true.
		/// IsValidPalindromeSentence("race a car") // returns false.]]>
		/// </example>
		bool IsValid(string? sentence);
	}
}