namespace Elephant.Common.Tests.StringOperationsTests
{
    /// <summary>
    /// <see cref="StringOperations"/> remove substrings tests.
    /// </summary>
    public class RemoveSubstringTests
    {
        private const string Source = "The big white dog walked around the block and then around the house.";

        /// <summary>
        /// <see cref="StringOperations.RemoveSubstringFromString(string, string)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData("", "", "")]
        [InlineData(Source, "Non-existing", Source)]
        [InlineData(Source, "The", " big white dog walked around the block and then around the house.")]
        [InlineData(Source, "the", "The big white dog walked around  block and then around the house.")]
        [InlineData(Source, Source, "")]
        public void RemoveSubstringFromStringTests(string source, string substringToRemove, string expected)
        {
            string result = StringOperations.RemoveSubstringFromString(source, substringToRemove);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void RemoveSubstringsFromStringTestCaseSensitivity()
        {
            string result = StringOperations.RemoveSubstringsFromString("The big white dog.", new[] { "the", "white" } );

            Assert.Equal("The big  dog.", result);
        }

        /// <summary>
        /// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void RemoveSubstringsFromStringTestFirstOccuranceOnly()
        {
            string result = StringOperations.RemoveSubstringsFromString("The very very big dog.", new[] { "very" });

            Assert.Equal("The  very big dog.", result);
        }

        /// <summary>
        /// <see cref="StringOperations.RemoveSubstringsFromString(string, IEnumerable{string})"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void RemoveSubstringsFromStringTestNonExisting()
        {
            string result = StringOperations.RemoveSubstringsFromString("The dog.", new[] { "Small" });

            Assert.Equal("The dog.", result);
        }
    }
}
