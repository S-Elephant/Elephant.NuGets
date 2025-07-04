namespace Elephant.Texts.Tests
{
	/// <summary>
	/// <see cref="RomanNumeralConverter"/> tests.
	/// </summary>
	public class RomanNumeralConverterTests
	{
		/// <summary>
		/// System under test.
		/// </summary>
		private readonly IRomanNumeralConverter _romanNumeralConverter;

		/// <summary>
		/// Setup.
		/// </summary>
		public RomanNumeralConverterTests()
		{
			_romanNumeralConverter = new RomanNumeralConverter();
		}

		#region ToRoman

		/// <summary>
		/// Custom overline character tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(10000, "X+")]
		[InlineData(12345, "X+I+I+CCCXLV")]
		[InlineData(4000, "I+V+")]
		[InlineData(5000, "V+")]
		public void ToRoman_CustomOverline(int num, string expected)
		{
			// Arrange.
			IRomanNumeralConverter customRomanNumeralConverter = new RomanNumeralConverter(combiningOverline: '+');

			// Act.
			string result = customRomanNumeralConverter.IntToRoman(num, RomanLargeNumberFormatType.Overline);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Classic (Standard) Format Tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(1, "I")]
		[InlineData(4, "IV")]
		[InlineData(9, "IX")]
		[InlineData(49, "XLIX")]
		[InlineData(3999, "MMMCMXCIX")]
		[InlineData(5000, "MMMMM")]
		public void ToRoman_ClassicFormat_ValidNumbers(int num, string expected)
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(num, RomanLargeNumberFormatType.MPrefix);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Overline Format Tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(4000, "I\u0305V\u0305")]
		[InlineData(5000, "V\u0305")]
		[InlineData(10000, "X\u0305")]
		[InlineData(12345, "X\u0305I\u0305I\u0305CCCXLV")]
		public void ToRoman_OverlineFormat_LargeNumbers(int num, string expected)
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(num, RomanLargeNumberFormatType.Overline);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Parentheses Format Tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(4000, "(IV)")]
		[InlineData(5000, "(V)")]
		[InlineData(10000, "(X)")]
		[InlineData(15000, "(XV)")]
		public void ToRoman_ParenthesesFormat_LargeNumbers(int num, string expected)
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(num, RomanLargeNumberFormatType.Parentheses);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Apostrophus Format Tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(5000, "|Ↄ|Ↄ")]
		[InlineData(10000, "CC|Ↄ|Ↄ")]
		[InlineData(50000, "|Ↄ|ↃↃ")]
		public void ToRoman_ApostrophusFormat_LargeNumbers(int num, string expected)
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(num, RomanLargeNumberFormatType.Apostrophus);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Zero value tests for all large number formats.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(RomanLargeNumberFormatType.Overline)]
		[InlineData(RomanLargeNumberFormatType.Parentheses)]
		[InlineData(RomanLargeNumberFormatType.Apostrophus)]
		[InlineData(RomanLargeNumberFormatType.MPrefix)]
		public void ToRoman_Zero(RomanLargeNumberFormatType romanLargeNumberFormat)
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(0, romanLargeNumberFormat);

			// Assert.
			Assert.Equal(_romanNumeralConverter.Zero, result);
		}

		/// <summary>
		/// Custom zero value tests for all large number formats.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(RomanLargeNumberFormatType.Overline)]
		[InlineData(RomanLargeNumberFormatType.Parentheses)]
		[InlineData(RomanLargeNumberFormatType.Apostrophus)]
		[InlineData(RomanLargeNumberFormatType.MPrefix)]
		public void ToRoman_CustomZero(RomanLargeNumberFormatType romanLargeNumberFormat)
		{
			// Arrange.
			string customZero = "Zero pikachu's!";
			IRomanNumeralConverter customRomanNumeralConverter = new RomanNumeralConverter(customZero);

			// Act.
			string result = customRomanNumeralConverter.IntToRoman(0, romanLargeNumberFormat);

			// Assert.
			Assert.Equal(customZero, result);
		}

		/// <summary>
		/// Negative integer test should throw.
		/// </summary>
		[Theory]
		[InlineData(-1)]
		[InlineData(int.MinValue)]
		[SpeedVeryFast, UnitTest]
		public void ToRoman_Negative_ThrowsException(int negativeValue)
		{
			// Act and Assert.
			Assert.Throws<ArgumentOutOfRangeException>(() => _romanNumeralConverter.IntToRoman(negativeValue, RomanLargeNumberFormatType.MPrefix));
		}

		/// <summary>
		/// Mixed Magnitudes test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ToRoman_ComplexNumber_CombinesFormatsCorrectly()
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(1_234_567, RomanLargeNumberFormatType.Overline);

			// Assert.
			Assert.Equal("I\u0305C\u0305C\u0305X\u0305X\u0305X\u0305I\u0305V\u0305DLXVII", result.Normalize());
		}

		/// <summary>
		/// Extremely large number test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ToRoman_LargeNumber_MPrefix()
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(int.MaxValue);

			// Assert.
			Assert.Equal("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMDCXLVII", result);
		}

		/// <summary>
		/// Extremely large number test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ToRoman_LargeNumber_Overline()
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(int.MaxValue, RomanLargeNumberFormatType.Overline);

			// Assert.
			Assert.Equal("I̅I̅C̅X̅L̅V̅I̅I̅C̅D̅L̅X̅X̅X̅I̅I̅I̅DCXLVII", result);
		}

		/// <summary>
		/// Extremely large number test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ToRoman_LargeNumber_Parentheses()
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(int.MaxValue, RomanLargeNumberFormatType.Parentheses);

			// Assert.
			Assert.Equal("(II)(CXLVII)(CDLXXXIII)DCXLVII", result);
		}

		/// <summary>
		/// Extremely large number test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ToRoman_LargeNumber_Apostrophus()
		{
			// Act.
			string result = _romanNumeralConverter.IntToRoman(int.MaxValue, RomanLargeNumberFormatType.Apostrophus);

			// Assert.
			Assert.Equal("IICXLVIICCC|Ↄ|ↃↃ|Ↄ|ↃↃↃ|Ↄ|ↃↃCC|Ↄ|ↃCC|Ↄ|ↃCC|Ↄ|Ↄ|Ↄ|Ↄ|ↃDCXLVII", result);
		}

		#endregion

		#region SmallRomanToInt

		/// <summary>
		/// Tests that valid standard Roman numerals convert correctly.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("I", 1)]
		[InlineData("V", 5)]
		[InlineData("X", 10)]
		[InlineData("L", 50)]
		[InlineData("C", 100)]
		[InlineData("D", 500)]
		[InlineData("M", 1000)]
		[InlineData("III", 3)]
		[InlineData("IV", 4)]
		[InlineData("IX", 9)]
		[InlineData("LVIII", 58)]
		[InlineData("XC", 90)]
		[InlineData("CD", 400)]
		[InlineData("CM", 900)]
		[InlineData("MCMXCIV", 1994)]
		[InlineData(RomanNumeralConverter.MaxSmallRomanValue, RomanNumeralConverter.MaxSmallRomanIntValue)]
		public void RomanToInt_ValidNumerals_ReturnsCorrectValue(string roman, int expected)
		{
			// Act.
			int result = _romanNumeralConverter.SmallRomanToInt(roman);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Tests that the converter is case-insensitive.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("ix", 9)]
		[InlineData("mcmxcix", 1999)]
		[InlineData("XxV", 25)]
		public void RomanToInt_CaseInsensitive_ReturnsCorrectValue(string mixedCaseRoman, int expected)
		{
			// Act.
			int result = _romanNumeralConverter.SmallRomanToInt(mixedCaseRoman);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// Tests that empty/whitespace strings throw FormatException.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\t")]
		[InlineData("\n")]
		public void RomanToInt_NormalizedEmptyOrWhitespace_ReturnsZero(string emptyValue)
		{
			// Act and Assert.
			Assert.Equal(0, _romanNumeralConverter.SmallRomanToInt(emptyValue));
		}

		/// <summary>
		/// An input that is too large should throw an <see cref="ArgumentOutOfRangeException"/>.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RomanToInt_ThrowsIfInvalidInputRange()
		{
			// Act and Assert.
			Assert.Throws<ArgumentOutOfRangeException>(() => _romanNumeralConverter.SmallRomanToInt("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMDCXLVII"));
		}

		#endregion
	}
}
