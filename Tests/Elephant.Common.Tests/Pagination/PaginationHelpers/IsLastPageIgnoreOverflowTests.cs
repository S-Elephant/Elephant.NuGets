using Elephant.Common.Pagination;

namespace Elephant.Common.Tests.Pagination.PaginationHelpers
{
	/// <summary>
	/// <see cref="PaginationHelper.IsLastPageIgnoreOverflow(int, int, int)"/> tests.
	/// </summary>
	public class IsLastPageIgnoreOverflowTests
	{
		/// <summary>
		/// Edge case tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(true, -1, 0, -1)]
		[InlineData(true, 0, 0, 0)]
		[InlineData(true, 0, 0, 1)]
		[InlineData(true, 1, 0, 1)]
		[InlineData(true, 10, 9, 1)]
		[InlineData(true, 1, 0, 10)]
		[InlineData(true, int.MinValue, 0, int.MinValue)]
		[InlineData(true, int.MinValue, 0, 0)]
		[InlineData(true, int.MinValue, 0, 1)]
		[InlineData(true, int.MinValue, int.MinValue, int.MinValue)]
		[InlineData(true, 0, 0, int.MinValue)]
		[InlineData(true, int.MaxValue, int.MaxValue - 1, 1)]
		[InlineData(true, int.MaxValue, 0, int.MaxValue)]
		[InlineData(false, int.MaxValue, 0, 1)]
		[InlineData(false, int.MaxValue, 100, 100)]
		[InlineData(true, int.MaxValue, int.MaxValue, int.MaxValue)]
		[InlineData(true, 1, 0, int.MaxValue)]
		[InlineData(true, 0, int.MinValue, 1)]
		[InlineData(true, int.MinValue, int.MaxValue, 1)]
		public void IsLastPageNumber_EdgeCases(bool expected, int sourceCount, int offset, int limit)
		{
			Assert.Equal(expected, PaginationHelper.IsLastPageIgnoreOverflow(sourceCount, offset, limit));
		}

		/// <summary>
		/// Realistic case tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(true, 10, 9, 1)]
		[InlineData(true, 100, 9, 10)]
		[InlineData(true, 250, 4, 50)]
		[InlineData(true, 100, 19, 5)]
		[InlineData(true, 500, 24, 20)]
		[InlineData(true, 1000, 39, 25)]
		[InlineData(true, 1000, 9, 100)]
		[InlineData(false, 100, 0, 10)]
		[InlineData(false, 100, 5, 10)]
		[InlineData(false, 500, 10, 20)]
		[InlineData(false, 1000, 10, 50)]
		[InlineData(true, 157, 15, 10)]
		[InlineData(true, 243, 12, 20)]
		[InlineData(true, 523, 20, 25)]
		[InlineData(false, 157, 5, 10)]
		[InlineData(false, 243, 5, 20)]
		public void IsLastPageNumber_RealisticCases(bool expected, int sourceCount, int offset, int limit)
		{
			Assert.Equal(expected, PaginationHelper.IsLastPageIgnoreOverflow(sourceCount, offset, limit));
		}

		/// <summary>
		/// Tests when NOT on the last page.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(1000, 0, 100)] // Large dataset, first page.
		[InlineData(50, 0, 25)] // Two pages, first page.
		[InlineData(2, 0, 1)] // Two items, first page.
		[InlineData(15, 7, 1)] // Page size of 1, middle page.
		public void IsLastPageNumber_NotLastPage(int sourceCount, int offset, int limit)
		{
			Assert.False(PaginationHelper.IsLastPageIgnoreOverflow(sourceCount, offset, limit));
		}

		/// <summary>
		/// Tests when ON the last page with exact fit.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(100, 9, 10)] // Last page, exact fit (10 pages total, page index 9).
		[InlineData(50, 1, 25)] // Two pages, last page exact fit (page index 1).
		[InlineData(1000, 9, 100)] // Large dataset, last page exact fit (page index 9).
		[InlineData(200, 1, 100)] // Exactly at boundary (2 pages, page index 1).
		[InlineData(20, 1, 10)] // Two full pages, on last page (page index 1).
		public void IsLastPageNumber_LastPageExactFit(int sourceCount, int offset, int limit)
		{
			Assert.True(PaginationHelper.IsLastPageIgnoreOverflow(sourceCount, offset, limit));
		}

