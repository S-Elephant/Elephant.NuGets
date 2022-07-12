using Elephant.Common.Extensions;

namespace Elephant.Common.Tests.Extensions
{
    /// <summary>
    /// <see cref="RecycleExtensions"/> tests.
    /// </summary>
    public class RecycleExtensionsTests
    {
        /// <summary>
        /// <see cref="RecycleExtensions.Recycle(int, int, int)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData(5, 5, 5, 5)]
        [InlineData(5, 0, 5, 5)]
        [InlineData(5, -5, 5, 5)]
        [InlineData(5, -5, 4, -4)]
        [InlineData(5, 1000, 4, 1000)]
        [InlineData(5, 1000, 1000, 1000)]
        [InlineData(5, -1000, -1000, -1000)]
        [InlineData(5, -1000, -2000, -1000)]
        [InlineData(int.MinValue, int.MinValue, int.MaxValue, int.MinValue)]
        [InlineData(int.MaxValue, int.MinValue, int.MaxValue, int.MaxValue)]
        public void RecycleTests(int value, int min, int max, int expectedValue)
        {
            Assert.Equal(expectedValue, value.Recycle(max, min));
        }

        /// <summary>
        /// <see cref="RecycleExtensions.Recycle(int?, int, int)"/> (nullable) tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData(5, 5, 5, 5)]
        [InlineData(5, 0, 5, 5)]
        [InlineData(5, -5, 5, 5)]
        [InlineData(5, -5, 4, -4)]
        [InlineData(5, 1000, 4, 1000)]
        [InlineData(5, 1000, 1000, 1000)]
        [InlineData(5, -1000, -1000, -1000)]
        [InlineData(5, -1000, -2000, -1000)]
        [InlineData(int.MinValue, int.MinValue, int.MaxValue, int.MinValue)]
        [InlineData(int.MaxValue, int.MinValue, int.MaxValue, int.MaxValue)]
        [InlineData(null, int.MinValue, int.MaxValue, null)]
        public void RecycleNullableTests(int? value, int min, int max, int? expectedValue)
        {
            Assert.Equal(expectedValue, value.Recycle(max, min));
        }
    }
}
