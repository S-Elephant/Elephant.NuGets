using Elephant.Common.Pagination;
using Elephant.Models.RequestModels;

namespace Elephant.Common.Tests.Pagination.PaginationHelpers
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
		[InlineData(3, 103, 999, 10)]
		public void PaginateCountTests(int expectedCount, int sourceCount, int offset, int limit)
		{
			// Arrange.
			List<int> source = Enumerable.Range(1, sourceCount).ToList();

			// Act.
			List<int> paginatedResult = PaginationHelper.Paginate(source, offset, limit);

			// Assert.
			Assert.Equal(expectedCount, paginatedResult.Count);
		}

		/// <summary>
		/// Verifies that pagination returns the correct first element for various offset/limit combinations.
		/// </summary>
		[Theory]
		[SpeedNormal, UnitTest]
		[InlineData(1, 10, 0, 100)] // First page, limit exceeds source.
		[InlineData(1, 110, 0, 35)] // First page of multiple pages.
		[InlineData(36, 110, 1, 35)] // Second page starts at item 36.
		[InlineData(71, 110, 2, 35)] // Third page starts at item 71.
		[InlineData(106, 110, 3, 35)] // Fourth page starts at item 106.
		public void PaginateExpectedFirstValueTests(int expectedFirstValue, int sourceCount, int offset, int limit)
		{
			// Arrange.
			List<int> source = Enumerable.Range(1, sourceCount).ToList();

			// Act.
			List<int> paginatedResult = PaginationHelper.Paginate(source, offset, limit);

			// Assert.
			Assert.Equal(expectedFirstValue, paginatedResult[0]);
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
			// Arrange.
			List<int> source = Enumerable.Range(1, sourceCount).ToList();

			// Act.
			List<int> paginatedResult = PaginationHelper.Paginate(source, offset, limit);

			// Assert.
			Assert.Equal(expectedLastValue, paginatedResult[^1]);
		}

		/// <summary>
		/// <see cref="PaginationHelper.Paginate{TSource}(IList{TSource},int,int)"/> test
		/// with a limit of zero or less should return zero.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0, 0, -1)]
		[InlineData(0, 0, 0)]
		[InlineData(0, 500, -400)]
		[InlineData(0, 500, -500)]
		[InlineData(0, 500, 0)]
		public void PaginateWithZeroOrLessLimitReturnsZero(int expectedCount, int offset, int limit)
		{
			// Arrange.
			List<int> source = Enumerable.Range(1, 10).ToList();

			// Act.
			List<int> result = PaginationHelper.Paginate(source, offset, limit);

			// Assert.
			Assert.Equal(expectedCount, result.Count);
		}

		/// <summary>
		/// <see cref="PaginationHelper.Paginate{TSource}(IList{TSource},int,int)"/> test
		/// with a limit of zero or less should return zero.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0, -1)]
		[InlineData(0, 0)]
		[InlineData(500, -400)]
		[InlineData(500, -500)]
		[InlineData(500, 0)]
		public void PaginateOverloadWithZeroOrLessLimitReturnsNone(int offset, int limit)
		{
			// Arrange.
			const int itemsInDatabaseCount = 10;
			IQueryable<int> source = Enumerable.Range(1, itemsInDatabaseCount).AsQueryable();
			PaginationRequest paginationRequest = new(offset, limit);
			if (limit <= 0)
				paginationRequest.Limit = 0; // Force limit to zero.

			// Act.
			IEnumerable<int> result = PaginationHelper.Paginate(source, paginationRequest);

			// Assert.
			Assert.Empty(result);
		}

		/// <summary>
		/// Verifies that the IQueryable overload correctly paginates using page-based offset.
		/// Offset represents the page number (0-based), not the number of items to skip.
		/// </summary>
		[Theory]
		[InlineData(1, 9, 1)] // Page 9, limit 1: returns 1 item.
		[InlineData(5, 0, 5)] // Page 0, limit 5: returns first 5 items.
		[InlineData(5, 1, 5)] // Page 1, limit 5: returns items 6-10.
		[InlineData(3, 2, 3)] // Page 2, limit 3: returns items 7-9.
		public void PaginateWithValidInputsReturnsCorrectPage(int expectedCount, int offset, int limit)
		{
			// Arrange.
			IQueryable<int> source = Enumerable.Range(1, 10).AsQueryable();

			// Act.
			List<int> result = source.Paginate(offset, limit).ToList();

			// Assert.
			Assert.Equal(expectedCount, result.Count);
			Assert.Equal(source.Skip(offset * limit).Take(limit).ToList(), result);
		}

		/// <summary>
		/// <see cref="PaginationHelper.Paginate{TSource}(IList{TSource},int,int)"/> test
		/// with 3 elements and a limit of int.MaxValue should return 3 elements.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void Paginate3ElementsWithMaxLimitReturns3()
		{
			// Arrange.
			List<int> source = [1, 2, 3];

			// Act.
			List<int> result = PaginationHelper.Paginate(source, 0, int.MaxValue);

			// Assert.
			Assert.Equal(3, result.Count);
		}

		/// <summary>
		/// <see cref="PaginationHelper.Paginate{TSource}(IList{TSource},int,int)"/> test
		/// with 0 elements and a limit of int.MaxValue should return 0 elements.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void Paginate0ElementsWithMaxLimitReturns0()
		{
			// Arrange.
			List<int> source = [];

			// Act.
			List<int> result = PaginationHelper.Paginate(source, 0, int.MaxValue);

			// Assert.
			Assert.Empty(result);
		}
	}
}