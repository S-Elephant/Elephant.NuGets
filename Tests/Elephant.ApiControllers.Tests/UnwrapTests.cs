using Elephant.Types.Results;
using Elephant.Types.Results.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elephant.ApiControllers.Tests
{
	/// <summary>
	/// <see cref="ElephantControllerBase.Unwrap{TData}"/> tests.
	/// </summary>
	public class UnwrapTests
	{
		private readonly ElephantControllerBase _systemUnderTest;

		/// <summary>
		/// Setup.
		/// </summary>
		public UnwrapTests()
		{
			_systemUnderTest = new ElephantControllerBase();
		}

		/// <summary>
		/// Result without data should return null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void OkWithoutDataReturnsNull()
		{
			// Arrange.
			IResult result = Result.Ok();

			// Act.
			IActionResult actionResult = _systemUnderTest.Unwrap(result);

			// Assert.
			ObjectResult createdResult = Assert.IsType<ObjectResult>(actionResult);
			Assert.Null(createdResult.Value);
		}

		/// <summary>
		/// Result without data should return null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void InternalServerErrorWithoutDataTest()
		{
			// Arrange.
			IResult result = Result.InternalServerError();

			// Act.
			IActionResult actionResult = _systemUnderTest.Unwrap(result);

			// Assert.
			ObjectResult createdResult = Assert.IsType<ObjectResult>(actionResult);
			Assert.Equal(StatusCodes.Status500InternalServerError, createdResult.StatusCode);
		}

		/// <summary>
		/// Result without data adn without a message should return no message.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void InternalServerErrorWithoutDataAndMessageReturnsNoMessage()
		{
			// Arrange.
			IResult result = Result.InternalServerError();

			// Act.
			IActionResult actionResult = _systemUnderTest.Unwrap(result);

			// Assert.
			ObjectResult createdResult = Assert.IsType<ObjectResult>(actionResult);
			Assert.Null(createdResult.Value);
		}

		/// <summary>
		/// Result without data should return correct message.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void InternalServerErrorWithoutDataReturnsCorrectMessage()
		{
			// Arrange.
			IResult result = Result.InternalServerError("Pikachu");

			// Act.
			IActionResult actionResult = _systemUnderTest.Unwrap(result);

			// Assert.
			ObjectResult createdResult = Assert.IsType<ObjectResult>(actionResult);
			Assert.Equal("Pikachu", createdResult.Value);
		}

		/// <summary>
		/// Result with int data should return correct data.
		/// </summary>
		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-10)]
		[InlineData(0)]
		[InlineData(10)]
		[InlineData(int.MaxValue)]
		[SpeedVeryFast, UnitTest]
		public void OkWithIntDataReturnsCorrectData(int data)
		{
			// Arrange.
			IResult<int> result = Result<int>.Ok(data);

			// Act.
			IActionResult actionResult = _systemUnderTest.Unwrap(result);

			// Assert.
			ObjectResult createdResult = Assert.IsType<ObjectResult>(actionResult);
			Assert.Equal(data, createdResult.Value);
		}

		/// <summary>
		/// Result with string data should return correct data.
		/// </summary>
		[Theory]
		[InlineData("")]
		[InlineData("-10")]
		[InlineData("__")]
		[InlineData("%#$%@#&%*%*$#$\\")]
		[InlineData("\n")]
		[InlineData(null)]
		[SpeedVeryFast, UnitTest]
		public void OkWithStringDataReturnsCorrectData(string? data)
		{
			// Arrange.
			IResult<string> result = Result<string>.Ok(data, null);

			// Act.
			IActionResult actionResult = _systemUnderTest.Unwrap(result);

			// Assert.
			ObjectResult createdResult = Assert.IsType<ObjectResult>(actionResult);
			Assert.Equal(data, createdResult.Value);
		}

		/// <summary>
		/// Customer test class.
		/// </summary>
		private class Customer
		{
			/// <summary>
			/// Name.
			/// </summary>
			internal string Name { get; set; } = "Mr. Pikachu";

			/// <summary>
			/// Constructor.
			/// </summary>
			public Customer()
			{
			}

			/// <summary>
			/// Constructor with initializer.
			/// </summary>
			public Customer(string name)
			{
				Name = name;
			}
		}

		/// <summary>
		/// Result with class data should return correct data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void OkWithClassDataReturnsCorrectData()
		{
			// Arrange.
			IResult<Customer> result = Result<Customer>.Ok(new Customer());

			// Act.
			IActionResult actionResult = _systemUnderTest.Unwrap(result);

			// Assert.
			ObjectResult createdResult = Assert.IsType<ObjectResult>(actionResult);
			Assert.Equal("Mr. Pikachu", ((Customer)createdResult.Value).Name);
		}

		/// <summary>
		/// Created result should return the correct HTTP status code.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CreatedReturnsCorrectHttpStatusCode()
		{
			// Arrange.
			List<Customer> createdCustomers = new()
			{
				new("Squirtle"),
				new("Elephant"),
				new("Charizard"),
			};
			IResult<List<Customer>> result = Result<List<Customer>>.Created(new List<Customer>(createdCustomers));

			// Act.
			IActionResult actionResult = _systemUnderTest.Unwrap(result);

			// Assert.
			ObjectResult createdResult = Assert.IsType<ObjectResult>(actionResult);
			Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
		}

		/// <summary>
		/// Created result should return the created objects.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CreatedReturnsCreatedObjects()
		{
			// Arrange.
			List<Customer> createdCustomers = new()
			{
				new("Squirtle"),
				new("Elephant"),
				new("Charizard"),
			};
			IResult<List<Customer>> result = Result<List<Customer>>.Created(new List<Customer>(createdCustomers));

			// Act.
			IActionResult actionResult = _systemUnderTest.Unwrap(result);

			// Assert.
			ObjectResult createdResult = Assert.IsType<ObjectResult>(actionResult);
			List<Customer> actionResultContents = ((List<Customer>)createdResult.Value);
			Assert.Equal(createdCustomers, actionResultContents);
		}
	}
}
