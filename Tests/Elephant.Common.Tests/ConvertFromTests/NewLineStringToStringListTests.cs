using System;

namespace Elephant.Common.Tests.ConvertFromTests
{
	/// <summary>
	/// <see cref="ConvertFrom.NewLineStringToStringList"/> tests.
	/// </summary>
	public class NewLineStringToStringListTests
	{
		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> handles multiple
		/// newline formats (\n, \r\n, <see cref="Environment.NewLine"/>).
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SimpleConversion()
		{
			// Arrange.
			string value = $"a\nb\r\nc{Environment.NewLine}d";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with default options removes
		/// empty entries between consecutive newlines.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ConversionWithEmptyValuesRemoved()
		{
			// Arrange.
			string value = $"a\nb\n\nc\n\r\n{Environment.NewLine}d";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with <see cref="StringSplitOptions.None"/>
		/// preserves empty entries between consecutive newlines.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ConversionWithEmptyValues()
		{
			// Arrange.
			string value = $"a\nb\n\nc\n\r\n{Environment.NewLine}d";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value, StringSplitOptions.None);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "", "c", "", "", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with default options returns empty
		/// list for empty string input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyReturnsEmpty()
		{
			// Arrange.
			string value = string.Empty;

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string>(), convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with <see cref="StringSplitOptions.None"/>
		/// returns single-item list containing empty string for empty string input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyReturnsOneEmptyString()
		{
			// Arrange.
			string value = string.Empty;

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value, StringSplitOptions.None);

			// Assert.
			Assert.Equal(new List<string> { string.Empty }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> returns empty list for null input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullReturnsEmpty()
		{
			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(null);

			// Assert.
			Assert.Equal(new List<string>(), convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> returns single-item list when input contains no newlines.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SingleValueWithoutNewlines()
		{
			// Arrange.
			string value = "single value";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string> { "single value" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with default options removes leading newline.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void LeadingNewlineRemoved()
		{
			// Arrange.
			string value = "\na\nb\nc";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with <see cref="StringSplitOptions.None"/>
		/// preserves leading newline as empty string.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void LeadingNewlinePreserved()
		{
			// Arrange.
			string value = "\na\nb\nc";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value, StringSplitOptions.None);

			// Assert.
			Assert.Equal(new List<string> { "", "a", "b", "c" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with default options removes trailing newline.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TrailingNewlineRemoved()
		{
			// Arrange.
			string value = "a\nb\nc\n";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with <see cref="StringSplitOptions.None"/>
		/// preserves trailing newline as empty string.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void TrailingNewlinePreserved()
		{
			// Arrange.
			string value = "a\nb\nc\n";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value, StringSplitOptions.None);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c", "" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with default options
		/// removes all newlines, returning empty list.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void OnlyNewlinesRemoved()
		{
			// Arrange.
			string value = "\n\n\n";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string>(), convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> with <see cref="StringSplitOptions.None"/>
		/// preserves all newlines as empty strings.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void OnlyNewlinesPreserved()
		{
			// Arrange.
			string value = "\n\n\n";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value, StringSplitOptions.None);

			// Assert.
			Assert.Equal(new List<string> { "", "", "", "" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> preserves leading, trailing,
		/// and tab whitespace within values.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void PreservesWhitespace()
		{
			// Arrange.
			string value = " a \n  b  \n\tc";

			// Act.
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string> { " a ", "  b  ", "\tc" }, convertedValues);
		}
	}
}
