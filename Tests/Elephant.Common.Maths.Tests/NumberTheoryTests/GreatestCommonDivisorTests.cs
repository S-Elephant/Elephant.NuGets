namespace Elephant.Common.Maths.Tests.NumberTheoryTests
{
	/// <summary>
	/// <see cref="NumberTheory.GreatestCommonDivisor"/> tests.
	/// </summary>
	public class GreatestCommonDivisorTests
	{
		/// <summary>
		/// <see cref="NumberTheory.GreatestCommonDivisor"/> must return the expected value.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(-1, -1, 1)]
		[InlineData(10, -1, 1)]
		[InlineData(0, 0, 0)]
		[InlineData(1, 1, 1)]
		[InlineData(1, 2, 1)]
		[InlineData(2, 1, 1)]
		[InlineData(8, 4, 4)]
		[InlineData(8, 12, 4)]
		[InlineData(100, 10, 10)]
		[InlineData(10, 100, 10)]
		[InlineData(100, 16, 4)]
		[InlineData(16, 100, 4)]
		[InlineData(1000, 160, 40)]
		[InlineData(160, 1000, 40)]
		[InlineData(int.MaxValue, int.MaxValue, int.MaxValue)]
		public void GreatestCommonDivisorReturnsExpected(int a, int b, int expected)
		{
			// Act.
			int result = NumberTheory.GreatestCommonDivisor(a, b);

			// Assert.
			Assert.Equal(expected, result);
		}
	}
}