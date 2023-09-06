namespace Elephant.Common.Maths.Tests.CombinatoricTests
{
	/// <summary>
	/// <see cref="Combinatorics.Fibonacci"/> tests.
	/// </summary>
	public class FibonacciTests
	{
		/// <summary>
		/// <see cref="Combinatorics.Fibonacci"/> must return the expected value.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 1)]
		[InlineData(3, 2)]
		[InlineData(4, 3)]
		[InlineData(5, 5)]
		[InlineData(6, 8)]
		[InlineData(7, 13)]
		[InlineData(8, 21)]
		[InlineData(9, 34)]
		[InlineData(10, 55)]
		[InlineData(11, 89)]
		[InlineData(12, 144)]
		[InlineData(13, 233)]
		[InlineData(14, 377)]
		[InlineData(15, 610)]
		[InlineData(16, 987)]
		[InlineData(17, 1597)]
		[InlineData(18, 2584)]
		[InlineData(19, 4181)]
		public void FibonacciReturnsExpected(int index, int expected)
		{
			// Act.
			int result = Combinatorics.Fibonacci(index);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="Combinatorics.Fibonacci"/> must return the expected value.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(-1, 1)]
		[InlineData(-2, -1)]
		[InlineData(-3, 2)]
		[InlineData(-4, -3)]
		[InlineData(-5, 5)]
		[InlineData(-6, -8)]
		[InlineData(-7, 13)]
		[InlineData(-8, -21)]
		[InlineData(-9, 34)]
		[InlineData(-10, -55)]
		[InlineData(-11, 89)]
		[InlineData(-12, -144)]
		[InlineData(-13, 233)]
		[InlineData(-14, -377)]
		[InlineData(-15, 610)]
		[InlineData(-16, -987)]
		[InlineData(-17, 1597)]
		[InlineData(-18, -2584)]
		[InlineData(-19, 4181)]
		public void FibonacciReturnsExpectedNegative(int negativeIndex, int expected)
		{
			// Act.
			int result = Combinatorics.Fibonacci(negativeIndex);

			// Assert.
			Assert.Equal(expected, result);
		}
	}
}