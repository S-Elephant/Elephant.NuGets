namespace Elephant.Common.Maths
{
	/// <summary>
	/// Concerned with counting, arrangement, and combination. This area often utilizes factorials,
	/// a concept you can calculate using basic arithmetic but which has wider applications in
	/// various branches of mathematics.
	/// </summary>
	public static class Combinatorics
	{
		/// <summary>
		/// Computes the factorial of <paramref name="value"/>.
		/// The factorial of 4! is calculated as 4 x 3 x 2 x 1 which is 24.
		/// </summary>
		/// <param name="value">The integer whose factorial is to be calculated.</param>
		/// <returns>Returns the factorial of <paramref name="value"/> if it is a non-negative integer.</returns>
		/// <exception cref="ArgumentException">Thrown when the argument is a negative number.</exception>
		/// <exception cref="OverflowException">Thrown when the factorial calculation would result in an overflow.</exception>
		public static long Factorial(int value)
		{
			// Check if n is negative, and if so, throw an exception
			if (value < 0)
				throw new ArgumentException($"Factorial is not defined for negative integers. Received: {value}.");

			// Factorial of a number in mathematics is the product of all the positive numbers less than or equal to a number.
			// There are no positive values less than zero, therefore the data set cannot be arranged which counts as the possible
			// combination of how data can be arranged. Thus, 0! = 1.
			if (value == 0)
				return 1;

			long result = 1;
			for (int i = 1; i <= value; ++i)
			{
				// Check for overflow before performing the multiplication
				if (result > long.MaxValue / i)
					throw new OverflowException($"Factorial calculation would result in an overflow. Received: {value}.");

				result *= i;
			}

			return result;
		}

		/// <summary>
		/// Calculate the n-th Fibonacci number using a time complexity of
		/// O(n), which is very efficient but will still take a long time for
		/// large numbers like e.g. int.MAxValue.
		/// </summary>
		/// <param name="fibonacciIndex">Index of the Fibonacci number to calculate.</param>
		/// <returns>n-th Fibonacci number.</returns>
		public static int Fibonacci(int fibonacciIndex)
		{
			// Base case: If n is 0 or 1, the Fibonacci number is n itself.
			if (fibonacciIndex == 0 || fibonacciIndex == 1)
				return fibonacciIndex;

			bool isNegative = fibonacciIndex < 0;
			fibonacciIndex = Math.Abs(fibonacciIndex);

			// Initialize an array to store already computed Fibonacci numbers.
			int[] fibNumbers = new int[fibonacciIndex + 1];

			// Seed values for the 0-th and 1-st Fibonacci numbers.
			fibNumbers[0] = 0;
			fibNumbers[1] = 1;

			// Calculate Fibonacci numbers from 2 to n.
			for (int i = 2; i <= fibonacciIndex; i++)
			{
				fibNumbers[i] = fibNumbers[i - 1] + fibNumbers[i - 2];
			}

			// The sign for a negative Fibonacci number F(-n) in the generalized Fibonacci sequence
			// alternates according to the formula F(-n) = (-1)^{n+1} times F(n). This means that if
			// n is even, then F(-n) will be the negative of F(n); and if n is odd, then F(-n) will
			// be the same as F(n).
			if (isNegative && fibonacciIndex % 2 == 0)
				return -fibNumbers[fibonacciIndex];

			// Return the n-th Fibonacci number.
			return fibNumbers[fibonacciIndex];
		}
	}
}
