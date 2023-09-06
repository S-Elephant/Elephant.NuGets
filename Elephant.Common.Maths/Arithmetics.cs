namespace Elephant.Common.Maths
{
	/// <summary>
	/// Arithmetic operations.
	/// Arithmetic is the most basic branch of mathematics, focusing on the fundamental operations of addition,
	/// subtraction, multiplication, and division. It is concerned with the manipulation of numbers to solve
	/// basic computational problems. It does not generally involve variables, unlike algebra. In essence,
	/// arithmetic provides the building blocks for other areas of mathematics.
	/// </summary>
	public static class Arithmetics
	{
		/// <summary>
		/// Determines whether a given 32 bit integer is a power of two and that it is greater than zero.
		/// </summary>
		/// <param name="value">32 bit integer to be checked.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="value"/> is a power of two; otherwise, <c>false</c>.
		/// </returns>
		/// <example>
		/// <code>
		/// bool result1 = IsPowerOfTwo(2);  // returns true.
		/// bool result2 = IsPowerOfTwo(3);  // returns false.
		/// bool result3 = IsPowerOfTwo(16); // returns true.
		/// </code>
		/// </example>
		/// <remarks>
		/// A number is considered to be a power of two if it can be written as 2^x
		/// where x is a non-negative integer.
		/// </remarks>
		public static bool IsPowerOfTwo(int value)
		{
			return (value > 0) && ((value & (value - 1)) == 0);
		}

		/// <summary>
		/// Determines whether a given 64 bit integer is a power of two and that it is greater than zero.
		/// </summary>
		/// <param name="value">64 bit integer to be checked.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="value"/> is a power of two; otherwise, <c>false</c>.
		/// </returns>
		/// <example>
		/// <code>
		/// bool result1 = IsPowerOfTwo(2);  // returns true.
		/// bool result2 = IsPowerOfTwo(3);  // returns false.
		/// bool result3 = IsPowerOfTwo(16); // returns true.
		/// </code>
		/// </example>
		/// <remarks>
		/// A number is considered to be a power of two if it can be written as 2^x
		/// where x is a non-negative integer.
		/// </remarks>
		public static bool IsPowerOfTwo(long value)
		{
			return (value > 0) && ((value & (value - 1)) == 0);
		}

		/// <summary>
		/// 'Rounds' a given 32-bit integer to the nearest power of two.
		/// </summary>
		/// <param name="value">32-bit integer to be rounded.</param>
		/// <returns>
		/// Nearest power of two. If the input is less than 1, it returns 1 (the smallest power of two).
		/// If int.MaxValue is the value then int.MaxValue is returned.
		/// </returns>
		/// <example>
		/// <code>
		/// int result1 = ToNearestPowerOfTwo(5);  // returns 4
		/// int result2 = ToNearestPowerOfTwo(15); // returns 16
		/// int result3 = ToNearestPowerOfTwo(33); // returns 32
		/// int result4 = ToNearestPowerOfTwo(50); // returns 64
		/// </code>
		/// </example>
		/// <remarks>
		/// A number is considered to be a power of two if it can be written as 2^x,
		/// where x is a non-negative integer. This function uses bit manipulation for efficient computation.
		/// An extremely large <paramref name="value"/> will take a long time to process.
		/// </remarks>
		public static int ToNearestPowerOfTwo(int value)
		{
			// 2^0 = 1 is the smallest power of 2.
			if (value < 1)
				return 1;

			// The value of int.MaxValue in C# is 2^31 - 1, which is one less than 2^31.
			// The nearest power of 2 for int.MaxValue would be an overflow. Therefore int.MaxValue is returned instead.
			if (value == int.MaxValue)
				return value;

			// If value is already a power of 2, return n.
			if ((value & (value - 1)) == 0)
				return value;

			// Find the next higher power of 2.
			int higher = 1;
			while (higher < value)
				higher <<= 1;

			// Find the previous lower power of 2.
			int lower = higher >> 1;

			// Determine the nearest power of 2: either higher or lower.
			return (higher - value < value - lower) ? higher : lower;
		}

		/// <summary>
		/// 'Rounds' a given 64-bit integer to the nearest power of two.
		/// </summary>
		/// <param name="value">64-bit integer to be rounded.</param>
		/// <returns>
		/// Nearest power of two. If the input is less than 1, it returns 1 (smallest power of two).
		/// If long.MaxValue is the value then long.MaxValue is returned.
		/// </returns>
		/// <example>
		/// <code>
		/// long result1 = ToNearestPowerOfTwo(5);  // returns 4
		/// long result2 = ToNearestPowerOfTwo(15); // returns 16
		/// long result3 = ToNearestPowerOfTwo(33); // returns 32
		/// long result4 = ToNearestPowerOfTwo(50); // returns 64
		/// </code>
		/// </example>
		/// <remarks>
		/// A number is considered to be a power of two if it can be written as 2^x,
		/// where x is a non-negative integer. This function uses bit manipulation for efficient computation.
		/// An extremely large <paramref name="value"/> will take a long time to process.
		/// </remarks>
		public static long ToNearestPowerOfTwo(long value)
		{
			// 2^0 = 1 is the smallest power of 2.
			if (value < 1)
				return 1;

			// The value of long.MaxValue in C# is 2^63 - 1, which is one less than 2^63.
			// The nearest power of 2 for long.MaxValue would be an overflow. Therefore long.MaxValue is returned instead.
			if (value == long.MaxValue)
				return value;

			// If value is already a power of 2, return n.
			if ((value & (value - 1)) == 0)
				return value;

			// Find the next higher power of 2.
			long higher = 1;
			while (higher < value)
				higher <<= 1;

			// Find the previous lower power of 2.
			long lower = higher >> 1;

			// Determine the nearest power of 2: either higher or lower.
			return (higher - value < value - lower) ? higher : lower;
		}
	}
}
