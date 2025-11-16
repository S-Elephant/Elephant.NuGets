using Elephant.Common.Pagination;

namespace Elephant.Common.Tests.Pagination.PaginationHelpers
{
	/// <summary>
	/// <see cref="PaginationHelper.CurrentOffset(int, int, int)"/> tests.
	/// </summary>
	public class CurrentOffsetTests
	{
		/// <summary>
		/// <see cref="PaginationHelper.CurrentOffset(int, int, int)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		// Negative values
		[InlineData(0, -1, 1, 1)]
		[InlineData(0, 1, -1, 1)]
		[InlineData(0, 1, 1, -1)]
		[InlineData(0, 100, -10, 1)]
		[InlineData(0, -100, 5, 10)]
		[InlineData(0, 50, -5, -10)]

		// Zero values.
		[InlineData(0, 0, 0, 1)]
		[InlineData(0, 100, 0, 1)]
		[InlineData(0, 0, 5, 10)]
		[InlineData(0, 100, 0, 10)]
		[InlineData(0, 0, 0, 0)]

		// Valid offsets within range.
		[InlineData(50, 100, 50, 1)]
		[InlineData(1, 100, 5, 50)]
		[InlineData(2, 100, 2, 45)]
		[InlineData(6, 1000, 6, 100)]
		[InlineData(9, 1000, 9, 100)]
		[InlineData(1, 50, 1, 10)]
		[InlineData(3, 200, 3, 25)]
		[InlineData(5, 500, 5, 50)]

		// Offset exceeds total page count.
		[InlineData(9, 1000, 10, 100)]
		[InlineData(9, 1000, 15, 100)]
		[InlineData(4, 50, 20, 10)]
		[InlineData(9, 100, 100, 10)]
		[InlineData(99, 1000, 500, 10)]
		[InlineData(0, 1, 10, 5)]

		// Single items.
		[InlineData(0, 1, 0, 1)]
		[InlineData(0, 1, 1, 1)]
		[InlineData(0, 1, 5, 1)]

		// large number edge cases.
		[InlineData(999, 10000, 999, 10)]
		[InlineData(9999, 100000, 9999, 10)]
		[InlineData(0, 1000000, 0, 100)]
		[InlineData(99, 10000, 99, 100)]

		// Limit of 1.
		[InlineData(0, 50, 0, 1)]
		[InlineData(10, 50, 10, 1)]
		[InlineData(49, 50, 49, 1)]
		[InlineData(49, 50, 100, 1)]

		// Various limits.
		[InlineData(0, 100, 0, 25)]
		[InlineData(1, 100, 1, 25)]
		[InlineData(3, 100, 3, 25)]
		[InlineData(0, 1000, 0, 100)]
		[InlineData(5, 1000, 5, 100)]

		// int.MaxValue and int.MinValue edge cases.
		[InlineData(0, int.MaxValue, 0, 1)]
		[InlineData(0, int.MaxValue, 0, int.MaxValue)]
		[InlineData(0, int.MaxValue, 1, int.MaxValue)]
		[InlineData(2147483646, int.MaxValue, int.MaxValue, 1)]
		[InlineData(0, int.MaxValue, int.MaxValue, int.MaxValue)]
		[InlineData(0, int.MinValue, 0, 1)]
		[InlineData(0, int.MinValue, int.MinValue, 1)]
		[InlineData(0, int.MinValue, 0, int.MinValue)]
		[InlineData(0, int.MinValue, int.MinValue, int.MinValue)]
		[InlineData(9, 100, int.MaxValue, 10)]
		[InlineData(0, 100, int.MinValue, 10)]
		[InlineData(0, 100, 50, int.MaxValue)]
		[InlineData(0, 100, 50, int.MinValue)]
		[InlineData(0, int.MaxValue, int.MinValue, 100)]
		[InlineData(0, int.MinValue, int.MaxValue, 100)]
		public void CurrentOffset(int expected, int sourceCount, int offset, int limit)
		{
			Assert.Equal(expected, PaginationHelper.CurrentOffset(sourceCount, offset, limit));
		}
	}
}