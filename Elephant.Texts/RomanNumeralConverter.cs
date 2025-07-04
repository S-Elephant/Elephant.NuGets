using System;
using System.Collections.Generic;
using System.Text;
using Elephant.Texts.Abstractions;

namespace Elephant.Texts
{
	/// <inheritdoc/>
	public class RomanNumeralConverter : IRomanNumeralConverter
	{
		/// <summary>
		/// At this value, the Roman large numbers start.
		/// The Roman default/small numbers range from 0-3999 (orginally it used to be 1-3999 instead).
		/// </summary>
		public const int RomanLargeNumberStart = MaxSmallRomanIntValue + 1;

		/// <summary>
		/// Minimum Roman int value.
		/// </summary>
		public const int MinRomanIntValue = 0;

		/// <summary>
		/// Maximum roman int value.
		/// </summary>
		public const int MaxSmallRomanIntValue = 3999;

		/// <summary>
		/// Maximum roman value before the Roman large numbers start.
		/// </summary>
		public const string MaxSmallRomanValue = "MMMCMXCIX";

		/// <inheritdoc/>
		public string Zero { get; set; }

		/// <inheritdoc/>
		public char CombiningOverline { get; set; }

		/// <summary>
		/// Array of integer values representing Roman numeral components in descending order.
		/// </summary>
		/// <remarks>
		/// Contains subtractive combinations (like CM for 900) and single-symbol values.
		/// Ordered from largest (900) to smallest (1) for efficient conversion.
		/// </remarks>
		private static readonly int[] NumeralComponentsDesc = { 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

		/// <summary>
		/// Array of Roman numeral symbols corresponding to the <see cref="NumeralComponentsDesc"/>.
		/// </summary>
		/// <remarks>
		/// Maintains exact index alignment with the Values array.
		/// Includes both standard symbols (D, L, V) and subtractive combinations (CM, CD, etc.).
		/// </remarks>
		private static readonly string[] NumeralSymbols = { "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

		/// <summary>
		/// Apostrophus lookup dictionary for the thousands values.
		/// </summary>
		private static readonly Dictionary<char, string> ApostrophusThousandsLookup = new()
		{
			{ 'I', "|Ↄ" },
			{ 'V', "|Ↄ|Ↄ" },
			{ 'X', "CC|Ↄ|Ↄ" },
			{ 'L', "|Ↄ|ↃↃ" },
			{ 'C', "CCC|Ↄ|ↃↃ" },
			{ 'D', "|Ↄ|ↃↃↃ" },
			{ 'M', "CCCC|Ↄ|ↃↃↃ" },
		};

		/// <summary>Constructor.</summary>
		public RomanNumeralConverter(string zero = "nulla", char combiningOverline = '\u0305')
		{
			Zero = zero;
			CombiningOverline = combiningOverline;
		}

		/// <inheritdoc/>
		public string IntToRoman(int intValue, RomanLargeNumberFormatType format = RomanLargeNumberFormatType.MPrefix)
		{
			if (intValue == 0)
				return Zero;

			if (intValue < 0)
				throw new ArgumentOutOfRangeException(nameof(intValue), "Roman numerals can only represent zero and positive integers.");

			// This can be done using the large-roman code but the ConvertSmallRoman() method is more efficient.
			if (intValue < RomanLargeNumberStart)
				return ConvertSmallRoman(intValue);

			StringBuilder sb = new();
			int magnitude = 0;
			int remaining = intValue;

			while (remaining > 0)
			{
				int chunk = remaining % 1000;
				remaining /= 1000;

				if (chunk > 0)
				{
					string chunkRoman = ConvertChunkToRoman(chunk);

					// Handle large numbers (>= 1000).
					if (magnitude > 0)
					{
						switch (format)
						{
							case RomanLargeNumberFormatType.MPrefix:
								// Just append M's (limited to 3999).
								sb.Insert(0, new string('M', chunk));
								break;

							case RomanLargeNumberFormatType.Overline:
								// Use Unicode combining overline (V̅, X̅, etc.).
								sb.Insert(0, ApplyOverlines(chunkRoman));
								break;

							case RomanLargeNumberFormatType.Parentheses:
								// Enclose in parentheses (e.g., (V) = 5000).
								sb.Insert(0, ")");
								sb.Insert(0, chunkRoman);
								sb.Insert(0, "(");
								break;

							case RomanLargeNumberFormatType.Apostrophus:
								// Use ancient apostrophus notation (|Ↄ|Ↄ = 5000).
								sb.Insert(0, ConvertToApostrophus(chunkRoman, magnitude));
								break;
						}
					}
					else
					{
						sb.Insert(0, chunkRoman);
					}
				}

				magnitude++;
			}

			return sb.ToString();
		}

		#region Int to Roman

		/// <summary>
		/// Convert an integer value between 1 and 3999 to its Roman numeral representation.
		/// </summary>
		/// <param name="intValue">Integer value to convert. Must be inclusive between 0-3999.</param>
		/// <returns>Roman numeral representation of the input value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="intValue"/> is negative or if it's greater than 3999.</exception>
		/// <remarks>
		/// <para>This optimized implementation:</para>
		/// <list type="bullet">
		///   <item><description>Provides immediate returns for 15 common single-character values.</description></item>
		///   <item><description>Uses hybrid division/subtraction algorithm for remaining values.</description></item>
		///   <item><description>Generates results with minimal memory allocations.</description></item>
		///   <item><description>Maintains O(1) performance for all valid inputs.</description></item>
		/// </list>
		/// <para>Roman numeral rules followed:</para>
		/// <list type="bullet">
		///   <item><description>Additive notation for III (3) and VII (7).</description></item>
		///   <item><description>Subtractive notation for IV (4) and IX (9).</description></item>
		///   <item><description>Maximum three consecutive identical symbols.</description></item>
		/// </list>
		/// </remarks>
		/// <example>
		/// Console.WriteLine(ConvertSmallRoman(42)); // Output: "XLII"
		/// Console.WriteLine(ConvertSmallRoman(1987)); // Output: "MCMLXXXVII"
		/// </example>
		private string ConvertSmallRoman(int intValue)
		{
			if (intValue == 0)
				return Zero;

			if (intValue < 0 || intValue >= RomanLargeNumberStart)
				throw new ArgumentOutOfRangeException(nameof(intValue), $"Value must be less than {RomanLargeNumberStart} but got {intValue}.");

			// Fast path for single-character values.
			switch (intValue)
			{
				case 1: return "I";
				case 4: return "IV";
				case 5: return "V";
				case 9: return "IX";
				case 10: return "X";
				case 40: return "XL";
				case 50: return "L";
				case 90: return "XC";
				case 100: return "C";
				case 400: return "CD";
				case 500: return "D";
				case 900: return "CM";
				case 1000: return "M";
			}

			// Optimized for minimal allocations.
			StringBuilder sb = new(15); // MMMDCCCLXXXVIII (3888) is longest.

			// Process the thousands.
			if (intValue >= 1000)
			{
				int thousands = intValue / 1000;
				sb.Append('M', thousands);
				intValue %= 1000;
			}

			// Process remaining using subtraction principle.
#pragma warning disable SA1501 // Statement should not be on a single line. Suppressed for clarity.
#pragma warning disable SA1107 // Code should not contain multiple statements on one line. Suppressed for clarity.
			while (intValue >= 900) { sb.Append("CM"); intValue -= 900; }
			while (intValue >= 500) { sb.Append('D'); intValue -= 500; }
			while (intValue >= 400) { sb.Append("CD"); intValue -= 400; }
			while (intValue >= 100) { sb.Append('C'); intValue -= 100; }
			while (intValue >= 90) { sb.Append("XC"); intValue -= 90; }
			while (intValue >= 50) { sb.Append('L'); intValue -= 50; }
			while (intValue >= 40) { sb.Append("XL"); intValue -= 40; }
			while (intValue >= 10) { sb.Append('X'); intValue -= 10; }
			while (intValue >= 9) { sb.Append("IX"); intValue -= 9; }
			while (intValue >= 5) { sb.Append('V'); intValue -= 5; }
			while (intValue >= 4) { sb.Append("IV"); intValue -= 4; }
			while (intValue >= 1) { sb.Append('I'); intValue -= 1; }
#pragma warning restore SA1107 // Code should not contain multiple statements on one line.
#pragma warning restore SA1501 // Statement should not be on a single line.

			return sb.ToString();
		}

		/// <summary>
		/// Converts a number chunk (0-3999) to its Roman numeral representation.
		/// </summary>
		/// <param name="chunkIntValue">Chunk integer value to convert. Must be in the inclusive range: 0-3999.</param>
		/// <returns>Roman numeral string for the chunk.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="chunkIntValue"/> > 3999.</exception>
		private string ConvertChunkToRoman(int chunkIntValue)
		{
			if (chunkIntValue == 0)
				return Zero;

			if (chunkIntValue > 3999)
				throw new ArgumentOutOfRangeException(nameof(chunkIntValue), $"Chunk value must be between 0-3999 but got {chunkIntValue}.");

			StringBuilder sb = new(15);

			// Process the thousands.
			int thousands = chunkIntValue / 1000;
			if (thousands > 0)
			{
				sb.Append('M', thousands);
				chunkIntValue %= 1000;
			}

			// Standard conversion for 1-999.
			for (int i = 0; i < NumeralComponentsDesc.Length && chunkIntValue > 0; i++)
			{
				int value = NumeralComponentsDesc[i];
				if (chunkIntValue < value) continue;

				int count = chunkIntValue / value;
				chunkIntValue %= value;

				// Append the symbol repeated 'count' times.
				string symbol = NumeralSymbols[i];
				if (count == 1)
				{
					sb.Append(symbol);
				}
				else
				{
					// Only needed for C (100) which can appear up to 3 times (300).
					sb.Append(symbol[0], count);
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Applies Unicode combining overlines to each character in a Roman numeral string.
		/// </summary>
		/// <param name="roman">Roman numeral string to modify.</param>
		/// <returns>String with overlines applied to each character.</returns>
		private string ApplyOverlines(string roman)
		{
			if (string.IsNullOrEmpty(roman))
				return roman;

			// Pre-allocate exact capacity needed (2 chars per input char).
			StringBuilder sb = new(roman.Length * 2);

			for (int i = 0; i < roman.Length; i++)
			{
				sb.Append(roman[i]);
				sb.Append(CombiningOverline);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Convert a Roman numeral chunk to ancient apostrophus notation.
		/// </summary>
		/// <param name="roman">Roman numeral string to convert.</param>
		/// <param name="magnitude">Magnitude (power of 1000) of the number.</param>
		/// <returns>Number in apostrophus notation.</returns>
		private static string ConvertToApostrophus(string roman, int magnitude)
		{
			if (string.IsNullOrEmpty(roman))
				return roman;

			if (magnitude != 1)
				return roman; // Only handle thousands (magnitude 1)

			// Ancient Roman notation for large numbers (|Ↄ|Ↄ = 5000, CC|Ↄ|Ↄ = 10000).

			// Pre-allocate with estimated capacity.
			StringBuilder sb = new(roman.Length * 5);

			// For simplicity, this handles magnitudes up to 1,000,000.
			// Thousands.
			for (int i = 0; i < roman.Length; i++)
			{
				char c = roman[i];
				if (ApostrophusThousandsLookup.TryGetValue(c, out string replacement))
					sb.Append(replacement);
				else
					sb.Append(c);
			}

			return sb.ToString();
		}

		#endregion

		/// <inheritdoc/>
		public int SmallRomanToInt(string romanValue)
		{
			if (string.IsNullOrWhiteSpace(romanValue) || romanValue.ToLowerInvariant() == Zero.ToLowerInvariant())
				return 0;

			romanValue = romanValue.ToUpperInvariant().Trim();
			int total = 0;
			int prevValue = 0;

			// Process from right to left for correct subtraction handling.
			for (int i = romanValue.Length - 1; i >= 0; i--)
			{
				int currentValue = RomanCharToInt(romanValue[i]);

				// Apply subtractive notation if needed.
				total += (currentValue < prevValue) ? -currentValue : currentValue;
				prevValue = currentValue;
			}

			// Validate the result is within standard Roman range.
			if (total < 1 || total > 3999)
				throw new ArgumentOutOfRangeException("Value must be between 1 and 3999.");

			return total;
		}

		/// <summary>
		/// Converts a single Roman numeral character to its integer value.
		/// </summary>
		/// <param name="c">Roman numeral character.</param>
		/// <returns>Roman character integer value.</returns>
		/// <exception cref="FormatException">Thrown for invalid Roman numeral characters.</exception>
		private int RomanCharToInt(char c)
		{
			return c switch
			{
				'I' => 1,
				'V' => 5,
				'X' => 10,
				'L' => 50,
				'C' => 100,
				'D' => 500,
				'M' => 1000,
				_ => throw new FormatException($"Invalid Roman numeral character: '{c}'")
			};
		}
	}
}
