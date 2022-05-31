namespace Elephant.Common.Tests.StringOperationsTests
{
    /// <summary>
    /// <see cref="StringOperations"/> tests.
    /// </summary>
    public class JoinWithTrailingTests
    {
        /// <summary>
        /// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> empty tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIfEmptyReturnsSeparator()
        {
            string joinedString = StringOperations.JoinWithTrailing('/');
         
            Assert.Equal("/", joinedString);
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> separator count tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("A", "B", "C")]
        [InlineData("blue", "Blue", "blue")]
        public void TestIf3StringsReturn3Separators(string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);
         
            Assert.Equal(3, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> separator count tests when the strings contain the separator inside.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("A/A", "B/B", "C")]
        [InlineData("A", "B/B", "C/C")]
        public void TestIf3StringsReturn5Separators(string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);
            
            Assert.Equal(5, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> separator count tests when the strings contain the separator on the outsides.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("A/", "/B", "C")]
        [InlineData("A", "/B", "C/")]
        public void TestIf3StringsWithSeparatorsReturn3Separators(string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);
           
            Assert.Equal(3, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> separator count tests with 1 string input.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("A")]
        [InlineData("//A//")]
        public void TestIf1StringHasOneSeparatorOccurance(string stringA)
        {
            string joinedString = StringOperations.JoinWithTrailing('/', stringA);
           
            Assert.Equal(1, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> 1 null value test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIf1NullValueReturnsOneSeparator()
        {
            string joinedString = StringOperations.JoinWithTrailing('/', null);
           
            Assert.Equal(1, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> 3 null value test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIf3NullValuesReturnOneSeparator()
        {
            string joinedString = StringOperations.JoinWithTrailing('/', null, null, null);
            
            Assert.Equal(1, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithTrailing(char, string?[])"/> null value combined with non-null values tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("A/", null, "A", null)]
        [InlineData("a/", null, "a", null)]
        [InlineData("a/A/", "a", null, "A")]
        [InlineData("A/", null, null, "A")]
        public void TestIfNullWithNonNullValuesIgnoresNullValues(string expected, string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.JoinWithTrailing('/', stringA, stringB, stringC);
            
            Assert.Equal(expected, joinedString);
        }
    }
}
