namespace Elephant.Texts.Abstractions
{
	/// <summary>
	/// Extremely optimized pangram (see: https://en.wikipedia.org/wiki/Pangram) detection functionality.
	/// A pangram is a sentence containing every letter of the alphabet at least once.
	/// This implementation uses bit manipulation for maximum performance.
	/// </summary>
	public interface IPangram
	{
		/// <summary>
		/// Returns whether the input string <paramref name="sentence"/> is a pangram.
		/// </summary>
		/// <param name="sentence">String to check for pangram status.</param>
		/// <returns>
		/// <c>true</c> if the input contains all letters of the English alphabet (case-insensitive).
		/// </returns>
		/// <remarks>
		/// This implementation:
		/// - Is case-insensitive.
		/// - Ignores non-Latin-alphabetic characters.
		/// - Uses bit manipulation for optimal performance.
		/// - Has early termination when all letters are found.
		/// - Has various small micro optimizations but doesn't use unsafe code.
		/// </remarks>
		bool IsValid(string? sentence);
	}
}