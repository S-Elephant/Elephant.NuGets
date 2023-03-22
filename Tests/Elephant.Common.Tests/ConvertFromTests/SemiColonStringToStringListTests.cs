using System;

namespace Elephant.Common.Tests.ConvertFromTests
{
	/// <summary>
	/// <see cref="ConvertFrom.SemiColonStringToStringList"/> tests.
	/// </summary>
	public class SemiColonStringToStringListTests
	{
		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SimpleConversion()
		{
			string value = "a;b;c;d";

			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value);

			Assert.Equal(new List<string> { "a", "b", "c", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ConversionWithEmptyValuesRemoved()
		{
			string value = "a;b;;c;;;d";

			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value);

			Assert.Equal(new List<string> { "a", "b", "c", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ConversionWithEmptyValues()
		{
			string value = "a;b;;c;;;d";

			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value, StringSplitOptions.None);

			Assert.Equal(new List<string> { "a", "b", "", "c", "", "", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyReturnsEmpty()
		{
			string value = string.Empty;

			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value);

			Assert.Equal(new List<string>(), convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyReturnsOneEmptyString()
		{
			string value = string.Empty;

			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(value, StringSplitOptions.None);

			Assert.Equal(new List<string> { string.Empty }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.SemiColonStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullReturnsOneEmptyString()
		{
			List<string> convertedValues = ConvertFrom.SemiColonStringToStringList(null);

			Assert.Equal(new List<string>(), convertedValues);
		}
	}
}
