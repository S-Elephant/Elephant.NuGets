using System.Net;
using Elephant.Types.Results.Abstractions;
using Xunit.Sdk;

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

		private IResult<int?> TestMethodWithData()
		{
			return Result<int?>.NotFound();
		}

		/// <summary>
		/// Test Method with data should return NotFound.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MethodReturnsNotFound()
		{
			// Act.
			IResult<int?> result = TestMethodWithData();

			// Assert.
			Assert.True(result.IsError);
			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
			Assert.Null(result.Data);
			Assert.Null(result.Message);
			Assert.True(result.UsesData);
		}

		private IResult<int?> TestMethodWithMessage()
		{
			return Result<int?>.NotFound("Message1");
		}

		/// <summary>
		/// Test Method with data should return NotFound.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MethodWithMessageReturnsNotFound()
		{
			// Act.
			IResult<int?> result = TestMethodWithMessage();

			// Assert.
			Assert.True(result.IsError);
			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
			Assert.Null(result.Data);
			Assert.Equal("Message1", result.Message);
			Assert.True(result.UsesData);
		}

		private IResult<string?> TestMethodWithStringDataAndMessage()
		{
			return Result<string?>.NotFound("Message1");
		}

		/// <summary>
		/// Test Method with data should return NotFound.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MethodWithStringAndMessageReturnsNotFound()
		{
			// Act.
			IResult<string?> result = TestMethodWithStringDataAndMessage();

			// Assert.
			Assert.True(result.IsError);
			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
			Assert.Null(result.Data);
			Assert.Equal("Message1", result.Message);
			Assert.True(result.UsesData);
		}

		private IResult<string> ContinueStringMethod()
		{
			return Result<string>.Continue(null, "Lorem ipsum");
		}

		/// <summary>
		/// Test Method with string data should return Continue.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void StringMethodReturnsContinue()
		{
			// Act.
			IResult<string> result = ContinueStringMethod();

			// Assert.
			Assert.True(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.Continue, result.StatusCode);
			Assert.Equal("Lorem ipsum", result.Message);
			Assert.True(result.UsesData);
		}

		private IResult<bool> ContinueWithFalseDataMethod()
		{
			return Result<bool>.Continue(false);
		}

		/// <summary>
		/// Test Method with null data should return Continue.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void BoolMethodWithDefaultDataReturnsContinue()
		{
			// Act.
			IResult<bool> result = ContinueWithFalseDataMethod();

			// Assert.
			Assert.True(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.Continue, result.StatusCode);
			Assert.Null(result.Message);
			Assert.False(result.Data);
			Assert.True(result.UsesData);
		}

		private IResult<bool> ContinueWithTrueDataMethod()
		{
			return Result<bool>.Continue(true);
		}

		/// <summary>
		/// Test Method with bool data should return Continue.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void BoolMethodWithBoolDataReturnsContinue()
		{
			// Act.
			IResult<bool> result = ContinueWithTrueDataMethod();

			// Assert.
			Assert.True(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.Continue, result.StatusCode);
			Assert.Null(result.Message);
			Assert.True(result.Data);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// Ok returns as expected.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void OkReturnsExpected()
		{
			// Act.
			IResult<string> result = Result<string>.Ok("Data here.");

			// Assert.
			Assert.True(result.IsSuccess);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
			Assert.Null(result.Message);
			Assert.Equal("Data here.", result.Data);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// <see cref="IResult{TData}.AsNoData"/> converts into expected result.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AsNoDataConvertsAsExpected()
		{
			// Arrange.
			IResult<string> result = Result<string>.Ok("Data here.");

			// Act.
			IResult resultWithoutData = result.AsNoData();

			// Assert.
			Assert.True(resultWithoutData.IsSuccess);
			Assert.Equal((int)HttpStatusCode.OK, resultWithoutData.StatusCode);
			Assert.False(resultWithoutData.UsesData);
		}
	}
}