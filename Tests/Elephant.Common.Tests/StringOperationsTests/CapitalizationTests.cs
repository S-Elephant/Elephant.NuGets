namespace Elephant.Common.Tests.StringOperationsTests
{
    /// <summary>
    /// <see cref="StringOperations"/> capitalization tests.
    /// </summary>
    public class CapitalizationTests
    {
        /// <summary>
        /// <see cref="StringOperations.CapitalizeFirstChar(string?)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData("", "")]
        [InlineData("a", "A")]
        [InlineData("A", "A")]
        [InlineData("The long dog.", "The long dog.")]
        [InlineData("the long dog.", "The long dog.")]
        public void CapitalizeFirstCharTests(string source, string expected)
        {
            Assert.Equal(expected, StringOperations.CapitalizeFirstChar(source));
        }

        /// <summary>
        /// <see cref="StringOperations.CapitalizeFirstChar(string?)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("a", "A")]
        [InlineData("A", "A")]
        [InlineData("The long dog.", "The long dog.")]
        [InlineData("the long dog.", "The long dog.")]
        public void CapitalizeFirstCharNullableTests(string? source, string? expected)
        {
            Assert.Equal(expected, StringOperations.CapitalizeFirstCharNullable(source));
        }

        /// <summary>
        /// <see cref="StringOperations.ToTitleCase(string)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData("", "")]
        [InlineData("a", "A")]
        [InlineData("A", "A")]
        [InlineData("The LONG dog.", "The Long Dog.")]
        [InlineData("the long dog.", "The Long Dog.")]
        public void ToTitleCaseTests(string source, string expected)
        {
            Assert.Equal(expected, StringOperations.ToTitleCase(source));
        }

        /// <summary>
        /// <see cref="StringOperations.ToTitleCase(string)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("a", "A")]
        [InlineData("A", "A")]
        [InlineData("The LONG dog.", "The Long Dog.")]
        [InlineData("the long dog.", "The Long Dog.")]
        public void ToTitleCaseNullableTests(string? source, string? expected)
        {
            Assert.Equal(expected, StringOperations.ToTitleCaseNullable(source));
        }
    }
}
