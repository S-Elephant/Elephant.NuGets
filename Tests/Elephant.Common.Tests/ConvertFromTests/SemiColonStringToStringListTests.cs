using System;

namespace Elephant.Common.Tests.ConvertFromTests
{
	/// <summary>
	/// <see cref="ConvertFrom.SemiColonStringToStringList"/> tests.
	/// </summary>
	public class SemiColonStringToStringListTests
	{
		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> splits semicolon-separated values into list.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SimpleConversion()
		{
			// Arrange.
			string value = "a;b;c;d";

			// Act.
			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> with default options removes
		/// empty entries between consecutive semicolons.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ConversionWithEmptyValuesRemoved()
		{
			// Arrange.
			string value = "a;b;;c;;;d";

			// Act.
			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> with <see cref="StringSplitOptions.None"/>
		/// preserves empty entries between consecutive semicolons.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ConversionWithEmptyValues()
		{
			// Arrange.
			string value = "a;b;;c;;;d";

			// Act.
			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value, StringSplitOptions.None);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "", "c", "", "", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> with default options returns empty list for empty string input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyReturnsEmpty()
		{
			// Arrange.
			string value = string.Empty;

			// Act.
			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value);

			// Assert.
			Assert.Equal(new List<string>(), convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> with <see cref="StringSplitOptions.None"/>
		/// returns single-item list containing empty string for empty string input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyReturnsOneEmptyString()
		{
			// Arrange.
			string value = string.Empty;

			// Act.
			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value, StringSplitOptions.None);

			// Assert.
			Assert.Equal(new List<string> { string.Empty }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> returns empty list for null input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullReturnsOneEmptyString()
		{
			// Act.
			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(null);

			// Assert.
			Assert.Equal(new List<string>(), convertedValues);
		}
	}
}
