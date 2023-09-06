namespace Elephant.Common.Maths.Tests.NumberTheoryTests
{
	/// <summary>
	/// <see cref="NumberTheory.LeastCommonMultiple(int,int)"/> tests.
	/// </summary>
	public class LeastCommonMultipleTests
	{
		/// <summary>
		/// <see cref="NumberTheory.LeastCommonMultiple(int,int)"/> must return the expected value.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(-1, -1, 1)]
		[InlineData(10, -1, 10)]
		[InlineData(0, 0, 0)]
		[InlineData(1, 1, 1)]
		[InlineData(1, 2, 2)]
		[InlineData(2, 1, 2)]
		[InlineData(8, 4, 8)]
		[InlineData(8, 12, 24)]
		[InlineData(100, 10, 100)]
		[InlineData(10, 100, 100)]
		[InlineData(100, 16, 400)]
		[InlineData(16, 100, 400)]
		[InlineData(1000, 160, 4000)]
		[InlineData(160, 1000, 4000)]
		[InlineData(int.MaxValue, int.MaxValue, 0)]
		public void LeastCommonMultipleReturnsExpected(int a, int b, int expected)
		{
			// Act.
			int result = NumberTheory.LeastCommonMultiple(a, b);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="NumberTheory.LeastCommonMultiple(int,int)"/> must return the expected value.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(8, 12, 24, 24)]
		[InlineData(-8, -12, -24, 24)]
		[InlineData(8, -12, -24, 24)]
		[InlineData(-8, 12, 24, 24)]
		[InlineData(2, 4, 24, 24)]
		[InlineData(2, 4, 8, 8)]
		[InlineData(8, 24, 16, 48)]
		[InlineData(3, 3, 1000, 3000)]
		[InlineData(3, 100, 1000, 3000)]
		[InlineData(3, 1000, 1000, 3000)]
		public void LeastCommonMultipleWithMultiInputReturnsExpected(int a, int b, int c, int expected)
		{
			// Act.
			int result = NumberTheory.LeastCommonMultiple(a, b, c);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="NumberTheory.LeastCommonMultiple(int,int)"/> must return 0 if no values provided.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void LeastCommonMultipleReturnsZeroIfNoValues()
		{
			// Act.
			int result = NumberTheory.LeastCommonMultiple();

			// Assert.
			Assert.Equal(0, result);
		}
	}
}