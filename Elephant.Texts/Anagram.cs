using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Elephant.Texts.Abstractions;

namespace Elephant.Texts
{
	/// <inheritdoc/>
	public class Anagram : IAnagram
	{
		/// <inheritdoc/>
		public bool IsValid(string? source, string? target, bool useAllLetters = true)
		{
			// Null check: If either string is null, they cannot be anagrams.
			if (source == null || target == null)
				return false;

			// Normalize both strings (lowercase, letters only, Unicode normalized).
			string normalizedSource = Normalize(source);
			string normalizedTarget = Normalize(target);

			// Empty string handling: only return true if BOTH strings are empty after normalization
			if (normalizedSource.Length == 0 || normalizedTarget.Length == 0)
				return normalizedSource.Length == 0 && normalizedTarget.Length == 0;

			// For exact anagram mode, lengths must match.
			if (useAllLetters && normalizedSource.Length != normalizedTarget.Length)
				return false;

			// Create frequency dictionaries for both strings.
			// Key: character, Value: occurrence count.
			Dictionary<char, int> sourceCounts = normalizedSource.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
			Dictionary<char, int> targetCounts = normalizedTarget.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

			// Partial anagram check (useAllLetters = false).
			if (!useAllLetters)
			{
				// Verify target contains at least the same count of each source character.
				return sourceCounts.All(kvp => targetCounts.TryGetValue(kvp.Key, out int count) && count >= kvp.Value);
			}

			// Exact anagram check (useAllLetters = true).
			// Must have same character counts and same total unique characters.
			return sourceCounts.Count == targetCounts.Count &&
				   sourceCounts.All(kvp => targetCounts.TryGetValue(kvp.Key, out int count) && count == kvp.Value);
		}

		/// <summary>
		/// Normalizes a string by converting to lowercase, removing non-letter characters,
		/// and applying Unicode normalization to handle accented characters consistently.
		/// </summary>
		/// <param name="stringToNormalize">Input string to normalize.</param>
		/// <returns>
		/// A normalized string containing only lowercase letters with proper Unicode handling,
		/// or an empty string if no letters were present in the input.
		/// </returns>
		/// <remarks>
		/// This method:
		/// 1. Decomposes Unicode characters (NormalizationForm.FormD) to handle accented characters.
		/// 2. Filters to only include uppercase and lowercase letters (ignores numbers, punctuation, etc.).
		/// 3. Converts all letters to lowercase invariant (culture-insensitive).
		/// </remarks>
		private static string Normalize(string stringToNormalize)
		{
			return new string(stringToNormalize.Normalize(NormalizationForm.FormD) // Handle Unicode decomposition for consistent canonical comparison. Decomposes Unicode characters into their base characters + combining marks. Example: "é" becomes "e" + "´". "ñ" becomes "n\u0303".
				.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter ||
							CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter)
				.Select(char.ToLowerInvariant)
				.ToArray());
		}
	}
}
