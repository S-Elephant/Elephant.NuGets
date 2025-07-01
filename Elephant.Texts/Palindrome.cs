using Elephant.Texts.Abstractions;

namespace Elephant.Texts
{
	/// <inheritdoc/>
	public class Palindrome : IPalindrome
	{
		/// <inheritdoc/>
		public bool IsValid(string? sentence)
		{
			// Null and empty strings are considered valid palindromes.
			if (string.IsNullOrEmpty(sentence))
				return true;

			// Initialize two indices:
			// left starts at the beginning of the string (index 0).
			// right starts at the end of the string (last index).
			int leftIndex = 0;
			int rightIndex = sentence!.Length - 1;

			// Loop until the pointers meet or cross each other.
			while (leftIndex < rightIndex)
			{
				// Left index handling:
				// Skip any characters that aren't letters or digits by moving the left index forward.
				// char.IsLetterOrDigit() checks if the character is A-Z, a-z, or 0-9.
				// We skip these because palindrome comparison only considers alphanumeric chars.
				while (leftIndex < rightIndex && !char.IsLetterOrDigit(sentence[leftIndex]))
				{
					leftIndex++;
				}

				// Right index handling:
				// Similarly skip non-alphanumeric characters from the end by moving right index backwards.
				while (leftIndex < rightIndex && !char.IsLetterOrDigit(sentence[rightIndex]))
				{
					rightIndex--;
				}

				// Compare characters (case-insensitive).
				// After skipping non-alphanumeric chars on both sides, compare the remaining characters.
				if (char.ToLower(sentence[leftIndex]) != char.ToLower(sentence[rightIndex]))
				{
					return false;
				}

				// Move both indices 1 step in their direction for the next comparison (if any).
				leftIndex++;
				rightIndex--;
			}

			return true;
		}
	}
}
