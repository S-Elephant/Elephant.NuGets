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
			// Arrange.
			string originalValue = "a\nb\nc";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> split by slash r slash n.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByRAndN()
		{
			// Arrange.
			string originalValue = "a\nb\r\nc";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> split by <see cref="Environment.NewLine"/>.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByEnvironmentNewLine()
		{
			// Arrange.
			string originalValue = $"a{Environment.NewLine}b{Environment.NewLine}c";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.Join(char, string?[])"/> split using multiple newlines and using the <see cref="StringSplitOptions"/> to remove empty entries.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByMultipleNewLines()
		{
			// Arrange.
			string originalValue = $"a{Environment.NewLine}\r\n\nb{Environment.NewLine}{Environment.NewLine}c";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue, StringSplitOptions.RemoveEmptyEntries);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with empty string returns single empty element.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_EmptyString_ReturnsSingleEmptyElement()
		{
			// Arrange.
			string originalValue = string.Empty;

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			_ = Assert.Single(splitValues);
			Assert.Equal(string.Empty, splitValues.First());
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with empty string and RemoveEmptyEntries returns empty collection.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_EmptyStringWithRemoveEmptyEntries_ReturnsEmptyCollection()
		{
			// Arrange.
			string originalValue = string.Empty;

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue, StringSplitOptions.RemoveEmptyEntries);

			// Assert.
			Assert.Empty(splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with single line and no newline returns single element.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_SingleLineNoNewline_ReturnsSingleElement()
		{
			// Arrange.
			string originalValue = "single line";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			_ = Assert.Single(splitValues);
			Assert.Equal("single line", splitValues.First());
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> split by slash r only.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_SplitByR_ReturnsCorrectElements()
		{
			// Arrange.
			string originalValue = "a\rb\rc";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with mixed newline types.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_MixedNewlineTypes_ReturnsCorrectElements()
		{
			// Arrange.
			string originalValue = "a\nb\rc\r\nd";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", "c", "d" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with leading newline.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_LeadingNewline_ReturnsEmptyFirstElement()
		{
			// Arrange.
			string originalValue = "\na\nb";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			Assert.Equal(new List<string> { string.Empty, "a", "b" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with trailing newline.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_TrailingNewline_ReturnsEmptyLastElement()
		{
			// Arrange.
			string originalValue = "a\nb\n";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			Assert.Equal(new List<string> { "a", "b", string.Empty }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with leading newline and RemoveEmptyEntries.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_LeadingNewlineWithRemoveEmptyEntries_ReturnsOnlyNonEmptyElements()
		{
			// Arrange.
			string originalValue = "\n\na\nb";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue, StringSplitOptions.RemoveEmptyEntries);

			// Assert.
			Assert.Equal(new List<string> { "a", "b" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with trailing newline and RemoveEmptyEntries.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_TrailingNewlineWithRemoveEmptyEntries_ReturnsOnlyNonEmptyElements()
		{
			// Arrange.
			string originalValue = "a\nb\n\n";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue, StringSplitOptions.RemoveEmptyEntries);

			// Assert.
			Assert.Equal(new List<string> { "a", "b" }, splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with only newlines.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_OnlyNewlines_ReturnsEmptyElements()
		{
			// Arrange.
			string originalValue = "\n\n\n";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			Assert.Equal(4, splitValues.Count());
			Assert.All(splitValues, item => Assert.Equal(string.Empty, item));
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with only newlines and RemoveEmptyEntries.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_OnlyNewlinesWithRemoveEmptyEntries_ReturnsEmptyCollection()
		{
			// Arrange.
			string originalValue = "\n\r\n\r";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue, StringSplitOptions.RemoveEmptyEntries);

			// Assert.
			Assert.Empty(splitValues);
		}

		/// <summary>
		/// <see cref="StringOperations.SplitByNewLine"/> with whitespace preserved.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SplitByNewLine_WithWhitespace_PreservesWhitespace()
		{
			// Arrange.
			string originalValue = "  a  \n  b  \n  c  ";

			// Act.
			IEnumerable<string> splitValues = StringOperations.SplitByNewLine(originalValue);

			// Assert.
			Assert.Equal(new List<string> { "  a  ", "  b  ", "  c  " }, splitValues);
		}
	}
}
