using Elephant.Testing.Xunit;
using Elephant.Validation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Elephant.Validation.Tests
{
    /// <summary>
    /// <see cref="Attributes.RequiredEqualOrGreaterThan"/> data annotation tests.
    /// </summary>
    public class RequiredEqualOrGreaterThanTests
	{
		/// <summary>
		/// Test object for testing <see cref="RequiredAttribute"/>.
		/// </summary>
		private class RequiredId
		{
			/// <summary>
			/// Id (required).
			/// </summary>
			[Required]
			public int? Id { get; set; } = null;

			/// <summary>
			/// Constructor.
			/// </summary>
			public RequiredId(int? id)
			{
				Id = id;
			}
		}

		/// <summary>
		/// Test object for testing <see cref="RequiredEqualOrGreaterThan"/>.
		/// </summary>
		private class RequiredEqualOrGreaterThanOneId
		{
			/// <summary>
			/// Id (required and must be >= 1).
			/// </summary>
			[RequiredEqualOrGreaterThan(1)]
			public int? Id { get; set; } = null;

			/// <summary>
			/// Constructor.
			/// </summary>
			public RequiredEqualOrGreaterThanOneId(int? id)
			{
				Id = id;
			}
		}

		/// <summary>
		/// Test without context with ints.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0, 1, false)]
		[InlineData(1, 1, true)]
		[InlineData(2, 1, true)]
		[InlineData(3, 1, true)]
		public void IsValidWithoutContextInt(object value, int minValue, bool expectedIsValid)
		{
            RequiredEqualOrGreaterThan requiredEqualOrGreaterThan = new(minValue);
			Assert.Equal(expectedIsValid, requiredEqualOrGreaterThan.IsValid(value));
		}

		/// <summary>
		/// Test without context with doubles.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0.01d, 1, false)]
		[InlineData(0.99d, 1, false)]
		[InlineData(1.5d, 1, true)]
		[InlineData(2500.01d, 1, true)]
		[InlineData(double.MaxValue - 1d, 1d, true)]
		public void IsValidWithoutContextDouble(object value, int minValue, bool expectedIsValid)
		{
			RequiredEqualOrGreaterThan requiredEqualOrGreaterThan = new(minValue);
			Assert.Equal(expectedIsValid, requiredEqualOrGreaterThan.IsValid(value));
		}

		/// <summary>
		/// Test without context with floats.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0.01f, 1, false)]
		[InlineData(0.99f, 1, false)]
		[InlineData(1.5f, 1, true)]
		[InlineData(2500.01f, 1, true)]
		[InlineData(float.MaxValue, 1f, true)] // TODO: Why does float.MaxValue bug?
		public void IsValidWithoutContextFloat(object value, int minValue, bool expectedIsValid)
		{
			RequiredEqualOrGreaterThan requiredEqualOrGreaterThan = new(minValue);
			Assert.Equal(expectedIsValid, requiredEqualOrGreaterThan.IsValid(value));
		}

		/// <summary>
		/// Test the built-in <see cref="RequiredAttribute"/> with a null value.
		/// Because if this one doesn't work then <see cref="Attributes.RequiredEqualOrGreaterThan"/> won't work either.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void RequiredTestWithNull()
		{
			RequiredId requiredIdTestObject = new(null);
			ValidationContext validationContext = new(requiredIdTestObject);
			List<ValidationResult> results = new();
			Assert.False(Validator.TryValidateObject(requiredIdTestObject, validationContext, results));
		}

        /// <summary>
        /// Test the built-in <see cref="RequiredAttribute"/> with the value 1.
        /// Because if this one doesn't work then <see cref="Attributes.RequiredEqualOrGreaterThan"/> won't work either.
        /// </summary>
        [Fact]
		[SpeedVeryFast]
		public void RequiredTestWithOne() // Test this to ensure that the base class works.
		{
			RequiredId requiredIdTestObject = new(1);
			ValidationContext validationContext = new(requiredIdTestObject);
			List<ValidationResult> results = new();
			Assert.True(Validator.TryValidateObject(requiredIdTestObject, validationContext, results));
		}

		/// <summary>
		/// Test with context.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(null, false)]
		[InlineData(-1, false)]
		[InlineData(0, false)]
		[InlineData(1, true)]
		[InlineData(99, true)]
		[InlineData(int.MaxValue, true)]
		public void IsValidWithContext(int? id, bool expectedIsValid)
		{
			RequiredEqualOrGreaterThanOneId testObject = new(id);
			ValidationContext context = new(testObject);
			List<ValidationResult> results = new();
			Assert.Equal(expectedIsValid, Validator.TryValidateObject(testObject, context, results));
		}
	}
}
