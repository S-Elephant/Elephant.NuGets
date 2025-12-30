using System;

namespace Elephant.Common.Maths.Tests.CombinatoricTests
{
	/// <summary>
	/// <see cref="Combinatorics.Factorial"/> tests.
	/// </summary>
	public class FactorialTests
	{
		/// <summary>
		/// <see cref="Combinatorics.Factorial"/> must return the expected value.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0, 1)]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(3, 6)]
		[InlineData(4, 24)]
		[InlineData(5, 120)]
		[InlineData(6, 720)]
		[InlineData(7, 5040)]
		[InlineData(8, 40320)]
		[InlineData(9, 362880)]
		[InlineData(10, 3628800)]
		[InlineData(20, 2432902008176640000)]
		public void FactorialReturnsExpected(int value, long expected)
		{
			// Act.
			long result = Combinatorics.Factorial(value);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="Combinatorics.Factorial"/> must throw an <see cref="ArgumentException"/>
		/// if the value is negative.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void FactorialReturnsArgumentException()
		{
			// Act and assert.
			_ = Assert.Throws<ArgumentException>(() => Combinatorics.Factorial(-1));
		}

		/// <summary>
		/// <see cref="Combinatorics.Factorial"/> must throw an <see cref="OverflowException"/>
		/// if the result value would overflow.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void FactorialReturnsOverflowException()
		{
			// Act and assert.
			_ = Assert.Throws<OverflowException>(() => Combinatorics.Factorial(100));
		}
	}
}