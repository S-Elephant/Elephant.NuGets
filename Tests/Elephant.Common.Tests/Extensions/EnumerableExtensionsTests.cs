using Elephant.Common.Extensions;

namespace Elephant.Common.Tests.Extensions
{
    /// <summary>
    /// <see cref="Elephant.Common.Extensions.Enumerable"/> test class.
    /// </summary>
    public class EnumerableExtensionsTests
    {
        /// <summary>
        /// <see cref="Elephant.Common.Extensions.Enumerable.None{TSource}(IEnumerable{TSource}, System.Func{TSource, bool})"/> tests.
        /// </summary>
        [Theory]
        [SpeedFast]
        [InlineData(true, 0)]
        [InlineData(true, -1)]
        [InlineData(true, 10)]
        [InlineData(true, int.MinValue)]
        [InlineData(true, int.MaxValue)]
        [InlineData(false, 1)]
        [InlineData(false, 2)]
        [InlineData(false, 3)]
        [InlineData(false, -10)]
        public void EmptyTest(bool expected, int value)
        {
            List<int> x = new() { -10, 1, 2, 3 };
            
            Assert.Equal(expected, x.None(x => x == value));
        }
    }
}
