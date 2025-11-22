namespace Elephant.Extensions.Tests
{
	/// <summary>
	/// <see cref="BetweenExtensions"/> tests.
	/// </summary>
	public class BetweenExtensionsTests
	{
		/// <summary>
		/// <see cref="BetweenExtensions.IsBetweenII{T}(T, T, T)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(5.0f, 5.0f, 5.0f, true)]
		[InlineData(5.0f, 4.9f, 5.0f, true)]
		[InlineData(5.0f, 4.9f, 5.1f, true)]
		[InlineData(5.0f, 5.1f, 4.9f, false)]
		// ReSharper disable once InconsistentNaming
		public void IsBetweenIITests(float value, float min, float max, bool expectedAreEqual)
		{
			// Act & Assert.
			Assert.Equal(expectedAreEqual, value.IsBetweenII(min, max));
		}

		/// <summary>
		/// <see cref="BetweenExtensions.IsBetweenIE{T}(T, T, T)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(5.0f, 5.0f, 5.0f, false)]
		[InlineData(5.0f, 4.9f, 5.0f, false)]
		[InlineData(5.0f, 4.9f, 5.1f, true)]
		[InlineData(5.0f, 5.1f, 4.9f, false)]
		// ReSharper disable once InconsistentNaming
		public void IsBetweenIETests(float value, float min, float max, bool expectedAreEqual)
		{
			// Act & Assert.
			Assert.Equal(expectedAreEqual, value.IsBetweenIE(min, max));
		}

		/// <summary>
		/// <see cref="BetweenExtensions.IsBetweenEI{T}(T, T, T)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(5.0f, 5.0f, 5.0f, false)]
		[InlineData(5.0f, 4.9f, 5.0f, true)]
		[InlineData(5.0f, 4.9f, 5.1f, true)]
		[InlineData(5.0f, 5.1f, 4.9f, false)]
		// ReSharper disable once InconsistentNaming
		public void IsBetweenEITests(float value, float min, float max, bool expectedAreEqual)
		{
			// Act & Assert.
			Assert.Equal(expectedAreEqual, value.IsBetweenEI(min, max));
		}

		/// <summary>
		/// <see cref="BetweenExtensions.IsBetweenEE{T}(T, T, T)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(5.0f, 5.0f, 5.0f, false)]
		[InlineData(5.0f, 4.9f, 5.0f, false)]
		[InlineData(5.0f, 4.9f, 5.1f, true)]
		[InlineData(5.0f, 5.1f, 4.9f, false)]
		// ReSharper disable once InconsistentNaming
		public void IsBetweenEETests(float value, float min, float max, bool expectedAreEqual)
		{
			// Act & Assert.
			Assert.Equal(expectedAreEqual, value.IsBetweenEE(min, max));
		}

		/// <summary>
		/// <see cref="BetweenExtensions.IsBetweenEE{T}(T, T, T)"/> tests.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void IsBetween_IsAliasForIsBetweenII()
		{
			// Arrange.
			int value = 5;

			// Act & Assert.
			Assert.Equal(value.IsBetweenII(1, 10), value.IsBetween(1, 10));
		}

		/// <summary>
		/// <see cref="BetweenExtensions.IsBetweenEE{T}(T, T, T)"/> tests.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void IsBetween_WorksWithInfinity()
		{
			// Assert: positive infinity as value and as max (inclusive) should be true for inclusive max.
			Assert.True(float.PositiveInfinity.IsBetweenII(-1f, float.PositiveInfinity));
			Assert.True(float.PositiveInfinity.IsBetweenEI(-1f, float.PositiveInfinity));

			// Assert: exclusive max should be false when value equals max.
			Assert.False(float.PositiveInfinity.IsBetweenEE(-1f, float.PositiveInfinity));
			Assert.False(float.PositiveInfinity.IsBetweenIE(-1f, float.PositiveInfinity));

			// Assert: negative infinity as min and value equal to min should behave same as other boundaries.
			Assert.True(float.NegativeInfinity.IsBetweenII(float.NegativeInfinity, 0f));
			Assert.False(float.NegativeInfinity.IsBetweenEE(float.NegativeInfinity, 0f));
		}

		/// <summary>
		/// <see cref="BetweenExtensions.IsBetweenEE{T}(T, T, T)"/>
		/// with DateTime value strictly between min and max for inclusive bounds
		/// should return true.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void IsBetween_DateTime_ValueBetweenInclusiveInclusive_ReturnsTrue()
		{
			// Arrange.
			DateTime min = new DateTime(2020, 01, 01);
			DateTime value = new DateTime(2020, 01, 02);
			DateTime max = new DateTime(2020, 01, 03);

			// Act & Assert.
			Assert.True(value.IsBetweenII(min, max));
		}

		/// <summary>
		/// <see cref="BetweenExtensions.IsBetweenEE{T}(T, T, T)"/>
		/// with DateTime exclusive-exclusive (EE) should exclude the lower bound.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void IsBetween_DateTime_MinEqualLowerBound_EE_ExcludesMin()
		{
			// Arrange.
			DateTime min = new DateTime(2020, 01, 01);
			DateTime max = new DateTime(2020, 01, 03);

			// Act & Assert.
			Assert.False(min.IsBetweenEE(min, max));
		}

		/// <summary>
		/// <see cref="BetweenExtensions.IsBetweenEE{T}(T, T, T)"/>
		/// with DateTime inclusive-exclusive should exclude the upper bound.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void IsBetween_DateTime_MaxEqualUpperBound_IE_ExcludesMax()
		{
			// Arrange.
			DateTime min = new DateTime(2020, 01, 01);
			DateTime max = new DateTime(2020, 01, 03);

			// Act & Assert.
			Assert.False(max.IsBetweenIE(min, max));
		}
	}
}
