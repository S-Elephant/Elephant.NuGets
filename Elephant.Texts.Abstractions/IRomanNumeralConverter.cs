using System;

namespace Elephant.Texts.Abstractions
{
	/// <summary>
	/// Convert a modern integer number into a Roman number and vice versa.
	/// </summary>
	public interface IRomanNumeralConverter
	{
		/// <summary>
		/// Combining overline character that may be used for large Roman numerals.
		/// </summary>
		/// <value>
		/// Default is '\u0305' (=Combining Overline, a stripe near the top). Can be modified for alternative representations.
		/// </value>
		/// <remarks>
		/// Used to denote multiplication by 1000 when placed over symbols.
		/// Changing this property affects all subsequent numeral formatting.
		/// </remarks>
		char CombiningOverline { get; set; }

		/// <summary>
		/// The digit 0 was orginally not used Roman numeral system but scholars later used it and called it "nulla".
		/// </summary>
		string Zero { get; set; }

		/// <summary>
		/// Convert an integer to a Roman numeral string using the specified large number format.
		/// </summary>
		/// <param name="intValue">Integer to convert. Must be greater or equal to zero or it will throw a <see cref="ArgumentOutOfRangeException"/>.</param>
		/// <param name="format">Format for representing large numbers. Defaults to <see cref="RomanLargeNumberFormatType.MPrefix"/>.</param>
		/// <returns>Roman numeral representation of the number.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="intValue"/> is negative.</exception>
		string IntToRoman(int intValue, RomanLargeNumberFormatType format = RomanLargeNumberFormatType.MPrefix);

		/// <summary>
		/// Convert a Roman numeral string to its integer equivalent (0-3999).
		/// </summary>
		/// <param name="romanValue">Roman numeral string to convert (case-insensitive).</param>
		/// <returns>Converted integer value. Returns 0 if <paramref name="romanValue"/> is empty, white-space-only, null or <see cref="Zero"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when value is outside the valid Roman numeral range (1-3999).</exception>
		/// <exception cref="FormatException">Thrown for invalid numerals.</exception>
		/// <remarks>
		/// Supports all standard Roman numerals:
		/// I=1, V=5, X=10, L=50, C=100, D=500, M=1000
		/// Valid subtractive combinations: IV=4, IX=9, XL=40, XC=90, CD=400, CM=900
		/// </remarks>
		int SmallRomanToInt(string romanValue);
	}
}