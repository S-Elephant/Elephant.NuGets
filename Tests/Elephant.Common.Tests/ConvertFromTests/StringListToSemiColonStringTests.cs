namespace Elephant.Common.Tests.ConvertFromTests
{
	/// <summary>
	/// <see cref="ConvertFrom.StringListToSemiColonString"/> tests.
	/// </summary>
	public class StringListToSemiColonStringTests
	{
		/// <summary>
		/// <see cref="ConvertFrom.StringListToSemiColonString"/> joins list items with semicolons.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SimpleConversion()
		{
			// Arrange.
			List<string> values = new() { "a", "b", "c", "d" };

			// Act.
			string convertedValue = ConvertFrom.StringListToSemiColonString(values);

			// Assert.
			Assert.Equal("a;b;c;d", convertedValue);
		}

		/// <summary>
		/// <see cref="ConvertFrom.StringListToSemiColonString"/> returns empty string for empty list input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyConversion()
		{
			// Arrange.
			List<string> values = new();

			// Act.
			string convertedValue = ConvertFrom.StringListToSemiColonString(values);

			// Assert.
			Assert.Equal(string.Empty, convertedValue);
		}

		/// <summary>
		/// <see cref="ConvertFrom.StringListToSemiColonString"/> returns empty string for null input.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullReturnsOneEmptyString()
		{
			// Act.
			string convertedValue = ConvertFrom.StringListToSemiColonString(null);

			// Assert.
			Assert.Equal(string.Empty, convertedValue);
		}
	}
}
