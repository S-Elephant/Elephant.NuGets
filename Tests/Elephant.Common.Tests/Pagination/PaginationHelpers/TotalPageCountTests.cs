using Elephant.Common.Pagination;

namespace Elephant.Common.Tests.Pagination.PaginationHelpers
{
	/// <summary>
	/// <see cref="PaginationHelper.TotalPageCount(int, int)"/> tests.
	/// </summary>
	public class TotalPageCountTests
	{
		/// <summary>
		/// <see cref="PaginationHelper.TotalPageCount(int, int)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		// Negative sourceCount tests should always return 0.
		[InlineData(0, -1, 1)]
		[InlineData(0, -100, 10)]
		[InlineData(0, -1, 100)]
		[InlineData(0, -50, 5)]

		// Negative limit tests should always return 0.
		[InlineData(0, 10, -1)]
		[InlineData(0, 100, -10)]
		[InlineData(0, 1, -100)]
		[InlineData(0, 50, -5)]

		// Both negative should always return 0.
		[InlineData(0, -1, 0)]
		[InlineData(0, 0, -1)]
		[InlineData(0, -1, -1)]
		[InlineData(0, -10, -10)]
		[InlineData(0, -100, -50)]

		// Zero sourceCount tests should always return 0.
		[InlineData(0, 0, 0)]
		[InlineData(0, 0, 1)]
		[InlineData(0, 0, 10)]
		[InlineData(0, 0, 100)]
		[InlineData(0, 0, int.MaxValue)]

		// Zero limit tests should always return 0.
		[InlineData(0, 1, 0)]
		[InlineData(0, 10, 0)]
		[InlineData(0, 100, 0)]
		[InlineData(0, int.MaxValue, 0)]

		// Exact division cases, sourceCount perfectly divisible by limit.
		[InlineData(1, 1, 1)]
		[InlineData(1, 10, 10)]
		[InlineData(2, 20, 10)]
		[InlineData(5, 50, 10)]
		[InlineData(10, 100, 10)]
		[InlineData(100, 1000, 10)]
		[InlineData(3, 30, 10)]
		[InlineData(4, 40, 10)]

		// Partial last page cases with the sourceCount not perfectly divisible.
		[InlineData(2, 11, 10)]
		[InlineData(2, 19, 10)]
		[InlineData(3, 21, 10)]
		[InlineData(3, 25, 10)]
		[InlineData(3, 29, 10)]
		[InlineData(11, 101, 10)]

		// sourceCount smaller than limit should return 1 page.
		[InlineData(1, 1, 10)]
		[InlineData(1, 5, 10)]
		[InlineData(1, 9, 10)]
		[InlineData(1, 1, 100)]
		[InlineData(1, 50, 100)]
		[InlineData(1, 99, 100)]

		// sourceCount much larger than limit.
		[InlineData(334, 1000, 3)]
		[InlineData(500, 1000, 2)]
		[InlineData(1000, 1000, 1)]

		// Limit of 1, each item should gets its own page.
		[InlineData(10, 10, 1)]
		[InlineData(100, 100, 1)]

		// Large limit single pages.
		[InlineData(1, 10, 1000)]
		[InlineData(1, 100, 1000)]
		[InlineData(1, 500, 1000)]

		// Edge cases with int.MaxValue.
		[InlineData(1, 1, int.MaxValue)]
		[InlineData(1, 100, int.MaxValue)]
		[InlineData(1, 10000, int.MaxValue)]
		[InlineData(1, int.MaxValue, int.MaxValue)]
		[InlineData(214748365, int.MaxValue, 10)]
		[InlineData(715827883, int.MaxValue, 3)]
		[InlineData(1073741824, int.MaxValue, 2)]
		[InlineData(429496730, int.MaxValue, 5)]

		// Near int.MaxValue sourceCount.
		[InlineData(214748365, int.MaxValue - 1, 10)]
		[InlineData(214748364, int.MaxValue - 10, 10)]
		[InlineData(1, int.MaxValue - 1, int.MaxValue)]

		// Large limits approaching int.MaxValue.
		[InlineData(1, 1000, int.MaxValue - 1)]
		[InlineData(1, 1000, int.MaxValue - 1000)]

		// Edge cases with int.MinValue.
		[InlineData(0, 1, int.MinValue)]
		[InlineData(0, 100, int.MinValue)]
		[InlineData(0, 10000, int.MinValue)]
		[InlineData(0, int.MinValue, int.MinValue)]
		[InlineData(0, int.MinValue, 10)]

		// Various limit sizes.
		[InlineData(4, 10, 3)]
		[InlineData(3, 10, 4)]
		[InlineData(2, 10, 5)]
		[InlineData(2, 10, 6)]
		[InlineData(2, 10, 7)]
		[InlineData(2, 10, 8)]
		[InlineData(2, 10, 9)]

		// Prime number.
		[InlineData(3, 13, 5)] // 13 items (prime), 5 per page
		[InlineData(3, 17, 7)] // 17 items (prime), 7 per page
		[InlineData(5, 23, 5)] // 23 items (prime), 5 per page

		// Power of 2.
		[InlineData(1, 8, 8)] // 8 items, 8 per page
		[InlineData(2, 16, 8)] // 16 items, 8 per page
		[InlineData(4, 32, 8)] // 32 items, 8 per page
		[InlineData(8, 64, 8)] // 64 items, 8 per page

		// Large sourceCounts.
		[InlineData(40000, 1000000, 25)]
		[InlineData(20000, 1000000, 50)]
		[InlineData(10000, 1000000, 100)]
		[InlineData(100000, 1000000, 10)]

		// Odd combinations.
		[InlineData(100, 999, 10)]
		[InlineData(34, 333, 10)]
		[InlineData(23, 111, 5)]
		[InlineData(1, 10, 9999)]


		// Realistic pagination scenarios.
		[InlineData(5, 42, 10)]
		[InlineData(4, 100, 30)]
		[InlineData(2, 100, 75)]
		[InlineData(10, 200, 20)]
		[InlineData(17, 250, 15)]
		[InlineData(34, 500, 15)]
		[InlineData(3, 75, 25)]
		[InlineData(20, 1000, 50)]
		[InlineData(10000, 100000, 10)]
		[InlineData(1000, 10000, 10)]
		public void TotalPageCount(int expected, int sourceCount, int limit)
		{
			// Act.
			int totalPageCount = PaginationHelper.TotalPageCount(sourceCount, limit);

			// Assert.
			Assert.Equal(expected, totalPageCount);
		}
	}
}