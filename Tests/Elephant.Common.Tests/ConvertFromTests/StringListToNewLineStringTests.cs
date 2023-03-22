using System;

namespace Elephant.Common.Tests.ConvertFromTests
{
	/// <summary>
	/// <see cref="ConvertFrom.StringListToNewLineString"/> tests.
	/// </summary>
	public class StringListToNewLineStringTests
	{
		/// <summary>
		/// <see cref="ConvertFrom.StringListToNewLineString"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SimpleConversion()
		{
			List<string> values = new() { "a", "b", "c", "d" };

			string convertedValue = ConvertFrom.StringListToNewLineString(values);

			Assert.Equal($"a{Environment.NewLine}b{Environment.NewLine}c{Environment.NewLine}d", convertedValue);
		}

		/// <summary>
		/// <see cref="ConvertFrom.StringListToNewLineString"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyConversion()
		{
			List<string> values = new();

			string convertedValue = ConvertFrom.StringListToNewLineString(values);

			Assert.Equal(string.Empty, convertedValue);
		}

		/// <summary>
		/// <see cref="ConvertFrom.StringListToNewLineString"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullReturnsOneEmptyString()
		{
			string convertedValue = ConvertFrom.StringListToNewLineString(null);

			Assert.Equal(string.Empty, convertedValue);
		}
	}
}
