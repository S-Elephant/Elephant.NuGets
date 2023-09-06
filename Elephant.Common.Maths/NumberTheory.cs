namespace Elephant.Common.Maths
{
	/// <summary>
	/// Investigates the properties and relationships of numbers, especially integers.
	/// It's more abstract and often doesn't involve direct computation like arithmetic.
	/// </summary>
	public static class NumberTheory
	{
		/// <summary>
		/// Determine if <paramref name="value"/> a prime number.
		/// A prime number is a number that can only be divided by itself and 1 without
		/// remainders but a value less or equal to 1 is never a prime number.
		/// </summary>
		/// <param name="value">Value to check.</param>
		/// <returns><c>true</c> if <paramref name="value"/> is a prime number.</returns>
		public static bool IsPrime(int value)
		{
			// Base case: numbers less than or equal to 1 are not primes.
			if (value <= 1)
				return false;

			// 2 is the only even prime number.
			// This check is performed for performance reasons.
			if (value == 2)
				return true;

			// Exclude even numbers greater than 2.
			// Optimized case for even numbers greater than 2. We know they can't be prime
			// and therefore return false immediately.
			if (value % 2 == 0)
				return false;

			// Check divisors up to the square root of value.
			// Start from 3 and increase by 2 (since we've already excluded even numbers).
			for (int i = 3; i <= Math.Sqrt(value); i += 2)
			{
				if (value % i == 0)
					return false;  // value has a divisor other than 1 and itself.
			}

			// If we get here, value must be a prime number.
			return true;
		}

		/// <summary>
		/// Calculate the Greatest Common Divisor (GCD) of two integers using the
		/// Euclidean algorithm. E.g. the numbers 12 and 8 can both be divided by 4
		/// but not by any bigger value without a fractional result, GCD is 4.
		/// Its complexity is O(log min(a,b)), which is very efficient.
		/// A negative result is converted into a positive result.
		/// </summary>
		/// <param name="a">First integer.</param>
		/// <param name="b">Second integer.</param>
		/// <returns>Greatest common divisor of <paramref name="a"/> and <paramref name="b"/>.
		/// A negative result is converted into a positive result.</returns>
		public static int GreatestCommonDivisor(int a, int b)
		{
			// Loop continues until b becomes zero.
			while (b != 0)
			{
				// Store the value of b in a temporary variable.
				int temp = b;

				// Update b to be the remainder when a is divided by b.
				b = a % b;

				// Update a to be the value stored in the temporary variable.
				a = temp;
			}

			// When b becomes zero, the GCD is stored in a.
			// a may be negative, ensure that it is always positive.
			return Math.Abs(a);
		}

		/// <summary>
		/// Calculates the Least Common Multiple (LCM) of two integers using their Greatest Common Divisor (GCD).
		/// A negative result is converted into a positive result.
		/// E.g. LCM of 8 and 12 is 24. 3x8 = 24, 2x12 = 24. 1x8 can never be 12, thus 24 is the LCM.
		/// </summary>
		/// <param name="a">First integer.</param>
		/// <param name="b">Second integer.</param>
		/// <returns>The LCM of the two given integers. Returns 0 if both inputs are zero.
		/// A negative result is converted into a positive result.</returns>
		public static int LeastCommonMultiple(int a, int b)
		{
			// Check explicitly for zero to avoid the division by zero error.
			if (a == 0 || b == 0)
				return 0;

			// Calculate the absolute value of a * b to ensure the LCM is positive.
			int product = Math.Abs(a * b);

			// Calculate the LCM using the GCD
			// a and/or b may be negative, ensure that they are always positive.
			return product / GreatestCommonDivisor(Math.Abs(a), Math.Abs(b));
		}

		/// <summary>
		/// Calculates the Least Common Multiple (LCM) of two integers using their Greatest Common Divisor (GCD).
		/// A negative result is converted into a positive result.
		/// E.g. LCM of 8, 16, 24 is 48. 6x8 = 48, 4x12 = 48, 2x24 = 48. There's no smaller multiplication possible
		/// and thus the LCM is 48.
		/// </summary>
		/// <param name="values">Array of integers for which to find the LCM.</param>
		/// <returns>
		/// The LCM of all the numbers in the given array.
		/// Returns 0 if the array is empty or if any number in the array is zero.
		/// A negative result is converted into a positive result.
		/// </returns>
		public static int LeastCommonMultiple(params int[] values)
		{
			// Check for an empty array and return 0 if it is empty.
			if (values.Length == 0)
				return 0;

			// Initialize LCM to the absolute value of the first number in the array.
			int lcm = Math.Abs(values[0]);

			// Loop through each number in the array, starting from the second element.
			foreach (int number in values.Skip(1))
			{
				// If LCM or any number in the array is zero, return 0 to avoid division by zero.
				if (lcm == 0 || number == 0)
					return 0;

				// Calculate the GCD of the absolute values of the current lcm and num.
				int gcd = GreatestCommonDivisor(Math.Abs(lcm), Math.Abs(number));

				// Update lcm using the formula: lcm(a, b) = |a * b| / gcd(a, b).
				lcm = Math.Abs(lcm * number) / gcd;
			}

			// Return the calculated LCM.
			return lcm;
		}

		/// <summary>
		/// Determines if <paramref name="value"/> is even.
		/// </summary>
		/// <param name="value">The integer to be checked.</param>
		/// <returns>True if the integer is even, otherwise false.</returns>
		public static bool IsEven(int value)
		{
			return value % 2 == 0;
		}

		/// <summary>
		/// Determines if <paramref name="value"/> is odd.
		/// </summary>
		/// <param name="value">The integer to be checked.</param>
		/// <returns>True if the integer is odd, otherwise false.</returns>
		public static bool IsOdd(int value)
		{
			return value % 2 != 0;
		}
	}
}
