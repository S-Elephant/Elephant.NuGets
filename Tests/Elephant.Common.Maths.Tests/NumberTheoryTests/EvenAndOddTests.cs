namespace Elephant.Common.Maths.Tests.NumberTheoryTests
{
	/// <summary>
	/// <see cref="NumberTheory.IsEven"/> and <see cref="NumberTheory.IsOdd"/> tests.
	/// </summary>
	public class EvenAndOddTests
	{
		/// <summary>
		/// <see cref="NumberTheory.IsEven"/> must return true.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0)]
		[InlineData(2)]
		[InlineData(4)]
		[InlineData(6)]
		[InlineData(8)]
		[InlineData(10)]
		[InlineData(20)]
		[InlineData(100)]
		[InlineData(1000)]
		[InlineData(8762)]
		[InlineData(-2)]
		[InlineData(-4)]
		[InlineData(-8)]
		[InlineData(-10)]
		[InlineData(-20)]
		[InlineData(-2356)]
		public void IsEvenReturnsTrue(int value)
		{
			// Act.
			bool isEven = NumberTheory.IsEven(value);

			// Assert.
			Assert.True(isEven);
		}

		/// <summary>
		/// <see cref="NumberTheory.IsEven"/> must return false.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(1)]
		[InlineData(3)]
		[InlineData(5)]
		[InlineData(7)]
		[InlineData(9)]
		[InlineData(11)]
		[InlineData(21)]
		[InlineData(101)]
		[InlineData(1001)]
		[InlineData(8763)]
		[InlineData(-3)]
		[InlineData(-5)]
		[InlineData(-7)]
		[InlineData(-9)]
		[InlineData(-21)]
		[InlineData(-2357)]
		public void IsEvenReturnsFalse(int value)
		{
			// Act.
			bool isEven = NumberTheory.IsEven(value);

			// Assert.
			Assert.False(isEven);
		}

		/// <summary>
		/// <see cref="NumberTheory.IsOdd"/> must return true.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(1)]
		[InlineData(3)]
		[InlineData(5)]
		[InlineData(7)]
		[InlineData(9)]
		[InlineData(11)]
		[InlineData(21)]
		[InlineData(101)]
		[InlineData(1001)]
		[InlineData(8763)]
		[InlineData(-3)]
		[InlineData(-5)]
		[InlineData(-7)]
		[InlineData(-9)]
		[InlineData(-21)]
		[InlineData(-2357)]

		public void IsOddReturnsTrue(int value)
		{
			// Act.
			bool isOdd = NumberTheory.IsOdd(value);

			// Assert.
			Assert.True(isOdd);
		}

		/// <summary>
		/// <see cref="NumberTheory.IsOdd"/> must return false.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0)]
		[InlineData(2)]
		[InlineData(4)]
		[InlineData(6)]
		[InlineData(8)]
		[InlineData(10)]
		[InlineData(20)]
		[InlineData(100)]
		[InlineData(1000)]
		[InlineData(8762)]
		[InlineData(-2)]
		[InlineData(-4)]
		[InlineData(-8)]
		[InlineData(-10)]
		[InlineData(-20)]
		[InlineData(-2356)]

		public void IsOddReturnsFalse(int value)
		{
			// Act.
			bool isOdd = NumberTheory.IsOdd(value);

			// Assert.
			Assert.False(isOdd);
		}
	}
}