		/// <summary>
		/// Tests when ON the last page with partial results.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(105, 10, 10)] // Last page with 5 items (page index 10 of 11 pages).
		[InlineData(101, 10, 10)] // Last page with 1 item (page index 10 of 11 pages).
		[InlineData(999, 9, 100)] // Large dataset, last page with 99 items (page index 9 of 10 pages).
		[InlineData(51, 2, 25)] // Three pages, last page with 1 item (page index 2).
		[InlineData(2, 1, 1)] // Two items, last page (1 item) - already correct!
		[InlineData(15, 14, 1)] // Page size of 1, last page - already correct!
		[InlineData(3, 0, 5)] // Limit larger than source, single page (page index 0).
		[InlineData(91, 9, 10)] // Last page with only 1 item remaining (page index 9 of 10 pages).
		public void IsLastPageNumber_LastPagePartial(int sourceCount, int offset, int limit)
		{
			Assert.True(PaginationHelper.IsLastPageIgnoreOverflow(sourceCount, offset, limit));
		}

		/// <summary>
		/// Tests with single page scenarios.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(1, 0, 1)] // Single item, single page.
		[InlineData(5, 0, 10)] // Items less than page size.
		[InlineData(50, 0, 100)] // Half page of items.
		[InlineData(1, 0, 100)] // One item with large page size.
		[InlineData(99, 0, 100)] // Just under page size.
		public void IsLastPageNumber_SinglePage(int sourceCount, int offset, int limit)
		{
			Assert.True(PaginationHelper.IsLastPageIgnoreOverflow(sourceCount, offset, limit));
		}

		/// <summary>
		/// Tests with page size of 1 (one item per page).
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(false, 10, 0, 1)] // First of 10 pages.
		[InlineData(false, 10, 5, 1)] // Middle of 10 pages.
		[InlineData(false, 10, 8, 1)] // Second to last of 10 pages.
		[InlineData(true, 10, 9, 1)] // Last of 10 pages.
		[InlineData(true, 5, 4, 1)] // Last of 5 pages.
		public void IsLastPageNumber_PageSizeOne(bool expected, int sourceCount, int offset, int limit)
		{
			Assert.Equal(expected, PaginationHelper.IsLastPageIgnoreOverflow(sourceCount, offset, limit));
		}

		/// <summary>
		/// Tests with large datasets.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(false, 10000, 0, 100)] // First page of 100 pages.
		[InlineData(true, 10000, 99, 100)] // Last page exact fit.
		[InlineData(true, 10001, 100, 100)] // Last page with 1 item.
		[InlineData(true, 9999, 99, 100)] // Last page with 99 items.
		public void IsLastPageNumber_LargeDatasets(bool expected, int sourceCount, int offset, int limit)
		{
			Assert.Equal(expected, PaginationHelper.IsLastPageIgnoreOverflow(sourceCount, offset, limit));
		}

		/// <summary>
		/// Tests with offsets that go well beyond the last page.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(true, 10000, 25000, 100)]
		[InlineData(true, 10000, 19800, 100)]
		[InlineData(true, -1, 1, 1)]
		[InlineData(true, int.MinValue, 1, 1)]
		[InlineData(true, 0, 1, 1)]
		[InlineData(true, 0, 1, 100)]
		[InlineData(true, 100000, int.MaxValue, 1000)]
		[InlineData(true, 100000, int.MaxValue, int.MaxValue)]
		[InlineData(true, 100000, int.MaxValue, int.MinValue)]
		public void IsLastPageNumber_Overflows(bool expected, int sourceCount, int offset, int limit)
		{
			Assert.Equal(expected, PaginationHelper.IsLastPageIgnoreOverflow(sourceCount, offset, limit));
		}
	}
}