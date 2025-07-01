namespace Elephant.Texts.Abstractions
{
	/// <summary>
	/// An anagram is a word or phrase formed by rearranging the letters of a different word or phrase,
	/// typically by using all the original letters exactly once. I.e., the word anagram can be
	/// rearranged into the phrase "nag a r a m".
	/// </summary>
	public interface IAnagram
	{
		/// <summary>
		/// Determines if two strings are valid anagrams, with an option for strict or partial matching.
		/// </summary>
		/// <param name="source">Source string.</param>
		/// <param name="target">Target string to compare against.</param>
		/// <param name="useAllLetters">
		/// If true, requires exact anagram (all letters must be used exactly once with same counts).
		/// If false, allows partial anagram (target must contain at least the letters from source).
		/// Default is true (exact match required).
		/// </param>
		/// <returns>
		/// True if the strings meet the anagram criteria based on the specified mode.
		/// Returns false if either source or target is null.
		/// Returns true if both strings are empty after normalization (including whitespace/punctuation-only strings).
		/// Returns false if one string is empty/normalized-empty and the other is not.
		/// </returns>
		public bool IsValid(string? source, string? target, bool useAllLetters = true);
	}
}
