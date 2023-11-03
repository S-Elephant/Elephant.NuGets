using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results.Tests
{
	/// <summary>
	/// Tests with data, using custom statuses.
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
			IResult result = Result.Custom(httpStatusCode, message);

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.True(result.IsInformativeRedirectionOrCustom);
			Assert.Equal(httpStatusCode, result.StatusCode);
			Assert.Equal(message, result.Message);
			Assert.False(result.UsesData);
		}
	}
}