using Elephant.Common.Pagination;

namespace Elephant.Common.Tests.Pagination.PaginationHelpers
{
	/// <summary>
	/// <see cref="PaginationHelper.LastOffset(int, int)"/> tests.
	/// </summary>
	public class LastOffsetTests
	{
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
		[InlineData(0, 5, 10)]
		[InlineData(0, 9, 10)]
		[InlineData(10, 11, 1)]
		[InlineData(9, 50, 5)]
		[InlineData(0, 15, 20)]
		[InlineData(19, 100, 5)]
		[InlineData(99, 1000, 10)]
		[InlineData(9, 1000, 100)]
		[InlineData(0, 0, 100)]
		[InlineData(24, 25, 1)]
		[InlineData(99, 100, 1)]
		[InlineData(49, 100, 2)]
		[InlineData(24, 100, 4)]
		[InlineData(4, 100, 20)]
		[InlineData(9, 250, 25)]
		[InlineData(0, 50, 100)]
		[InlineData(9, 500, 50)]

		// int.MinValue and int.MaxValue tests.
		[InlineData(0, int.MinValue, 0)]
		[InlineData(0, int.MinValue, 1)]
		[InlineData(0, int.MinValue, 10)]
		[InlineData(0, int.MinValue, int.MaxValue)]
		[InlineData(0, 0, int.MinValue)]
		[InlineData(0, 1, int.MinValue)]
		[InlineData(0, int.MaxValue, int.MinValue)]
		[InlineData(0, int.MaxValue, 0)]
		[InlineData(2147483646, int.MaxValue, 1)]
		[InlineData(214748364, int.MaxValue, 10)]
		[InlineData(21474836, int.MaxValue, 100)]
		[InlineData(2147483, int.MaxValue, 1000)]
		[InlineData(0, 10, int.MaxValue)]
		[InlineData(0, 100, int.MaxValue)]
		[InlineData(0, int.MinValue, int.MinValue)]
		[InlineData(0, int.MaxValue, int.MaxValue)]
		public void LastOffset(int expected, int sourceCount, int limit)
		{
			Assert.Equal(expected, PaginationHelper.LastOffset(sourceCount, limit));
		}
	}
}