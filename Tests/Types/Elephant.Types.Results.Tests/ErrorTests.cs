using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results.Tests
{
	/// <summary>
	/// Error (the generic version) tests.
	/// </summary>
	public class ErrorTests
	{
		/// <summary>
		/// Test a basic custom constructor without data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CustomExceptionWithoutDataTest()
		{
			// Act.
			IResult result = Result.ErrorNoData(new NotImplementedException());

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal(500, result.StatusCode);
			Assert.False(result.UsesData);
		}

		/// <summary>
		/// Test a basic custom constructor with data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CustomExceptionWithDataTest()
		{
			// Act.
			IResult<string> result = Result<string>.Error(new NotImplementedException());

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal(500, result.StatusCode);
			Assert.True(result.UsesData);
		}

		private IResult<string> ErrorMethod(bool errorOccurred)
		{
			IResult<string> result = Result<string>.Ok("Success.");

			if (errorOccurred)
			{
				result.AddError("Error 1")
					.AddInternalServerError("Error 2")
					.AddContinue("Continue")
					.AddOk("Ok data")
					.AddConflict("Concurrency conflict on .."); // You can chain statuses.
			}

			return result;
		}

		/// <summary>
		/// Test a basic error method using custom errors.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ErrorMethodTest()
		{
			// Act.
			IResult<string> result = ErrorMethod(true);

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal(500, result.StatusCode);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private class Customer
		{
		}

		/// <summary>
		/// Test an error with class data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ErrorWithClassDataTest()
		{
			// Act.
			IResult<Customer> result = Result<Customer>.Error(new AccessViolationException(), data: new Customer());

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal(500, result.StatusCode);
			Assert.True(result.UsesData);
		}
	}
}