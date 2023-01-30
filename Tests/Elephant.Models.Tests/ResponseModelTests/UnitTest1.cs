using Elephant.Models.RequestModels;

namespace Elephant.Models.Tests.ResponseModelTests
{
	/// <summary>
	/// <see cref="PaginationResponseWrapper{TData}"/> tests.
	/// </summary>
	public class UnitTest1
	{
		/// <summary>
		/// Constructor offset test.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void IsConstructorOffsetCorrectlyAssigned()
		{
			PaginationResponseWrapper<bool> sut = new (true, 33, 100);

			Assert.Equal(33, sut.Offset);
		}

		/// <summary>
		/// Constructor limit test.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void IsConstructorLimitCorrectlyAssigned()
		{
			PaginationResponseWrapper<bool> sut = new (true, 33, 100);

			Assert.Equal(100, sut.Limit);
		}
	}
}