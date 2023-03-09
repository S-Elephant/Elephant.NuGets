using System;

namespace Elephant.Common.Tests.StringOperationsTests
{
	/// <summary>
	/// <see cref="StringOperations.SplitByNewLine"/> tests.
	/// </summary>
	public class SplitByNewLineTests
	{
		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> split by slash n.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByN()
		{
			string originalValue = "a\nb\nc";

			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			Assert.Equal(new List<string> { "a", "b", "c" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> split by slash r slash n.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByRAndN()
		{
			string originalValue = "a\nb\r\nc";

			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			Assert.Equal(new List<string> { "a", "b", "c" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> split by <see cref="Environment.NewLine"/>.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByEnvironmentNewLine()
		{
			string originalValue = $"a{Environment.NewLine}b{Environment.NewLine}c";

			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			Assert.Equal(new List<string> { "a", "b", "c" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> split using multiple newlines and using the <see cref="StringSplitOptions"/> to remove empty entries.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByMultipleNewLines()
		{
			string originalValue = $"a{Environment.NewLine}\r\n\nb{Environment.NewLine}{Environment.NewLine}c";

			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue, StringSplitOptions.RemoveEmptyEntries);

			Assert.Equal(new List<string> { "a", "b", "c" }, splitValues);
		}
	}
}
