namespace Elephant.Common.Tests.StringOperationsTests
{
    /// <summary>
    /// <see cref="StringOperations"/> tests.
    /// </summary>
    public class JoinTests
    {
        /// <summary>
        /// <see cref="StringOperations.Join(char, string?[])"/> empty tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void TestIfEmptyReturnsEmpty()
        {
            string joinedString = StringOperations.Join('/');

            Assert.Equal(string.Empty, joinedString);
        }

        /// <summary>
        /// <see cref="StringOperations.Join(char, string?[])"/> separator count tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData("A", "B", "C")]
        [InlineData("blue", "Blue", "blue")]
        public void TestIf3StringsReturn2Separators(string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.Join('/', stringA, stringB, stringC);

            Assert.Equal(2, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.Join(char, string?[])"/> separator count tests when the strings contain the separator inside.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData("A/A", "B/B", "C")]
        [InlineData("A", "B/B", "C/C")]
        public void TestIf3StringsReturns4InsideSeparators(string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.Join('/', stringA, stringB, stringC);

            Assert.Equal(4, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.Join(char, string?[])"/> separator count tests when the strings contain the separator on the outsides.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData("A/", "/B", "C")]
        [InlineData("A", "/B", "C/")]
        public void TestIf3StringsReturn2OutsideSeparators(string stringA, string stringB, string stringC)
        {
            string joinedString = StringOperations.Join('/', stringA, stringB, stringC);

            Assert.Equal(2, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.Join(char, string?[])"/> separator count tests with 1 string input.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData("A")]
        [InlineData("//A//")]
        public void TestIf1StringReturnsNoSeparators(string stringA)
        {
            string joinedString = StringOperations.Join('/', stringA);

            Assert.Equal(0, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.Join(char, string?[])"/> 1 null value test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void TestIf1NullValueReturnEmpty()
        {
            string joinedString = StringOperations.Join('/', new string?[] { null });

            Assert.Equal(0, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.Join(char, string?[])"/> 3 null value test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void TestIf3NullValuesReturnEmpty()
        {
            string joinedString = StringOperations.Join('/', null, null, null);

            Assert.Equal(0, joinedString.Count(x => x == '/'));
        }

        /// <summary>
        /// <see cref="StringOperations.Join(char, string?[])"/> null value combined with non-null values tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
        [InlineData("A", null, "A", null)]
        [InlineData("a", null, "a", null)]
        [InlineData("a/A", "a", null, "A")]
        [InlineData("A", null, null, "A")]
        public void TestIfNullWithNonNullValuesIgnoresNullValues(string expected, string? stringA, string? stringB, string? stringC)
        {
            string joinedString = StringOperations.Join('/', stringA, stringB, stringC);

            Assert.Equal(expected, joinedString);
        }
    }
}
