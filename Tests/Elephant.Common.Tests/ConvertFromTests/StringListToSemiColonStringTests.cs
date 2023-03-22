namespace Elephant.Common.Tests.ConvertFromTests
{
	/// <summary>
	/// <see cref="ConvertFrom.StringListToSemiColonString"/> tests.
	/// </summary>
	public class StringListToSemiColonStringTests
	{
		/// <summary>
		/// <see cref="ConvertFrom.StringListToSemiColonString"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SimpleConversion()
		{
			List<string> values = new() { "a", "b", "c", "d" };

			string convertedValue = ConvertFrom.StringListToSemiColonString(values);

			Assert.Equal("a;b;c;d", convertedValue);
		}

		/// <summary>
		/// <see cref="ConvertFrom.StringListToSemiColonString"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyConversion()
		{
			List<string> values = new();

			string convertedValue = ConvertFrom.StringListToSemiColonString(values);

			Assert.Equal(string.Empty, convertedValue);
		}

		/// <summary>
		/// <see cref="ConvertFrom.StringListToSemiColonString"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullReturnsOneEmptyString()
		{
			string convertedValue = ConvertFrom.StringListToSemiColonString(null);

			Assert.Equal(string.Empty, convertedValue);
		}
	}
}
