namespace Elephant.Common.Maths.Tests.NumberTheoryTests
{
	/// <summary>
	/// <see cref="NumberTheory.IsPrime"/> tests.
	/// </summary>
	public class IsPrimeTests
	{
		/// <summary>
		/// <see cref="NumberTheory.IsPrime"/> must return true.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(5)]
		[InlineData(7)]
		[InlineData(11)]
		[InlineData(13)]
		[InlineData(17)]
		[InlineData(19)]
		[InlineData(23)]
		[InlineData(29)]
		[InlineData(31)]
		[InlineData(37)]
		[InlineData(41)]
		[InlineData(43)]
		[InlineData(47)]
		[InlineData(53)]
		[InlineData(59)]
		[InlineData(61)]
		[InlineData(67)]
		[InlineData(71)]
		[InlineData(73)]
		[InlineData(79)]
		[InlineData(83)]
		[InlineData(89)]
		[InlineData(97)]

		public void IsPrimeReturnsTrue(int value)
		{
			// Act.
			bool isPrime = NumberTheory.IsPrime(value);

			// Assert.
			Assert.True(isPrime);
		}

		/// <summary>
		/// <see cref="NumberTheory.IsPrime"/> must return false.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(int.MinValue)]
		[InlineData(-2)]
		[InlineData(-3)]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(4)]
		[InlineData(6)]
		[InlineData(8)]
		[InlineData(9)]
		[InlineData(10)]
		[InlineData(90)]
		[InlineData(95)]
		[InlineData(96)]

		public void IsPrimeReturnsFalse(int value)
		{
			// Act.
			bool isPrime = NumberTheory.IsPrime(value);

			// Assert.
			Assert.False(isPrime);
		}
	}
}