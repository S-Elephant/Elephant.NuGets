namespace Elephant.Common.Tests.StringOperationsTests
{
	/// <summary>
	/// <see cref="StringOperations.EncloseByIfNotAlready"/> tests.
	/// </summary>
	public class EncloseByIfNotAlreadyTests
	{
		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should enclose using double quotes.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseInDoubleQuotes()
		{
			const string originalString = "This is a sentence that must be enclosed in double quotes.";

			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			Assert.Equal($"\"{originalString}\"", enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should NOT enclose using double quotes because it already was.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseInDoubleQuotesShouldDoNothing()
		{
			const string originalString = "\"This is a sentence that must be enclosed in double quotes.\"";

			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, '"');

			Assert.Equal(originalString, enclosedValue);
		}

		/// <summary>
		/// <see cref="StringOperations.EncloseByIfNotAlready"/> should enclose using 'A'.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EncloseByA()
		{
			const string originalString = "This is a sentence that must be enclosed in double quotes.";

			string enclosedValue = StringOperations.EncloseByIfNotAlready(originalString, 'A');

			Assert.Equal($"A{originalString}A", enclosedValue);
		}
	}
}
