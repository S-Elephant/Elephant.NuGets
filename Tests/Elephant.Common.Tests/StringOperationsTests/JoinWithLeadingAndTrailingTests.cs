namespace Elephant.Common.Tests.StringOperationsTests
{
    /// <summary>
    /// <see cref="StringOperations"/> tests.
    /// </summary>
    public class JoinWithLeadingAndTrailingAndTrailingTests
    {
        /// <summary>
        /// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> empty tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIfEmptyReturnsTwoSeparators()
        {
            string joinedString = StringOperations.JoinWithLeadingAndTrailing('/');
         
            Assert.Equal("//", joinedString);
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> separator count tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("A", "B", "C")]
        [InlineData("blue", "Blue", "blue")]
        public void TestIf4StringsHave3SeparatorOccurances(string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);
         
            Assert.Equal(4, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> separator count tests when the strings contain the separator inside.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("A/A", "B/B", "C")]
        [InlineData("A", "B/B", "C/C")]
        public void TestIf3StringsHave6SeparatorOccurances(string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);
            
            Assert.Equal(6, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> separator count tests when the strings contain the separator on the outsides.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("A/", "/B", "C")]
        [InlineData("A", "/B", "C/")]
        public void TestIf3StringsHave4SeparatorOccurances(string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);
           
            Assert.Equal(4, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> separator count tests with 1 string input.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("A")]
        [InlineData("//A//")]
        public void TestIf1StringReturnsTwoSeparators(string stringA)
        {
            string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA);
           
            Assert.Equal(2, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> 1 null value test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIf1NullValueReturnsTwoSeparators()
        {
            string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', null);
           
            Assert.Equal(2, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> 3 null value test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIf3NullValuesReturnTwoSeparators()
        {
            string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', null, null, null);
            
            Assert.Equal(2, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.JoinWithLeadingAndTrailing(char, string?[])"/> null value combined with non-null values tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData("/A/", null, "A", null)]
        [InlineData("/a/", null, "a", null)]
        [InlineData("/a/A/", "a", null, "A")]
        [InlineData("/A/", null, null, "A")]
        [InlineData("/A/", null, null, "/A/")]
        public void TestIfNullWithNonNullValuesIgnoresNullValues(string expected, string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.JoinWithLeadingAndTrailing('/', stringA, stringB, stringC);
            
            Assert.Equal(expected, joinedString);
        }
    }
}
