using Elephant.Common.Pagination;

namespace Elephant.Common.Tests.Pagination
{
    /// <summary>
    /// <see cref="PaginationHelper"/> tests.
    /// </summary>
    public class PaginationHelperTests
    {
        /// <summary>
        /// <see cref="PaginationHelper.Paginate{TSource}(IQueryable{TSource}, int, int)"/> tests.
        /// </summary>
        [Theory]
        [SpeedNormal, UnitTest]
        [InlineData(0, 1, 0, 0)]
        [InlineData(1, 1, 1, 1)]
        [InlineData(0, 0, 1, 1)]
        [InlineData(10, 10, 1, 100)]
        [InlineData(35, 110, 1, 35)]
        [InlineData(35, 110, 2, 35)]
        [InlineData(35, 110, 3, 35)]
        [InlineData(5, 110, 4, 35)]
        public void PaginateCountTests(int expectedCount, int sourceCount, int pageNumber, int pageSize)
        {
            List<int> source = Enumerable.Range(1, sourceCount).ToList();

            List<int> paginatedResult = PaginationHelper.Paginate(source, pageNumber, pageSize);

            Assert.Equal(expectedCount, paginatedResult.Count);
        }

        /// <summary>
        /// <see cref="PaginationHelper.Paginate{TSource}(IQueryable{TSource}, int, int)"/> tests.
        /// </summary>
        [Theory]
        [SpeedNormal, UnitTest]
        [InlineData(1, 10, 1, 100)]
        [InlineData(1, 110, 1, 35)]
        [InlineData(36, 110, 2, 35)]
        [InlineData(71, 110, 3, 35)]
        [InlineData(106, 110, 4, 35)]
        public void PaginateExpectedFirstValueTests(int expectedFirstValue, int sourceCount, int pageNumber, int pageSize)
        {
            List<int> source = Enumerable.Range(1, sourceCount).ToList();

            List<int> paginatedResult = PaginationHelper.Paginate(source, pageNumber, pageSize);

            Assert.Equal(expectedFirstValue, paginatedResult.First());
        }

        /// <summary>
        /// <see cref="PaginationHelper.Paginate{TSource}(IQueryable{TSource}, int, int)"/> tests.
        /// </summary>
        [Theory]
        [SpeedNormal, UnitTest]
        [InlineData(1, 1, 1, 1)]
        [InlineData(10, 10, 1, 100)]
        [InlineData(35, 110, 1, 35)]
        [InlineData(70, 110, 2, 35)]
        [InlineData(105, 110, 3, 35)]
        [InlineData(110, 110, 4, 35)]
        public void PaginateExpectedLastValueTests(int expectedLastValue, int sourceCount, int pageNumber, int pageSize)
        {
            List<int> source = Enumerable.Range(1, sourceCount).ToList();

            List<int> paginatedResult = PaginationHelper.Paginate(source, pageNumber, pageSize);

            Assert.Equal(expectedLastValue, paginatedResult.Last());
        }

        /// <summary>
        /// <see cref="PaginationHelper.LastPageNumber(int, int)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
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
        /// <see cref="PaginationHelper.IsLastPageNumber(int, int, int)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast, UnitTest]
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