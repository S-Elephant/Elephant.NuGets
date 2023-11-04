using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results.Tests
{
	/// <summary>
	/// Chain test.
	/// </summary>
	public class ChainTests
	{
		/// <summary>
		/// Combined success, informative and error statuses in a single chain.
		/// </summary>
		private IResult<string> ChainDataMethod()
		{
			IResult<string> result = Result<string>.Ok("Success.");
			result.AddError("Unable to fetch cat food.")
				.AddInternalServerError("Unable to fetch cat food.")
				.AddInternalServerError()
				.AddInternalServerError("Unable to fetch cat food.")
				.AddContinue("Cat food", "Continue")
				.AddConflict("Concurrency conflict on .."); // You can chain different statuses as long as the generic is of the same type.

			return result;
		}

		/// <summary>
		/// Test chaining different statuses that contain string data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ChainDataMethodTest()
		{
			// Act.
			IResult<string> result = ChainDataMethod();

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal(500, result.StatusCode);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// Combined success, informative and error statuses in a single chain.
		/// </summary>
		private IResult ChainNoDataMethod()
		{
			IResult result = Result.OkNoData();
			result.AddErrorNoData("Unable to fetch cat food.")
				.AddInternalServerErrorNoData("Unable to fetch cat food.")
				.AddInternalServerErrorNoData()
				.AddInternalServerErrorNoData("Unable to fetch cat food.")
				.AddContinueNoData("Continue")
				.AddConflictNoData("Concurrency conflict on .."); // You can chain different statuses as long as the generic is of the same type.

			return result;
		}

		/// <summary>
		/// Test chaining different statuses that contain bool data and those that contain no data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ChainNoDataMethodTest()
		{
			// Act.
			IResult<bool> result = ChainNoDataMethod();

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal(500, result.StatusCode);
			Assert.False(result.UsesData);
		}
	}
}