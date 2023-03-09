using System;

namespace Elephant.Common.Tests.ConvertFromTests
{
	/// <summary>
	/// <see cref="ConvertFrom.NewLineStringToStringList"/> tests.
	/// </summary>
	public class NewLineStringToStringListTests
	{
		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SimpleConversion()
		{
			string value = $"a\nb\r\nc{Environment.NewLine}d";

			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			Assert.Equal(new List<string> { "a", "b", "c", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ConversionWithEmptyValuesRemoved()
		{
			string value = $"a\nb\n\nc\n\r\n{Environment.NewLine}d";

			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			Assert.Equal(new List<string> { "a", "b", "c", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ConversionWithEmptyValues()
		{
			string value = $"a\nb\n\nc\n\r\n{Environment.NewLine}d";

			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value, StringSplitOptions.None);

			Assert.Equal(new List<string> { "a", "b", "", "c", "", "", "d" }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyReturnsEmpty()
		{
			string value = string.Empty;

			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value);

			Assert.Equal(new List<string>(), convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyReturnsOneEmptyString()
		{
			string value = string.Empty;

			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(value, StringSplitOptions.None);

			Assert.Equal(new List<string> { string.Empty }, convertedValues);
		}

		/// <summary>
		/// <see cref="ConvertFrom.NewLineStringToStringList"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullReturnsOneEmptyString()
		{
			List<string> convertedValues = ConvertFrom.NewLineStringToStringList(null);

			Assert.Equal(new List<string>(), convertedValues);
		}
	}
}
