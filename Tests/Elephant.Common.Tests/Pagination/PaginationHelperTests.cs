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
		[InlineData(0, 1, -1, 0)]
		[InlineData(1, 1, 0, 1)]
		[InlineData(0, 0, 0, 1)]
		[InlineData(10, 10, 0, 100)]
		[InlineData(35, 110, 0, 35)]
		[InlineData(35, 110, 1, 35)]
		[InlineData(35, 110, 2, 35)]
		[InlineData(5, 110, 3, 35)]
		public void PaginateCountTests(int expectedCount, int sourceCount, int offset, int limit)
		{
			List<int> source = Enumerable.Range(1, sourceCount).ToList();

			List<int> paginatedResult = PaginationHelper.Paginate(source, offset, limit);

			Assert.Equal(expectedCount, paginatedResult.Count);
		}

		/// <summary>
		/// <see cref="PaginationHelper.Paginate{TSource}(IQueryable{TSource}, int, int)"/> tests.
		/// </summary>
		[Theory]
		[SpeedNormal, UnitTest]
		[InlineData(1, 10, 0, 100)]
		[InlineData(1, 110, 0, 35)]
		[InlineData(36, 110, 1, 35)]
		[InlineData(71, 110, 2, 35)]
		[InlineData(106, 110, 3, 35)]
		public void PaginateExpectedFirstValueTests(int expectedFirstValue, int sourceCount, int offset, int limit)
		{
			List<int> source = Enumerable.Range(1, sourceCount).ToList();

			List<int> paginatedResult = PaginationHelper.Paginate(source, offset, limit);

			Assert.Equal(expectedFirstValue, paginatedResult.First());
		}

		/// <summary>
		/// <see cref="PaginationHelper.Paginate{TSource}(IQueryable{TSource}, int, int)"/> tests.
		/// </summary>
		[Theory]
		[SpeedNormal, UnitTest]
		[InlineData(1, 1, 0, 1)]
		[InlineData(10, 10, 0, 100)]
		[InlineData(35, 110, 0, 35)]
		[InlineData(70, 110, 1, 35)]
		[InlineData(105, 110, 2, 35)]
		[InlineData(110, 110, 3, 35)]
		public void PaginateExpectedLastValueTests(int expectedLastValue, int sourceCount, int offset, int limit)
		{
			List<int> source = Enumerable.Range(1, sourceCount).ToList();

			List<int> paginatedResult = PaginationHelper.Paginate(source, offset, limit);

			Assert.Equal(expectedLastValue, paginatedResult.Last());
		}

		/// <summary>
		/// <see cref="PaginationHelper.LastOffset(int, int)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0, -1, -1)]
		[InlineData(0, 0, 0)]
		[InlineData(0, 0, 1)]
		[InlineData(0, 1, 1)]
		[InlineData(9, 10, 1)]
		[InlineData(0, 1, 10)]
		[InlineData(9, 100, 10)]
		[InlineData(4, 250, 50)]
		public void LastOffset(int expected, int sourceCount, int limit)
		{
			Assert.Equal(expected, PaginationHelper.LastOffset(sourceCount, limit));
		}

		/// <summary>
		/// <see cref="PaginationHelper.TotalPageCount(int, int)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0, -1, 0)]
		[InlineData(0, 0, -1)]
		[InlineData(0, -1, -1)]
		[InlineData(0, 0, 0)]
		[InlineData(0, 1, 0)]
		[InlineData(0, 0, 1)]
		[InlineData(1, 1, 1)]
		[InlineData(1, 10, 10)]
		[InlineData(2, 10, 5)]
		[InlineData(2, 10, 7)]
		[InlineData(2, 10, 9)]
		[InlineData(1, 10, 11)]
		[InlineData(1, 10, 9999)]
		[InlineData(4, 10, 3)]
		public void TotalPageCount(int expected, int sourceCount, int limit)
		{
			Assert.Equal(expected, PaginationHelper.TotalPageCount(sourceCount, limit));
		}

		/// <summary>
		/// <see cref="PaginationHelper.CurrentOffset(int, int, int)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0, -1, 1, 1)]
		[InlineData(0, 1, -1, 1)]
		[InlineData(0, 1, 1, -1)]
		[InlineData(0, 0, 0, 1)]
		[InlineData(0, 100, -10, 1)]
		[InlineData(0, 100, 0, 1)]
		[InlineData(50, 100, 50, 1)]
		[InlineData(1, 100, 5, 50)]
		[InlineData(2, 100, 2, 45)]
		[InlineData(6, 1000, 6, 100)]
		[InlineData(9, 1000, 9, 100)]
		[InlineData(9, 1000, 10, 100)]
		public void CurrentOffset(int expected, int sourceCount, int offset, int limit)
		{
			Assert.Equal(expected, PaginationHelper.CurrentOffset(sourceCount, offset, limit));
		}

		/// <summary>
		/// <see cref="PaginationHelper.IsLastPage(int, int, int)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(true, -1, 0, -1)]
		[InlineData(true, 0, 0, 0)]
		[InlineData(true, 0, 0, 1)]
		[InlineData(true, 1, 0, 1)]
		[InlineData(true, 10, 9, 1)]
		[InlineData(true, 1, 0, 10)]
		[InlineData(true, 100, 9, 10)]
		[InlineData(true, 250, 4, 50)]
		public void IsLastPageNumber(bool expected, int sourceCount, int offset, int limit)
		{
			Assert.Equal(expected, PaginationHelper.IsLastPage(sourceCount, offset, limit));
		}
	}
}