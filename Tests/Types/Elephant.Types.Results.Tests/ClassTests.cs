using System.Net;
using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results.Tests
{
	/// <summary>
	/// Tests with data that are classes.
	/// </summary>
	public class ClassTests
	{
		/// <summary>
		/// Customer test class.
		/// </summary>
		private sealed class Customer
		{
			/// <summary>
			/// Name.
			/// </summary>
			internal string Name { get; set; }

			/// <summary>
			/// City.
			/// </summary>
			internal string City { get; set; }

			/// <summary>
			/// Birth year.
			/// </summary>
			internal int BirthYear { get; set; }

			/// <summary>
			/// Constructor with optional initializers.
			/// </summary>
			public Customer(string name = "Pikachu", string city = "Maastricht", int birthYear = 1980)
			{
				Name = name;
				City = city;
				BirthYear = birthYear;
			}
		}

		/// <summary>
		/// Is it possible to construct a valid <see cref="IResult{Customer}"/>?
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CanConstructValidCustomerResult()
		{
			// Act.
			IResult<Customer> result = new Result<Customer>();

			// Assert.
			Assert.True(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// Add 2 successes still returns the first success?
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MultipleSuccessesReturnsFirst()
		{
			// Act.
			IResult<Customer> result = new Result<Customer>(new Customer("Squirtle"), (int)HttpStatusCode.OK, "Success.");
			_ = result.AddOk(new Customer());

			// Assert.
			Assert.True(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal("Squirtle", result.Data?.Name);
		}

		/// <summary>
		/// Tests that adding multiple successes returns the first success.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MultipleSuccessesReturnsFirst2()
		{
			// Act.
			IResult<Customer> result = new Result<Customer>(new Customer("Squirtle"), (int)HttpStatusCode.OK, "Success.");
			_ = result.AddOk(new Customer());
			_ = result.AddOk(new Customer());
			_ = result.AddCustom(new Customer("Charizard"), (int)HttpStatusCode.Processing);
			_ = result.AddOk(new Customer());

			// Assert.
			Assert.True(result.IsSuccess);
			Assert.False(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
			Assert.Equal("Squirtle", result.Data?.Name);
			Assert.Equal("Success.", result.Message);
			Assert.True(result.UsesData);
		}

		/// <summary>
		/// Adding multiple successes and an error should return the error.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AddingSuccessesAndErrorReturnsError()
		{
			// Act.
			IResult<Customer> result = new Result<Customer>(new Customer("Squirtle"), (int)HttpStatusCode.OK, "Success.");
			_ = result.AddOk(new Customer());
			_ = result.AddOk(new Customer());
			_ = result.AddCustom(new Customer("Charizard"), (int)HttpStatusCode.InternalServerError);
			_ = result.AddOk(new Customer());

			// Assert.
			Assert.False(result.IsSuccess);
			Assert.True(result.IsError);
			Assert.False(result.IsInformativeRedirectionOrCustom);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
			Assert.Equal("Charizard", result.Data?.Name);
			Assert.Null(result.Message);
			Assert.True(result.UsesData);
		}
	}
}