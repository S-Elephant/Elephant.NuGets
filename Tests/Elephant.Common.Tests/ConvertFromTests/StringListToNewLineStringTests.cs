using System;

namespace Elephant.Common.Tests.ConvertFromTests
{
	/// <summary>
	/// <see cref="ConvertFrom.StringListToNewLineString"/> tests.
	/// </summary>
	public class StringListToNewLineStringTests
	{
		/// <summary>
		/// <see cref="ConvertFrom.StringListToNewLineString"/> joins list items with <see cref="Environment.NewLine"/>.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SimpleConversion()
		{
			// Arrange.
			List<string> values = new() { "a", "b", "c", "d" };

			// Act.
			string convertedValue = ConvertFrom.StringListToNewLineString(values);

			// Assert.
			Assert.Equal($"a{Environment.NewLine}b{Environment.NewLine}c{Environment.NewLine}d", convertedValue);
		}

		/// <summary>
		/// <see cref="ConvertFrom.StringListToNewLineString"/> returns empty string for empty list input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyConversion()
		{
			// Arrange.
			List<string> values = new();

			// Act.
			string convertedValue = ConvertFrom.StringListToNewLineString(values);

			// Assert.
			Assert.Equal(string.Empty, convertedValue);
		}

		/// <summary>
		/// <see cref="ConvertFrom.StringListToNewLineString"/> returns empty string for null input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullReturnsOneEmptyString()
		{
			// Act.
			string convertedValue = ConvertFrom.StringListToNewLineString(null);

			// Assert.
			Assert.Equal(string.Empty, convertedValue);
		}
	}
}
