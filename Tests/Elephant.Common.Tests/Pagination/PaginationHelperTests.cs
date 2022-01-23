using Elephant.Common.Pagination;
using Elephant.Testing.Xunit;

namespace Elephant.Common.Tests.Pagination
{
    /// <summary>
    /// <see cref="PaginationHelper"/> tests.
    /// </summary>
    public class PaginationHelperTests
    {
        /// <summary>
        /// Paginate(..) tests.
        /// </summary>
        [Theory]
        [SpeedNormal]
        [InlineData(0, -1, -1, 1, 0, 0)]
        [InlineData(1, 1, 1, 1, 1, 1)]
        [InlineData(0, -1, -1, 0, 1, 1)]
        [InlineData(10, 1, 10, 10, 1, 100)]
        [InlineData(35, 1, 35, 110, 1, 35)]
        [InlineData(35, 36, 70, 110, 2, 35)]
        [InlineData(35, 71, 105, 110, 3, 35)]
        [InlineData(5, 106, 110, 110, 4, 35)]
        public void Paginate(int expectedCount, int expectedFirstValue, int expectedLastValue, int sourceCount, int pageNumber, int pageSize)
        {
            List<int> source = Enumerable.Range(1, sourceCount).ToList();

            List<int> paginatedResult = PaginationHelper.Paginate(source, pageNumber, pageSize);

            int paginatedItemCount = paginatedResult.Count;
            Assert.Equal(expectedCount, paginatedItemCount);

            if (paginatedItemCount > 0)
            {
                Assert.Equal(expectedFirstValue, paginatedResult.First());
                Assert.Equal(expectedLastValue, paginatedResult.Last());
            }
        }
        
        
        /// <summary>
        /// LastPageNumber(..) tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData(1, -1, -1)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(10, 10, 1)]
        [InlineData(1, 1, 10)]
        [InlineData(10, 100, 10)]
        [InlineData(5, 250, 50)]
        public void LastPageNumber(int expected, int sourceCount, int pageSize)
        {
            Assert.Equal(expected, PaginationHelper.LastPageNumber(sourceCount, pageSize));
        }
        
        /// <summary>
        /// LastPageNumber(..) tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData(true, -1, -1, 1)]
        [InlineData(true, 0, 0, 1)]
        [InlineData(true, 0, 1, 1)]
        [InlineData(true, 1, 1, 1)]
        [InlineData(true, 10, 1, 10)]
        [InlineData(true, 1, 10, 1)]
        [InlineData(true, 100, 10, 10)]
        [InlineData(true, 250, 50, 5)]
        public void IsLastPageNumber(bool expected, int sourceCount, int pageSize, int pageNumber)
        {
            Assert.Equal(expected, PaginationHelper.IsLastPageNumber(sourceCount, pageNumber, pageSize));
        }
    }
}