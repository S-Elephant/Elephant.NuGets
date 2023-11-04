using System.Net;
using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results.Tests
{
	/// <summary>
	/// Tests without data.
	/// </summary>
	public class DatalessTests
	{
		/// <summary>
		/// Is able to correctly construct a success through a static constructor without using data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void StaticSuccessConstructorTest()
		{
			// Act.
			IResult result = new Result();

			// Assert.
			Assert.True(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
			Assert.False(result.UsesData);
		}

		/// <summary>
		/// Creating an <see cref="IResult"/> and then adding an error should return an error.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AddingAnErrorReturnsError()
		{
			// Act.
			IResult result = new Result();
			result.AddCustom(false, (int)HttpStatusCode.InternalServerError, "Internal server error.");

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
			Assert.False(result.UsesData);
			Assert.NotNull(result.Message);
		}

		/// <summary>
		/// Is able to correctly construct an error through a static constructor without using data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void StaticErrorConstructorTest()
		{
			// Act.
			IResult result = new Result((int)HttpStatusCode.BadRequest);

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
			Assert.False(result.UsesData);
			Assert.Null(result.Message);
		}

		/// <summary>
		/// Adding successes and errors should return the first error.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AddingMultipleSuccessAndErrorTest()
		{
			// Act.
			IResult result = new Result();
			result.AddOk(true, "Success.");
			result.AddCustom(false, (int)HttpStatusCode.InternalServerError, "Internal server error.");
			result.AddCustom(true, (int)HttpStatusCode.BadRequest);
			result.AddCustom(false, (int)HttpStatusCode.OK);

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
			Assert.False(result.UsesData);
			Assert.NotNull(result.Message);
		}

		/// <summary>
		/// Is able to correctly construct an informational through a static constructor without using data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void StaticInformationalConstructorTest()
		{
			// Act.
			IResult result = Result.ContinueNoData();

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.True(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.Continue, result.StatusCode);
			Assert.False(result.UsesData);
		}

		/// <summary>
		/// Is able to correctly construct an informational through a static constructor without using data and
		/// then adding another informational should return the first informational.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void StaticInformationalConstructorTest2()
		{
			// Act.
			IResult result = Result.SwitchingProtocolsNoData();
			result.AddContinueNoData();

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.True(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.SwitchingProtocols, result.StatusCode);
			Assert.False(result.UsesData);
		}

		private IResult NotFoundMethod()
		{
			return Result.NotFoundNoData();
		}

		/// <summary>
		/// Test Method without data should return NotFound.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MethodReturnsNotFound()
		{
			// Act.
			IResult result = NotFoundMethod();

			// Assert.
			Assert.True(result.IsError);
			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
			Assert.Null(result.Message);
			Assert.False(result.UsesData);
		}

		private IResult ContinueMethod()
		{
			return Result.ContinueNoData();
		}

		/// <summary>
		/// Test Method without data should return Continue.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MethodReturnsContinue()
		{
			// Act.
			IResult result = ContinueMethod();

			// Assert.
			Assert.True(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.Continue, result.StatusCode);
			Assert.Null(result.Message);
			Assert.False(result.UsesData);
		}
	}
}