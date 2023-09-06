namespace Elephant.Common.Maths.Tests.ArithmeticTests
{
	/// <summary>
	/// <see cref="Arithmetics.IsPowerOfTwo(int)"/> and <see cref="Arithmetics.ToNearestPowerOfTwo(long)"/> tests.
	/// </summary>
	public class IsPowerOfTwoTests
	{
		/// <summary>
		/// <see cref="Arithmetics.IsPowerOfTwo(int)"/> must return true for values
		/// that are a power of 2.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(4)]
		[InlineData(8)]
		[InlineData(16)]
		[InlineData(32)]
		[InlineData(64)]
		[InlineData(128)]
		[InlineData(256)]
		[InlineData(512)]
		[InlineData(1024)]
		[InlineData(2048)]
		[InlineData(4096)]
		[InlineData(8192)]
		[InlineData(16384)]
		[InlineData(32768)]
		[InlineData(524288)]
		public void PowerOf2ReturnsTrue32Bit(int value)
		{
			// Act.
			bool isPowerOfTwo = Arithmetics.IsPowerOfTwo(value);

			// Assert.
			Assert.True(isPowerOfTwo);
		}

		/// <summary>
		/// <see cref="Arithmetics.IsPowerOfTwo(long)"/> must return true for values
		/// that are a power of 2.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(4)]
		[InlineData(8)]
		[InlineData(16)]
		[InlineData(32)]
		[InlineData(64)]
		[InlineData(128)]
		[InlineData(256)]
		[InlineData(512)]
		[InlineData(1024)]
		[InlineData(2048)]
		[InlineData(4096)]
		[InlineData(8192)]
		[InlineData(16384)]
		[InlineData(32768)]
		[InlineData(524288)]
		public void PowerOf2ReturnsTrue64Bit(long value)
		{
			// Act.
			bool isPowerOfTwo = Arithmetics.IsPowerOfTwo(value);

			// Assert.
			Assert.True(isPowerOfTwo);
		}

		/// <summary>
		/// <see cref="Arithmetics.ToNearestPowerOfTwo(int)"/> must return false for values
		/// that are not a power of 2.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-2)]
		[InlineData(int.MinValue)]
		[InlineData(3)]
		[InlineData(5)]
		[InlineData(63)]
		[InlineData(412)]
		[InlineData(500)]
		[InlineData(91000)]
		[InlineData(524289)]
		[InlineData(int.MaxValue)]
		public void PowerOf2ReturnsFalse32Bit(int value)
		{
			Assert.False(Arithmetics.IsPowerOfTwo(value));
		}

		/// <summary>
		/// <see cref="Arithmetics.ToNearestPowerOfTwo(int)"/> must return false for values
		/// that are not a power of 2.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-2)]
		[InlineData(long.MinValue)]
		[InlineData(3)]
		[InlineData(5)]
		[InlineData(63)]
		[InlineData(412)]
		[InlineData(500)]
		[InlineData(91000)]
		[InlineData(524289)]
		[InlineData(long.MaxValue)]
		public void PowerOf2ReturnsFalse64Bit(long value)
		{
			Assert.False(Arithmetics.IsPowerOfTwo(value));
		}
	}
}