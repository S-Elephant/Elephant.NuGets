using System.Net;
using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results.Tests
{
	/// <summary>
	/// Tests with data.
	/// </summary>
	public class DataTests
	{
		/// <summary>
		/// Is able to correctly construct a success through a static constructor with data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void StaticSuccessConstructorTest()
		{
			// Act.
			IResult<int> result = new Result<int>();

			// Assert.
			Assert.True(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
			Assert.Equal(0, result.Data);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// Is able to correctly construct an error through a static constructor with data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void StaticErrorConstructorTest()
		{
			// Act.
			IResult<string> result = new Result<string>("Pikachu", (int)HttpStatusCode.InternalServerError, "Internal server error.");

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
			Assert.Equal("Pikachu", result.Data);
			Assert.Equal("Internal server error.", result.Message);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// Is able to correctly construct an error through a static constructor with data but without a message.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void StaticErrorConstructorWithoutMessageTest()
		{
			// Act.
			IResult<string> result = new Result<string>("Pikachu", (int)HttpStatusCode.InternalServerError);

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
			Assert.Null(result.Message);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// Is able to correctly construct an error through a static constructor with data and then adding a
		/// success status, should return the error status.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AddingSuccessToErrorShouldReturnError()
		{
			// Act.
			IResult<string> result = new Result<string>("Pikachu", (int)HttpStatusCode.InternalServerError);
			result.AddOk(null, "Success."); // This result should be ignored in the final result.

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
			Assert.Null(result.Message);
			Assert.True(result.UsesData);
		}
	}
}