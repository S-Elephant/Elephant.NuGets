using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results.Tests
{
	/// <summary>
	/// Tests using custom statuses.
	/// </summary>
	public class CustomTests
	{
		/// <summary>
		/// Test a basic custom constructor with data.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("Pikachu", 800, "Custom response 800.")]
		[InlineData("Charizard", 900, "Custom response 900.")]
		public void BasicCustomConstructorWithDataTest(string data, int httpStatusCode, string message)
		{
			// Act.
			IResult<string> result = Result<string>.Custom(data, httpStatusCode, message);

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.True(result.IsInformativeRedirectionOrCustom);
			Assert.Equal(httpStatusCode, result.StatusCode);
			Assert.Equal(data, result.Data);
			Assert.Equal(message, result.Message);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// Test a basic custom constructor without data.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(800, "Custom response 800.")]
		[InlineData(900, "Custom response 900.")]
		public void BasicCustomConstructorWithoutDataTest(int httpStatusCode, string message)
		{
			// Act.
			IResult result = Result.CustomNoData(httpStatusCode, message);

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.True(result.IsInformativeRedirectionOrCustom);
			Assert.Equal(httpStatusCode, result.StatusCode);
			Assert.Equal(message, result.Message);
			Assert.False(result.UsesData);
		}

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
		/// Test a basic custom constructor without data.
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

		/// <summary>
		/// Simple error test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SimpleErrorTest()
		{
			// Act.
			IResult result = Result.ErrorNoData("Something went wrong.");

			Assert.True(result.IsError);
			Assert.Equal("Something went wrong.", result.Message);
			Assert.False(result.UsesData);
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private class Customer
		{
		}

		/// <summary>
		/// Error test with class data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void DataClassErrorTest()
		{
			// Act.
			IResult<Customer> result = Result<Customer>.Error("Unable to process customer.", new Customer());

			Assert.True(result.IsError);
			Assert.True(result.UsesData);
		}
	}
}