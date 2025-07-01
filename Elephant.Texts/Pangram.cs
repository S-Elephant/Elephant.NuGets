using Elephant.Texts.Abstractions;

namespace Elephant.Texts
{
	/// <inheritdoc/>
	public class Pangram : IPangram
	{
		/// <summary>
		/// Bitmask representing all 26 letters of the English alphabet (A-Z).
		/// The least significant 26 bits are set (0x03FFFFFF in hexadecimal).
		/// </summary>
		private const uint AllLettersMask = 0x03FFFFFF;

		/// <inheritdoc/>
		public bool IsValid(string? sentence)
		{
			if (sentence == null)
				return false;

			int letterFlags = 0;
			foreach (char c in sentence)
			{
				// Check if character is a letter (A-Z or a-z).
				if (c >= 'A' && c <= 'Z')
				{
					// Set the corresponding bit for this letter.
					// Example: 'C' (ASCII 67) → 67 - 65 = 2 → 1 << 2 = 0b00000100
					letterFlags |= 1 << (c - 'A');
				}
				// Check if character is a lowercase letter (a-z).
				else if (c >= 'a' && c <= 'z')
				{
					// Set the corresponding bit (same as uppercase but for lowercase).
					// Example: 'c' (ASCII 99) → 99 - 97 = 2 → 1 << 2 = 0b00000100
					letterFlags |= 1 << (c - 'a');
				}

				// Early exit if all letters found, improving performance for longer sentences.
				if (letterFlags == 0x03FFFFFF)
					return true;
			}

			// Return true if all required letters are present.
			return letterFlags == 0x03FFFFFF;
		}
	}
}