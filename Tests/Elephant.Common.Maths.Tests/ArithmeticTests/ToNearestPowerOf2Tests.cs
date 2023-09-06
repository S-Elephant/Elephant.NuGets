namespace Elephant.Common.Maths.Tests.ArithmeticTests
{
	/// <summary>
	/// <see cref="Arithmetics.ToNearestPowerOfTwo(int)"/> tests.
	/// </summary>
	public class ToNearestPowerOf2Tests
	{
		/// <summary>
		/// <see cref="Arithmetics.ToNearestPowerOfTwo(int)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(int.MinValue, 1)]
		[InlineData(-512, 1)]
		[InlineData(-1, 1)]
		[InlineData(0, 1)]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(3, 2)]
		[InlineData(4, 4)]
		[InlineData(5, 4)]
		[InlineData(6, 4)]
		[InlineData(7, 8)]
		[InlineData(8, 8)]
		[InlineData(14000, 16384)]
		[InlineData(22000, 16384)]
		[InlineData(int.MaxValue, int.MaxValue)]
		public void ToNearestPowerOfTwo32Bit(int value, int expected)
		{
			// Act.
			int result = Arithmetics.ToNearestPowerOfTwo(value);

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="Arithmetics.ToNearestPowerOfTwo(long)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(long.MinValue, 1)]
		[InlineData(-512, 1)]
		[InlineData(-1, 1)]
		[InlineData(0, 1)]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(3, 2)]
		[InlineData(4, 4)]
		[InlineData(5, 4)]
		[InlineData(6, 4)]
		[InlineData(7, 8)]
		[InlineData(8, 8)]
		[InlineData(14000, 16384)]
		[InlineData(22000, 16384)]
		[InlineData(long.MaxValue, long.MaxValue)]
		public void ToNearestPowerOfTwo64Bit(long value, long expected)
		{
			// Act.
			long result = Arithmetics.ToNearestPowerOfTwo(value);

			// Assert.
			Assert.Equal(expected, result);
		}
	}
}