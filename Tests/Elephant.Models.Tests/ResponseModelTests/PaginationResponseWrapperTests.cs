using Elephant.Models.ResponseModels;

namespace Elephant.Models.Tests.ResponseModelTests
{
	/// <summary>
	/// <see cref="PaginationResponseWrapper{TData}"/> tests.
	/// </summary>
	public class PaginationResponseWrapperTests
	{
		/// <summary>
		/// Constructor offset test.
		/// </summary>
		[Theory]
		[InlineData(int.MinValue, 0)]
		[InlineData(-1, 0)]
		[InlineData(0, 0)]
		[InlineData(33, 33)]
		[InlineData(int.MaxValue, int.MaxValue)]

		[SpeedVeryFast]
		public void IsConstructorOffsetCorrectlyAssigned(int offset, int expectedOffset)
		{
			PaginationResponseWrapper<bool> sut = new(true, offset);

			Assert.Equal(expectedOffset, sut.Offset);
		}

		/// <summary>
		/// Constructor limit test.
		/// </summary>
		[Theory]
		[InlineData(int.MinValue, int.MaxValue)]
		[InlineData(-1, int.MaxValue)]
		[InlineData(int.MaxValue, int.MaxValue)]
		[InlineData(33, 33)]
		[SpeedVeryFast]
		public void IsConstructorLimitCorrectlyAssigned(int limit, int expectedLimit)
		{
			PaginationResponseWrapper<bool> sut = new(true, limit: limit);

			Assert.Equal(expectedLimit, sut.Limit);
		}
	}
}