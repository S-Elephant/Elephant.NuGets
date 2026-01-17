namespace Elephant.Texts.Tests.Truncators
{
	/// <summary>
	/// <see cref="Truncator.TruncateWithEllipsis"/> tests.
	/// </summary>
	public class TruncateWithEllipsisTests
	{
		/// <summary>
		/// Non-null wrapper returns the original value when no truncation is required.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsOriginal()
		{
			// Act.
			string result = Truncator.TruncateWithEllipsis("Hello", 10);

			// Assert.
			Assert.Equal("Hello", result);
		}

		/// <summary>
		/// Non-null wrapper throws <see cref="ArgumentNullException"/> when called with null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullValue_ThrowsArgumentNullException()
		{
			// Act & Assert.
			_ = Assert.Throws<ArgumentNullException>(() => Truncator.TruncateWithEllipsis(null!, 5));
		}
	}
}