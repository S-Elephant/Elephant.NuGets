namespace Elephant.Extensions.Tests
{
	/// <summary>
	/// <see cref="RecycleExtensions"/> tests.
	/// </summary>
	public class RecycleExtensionsTests
	{
		/// <summary>
		/// <see cref="RecycleExtensions.Recycle(int, int, int)"/>
		/// tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(5, 5, 5, 5)]
		[InlineData(-534, 0, 11, 0)]
		[InlineData(5, 0, 5, 5)]
		[InlineData(5, -5, 5, 5)]
		[InlineData(5, -5, 4, -4)]
		[InlineData(5, 1000, 4, 1000)]
		[InlineData(5, 1000, 1000, 1000)]
		[InlineData(5, -1000, -1000, -1000)]
		[InlineData(5, -1000, -2000, -1000)]
		[InlineData(int.MinValue, int.MinValue, int.MaxValue, int.MinValue)]
		[InlineData(int.MaxValue, int.MinValue, int.MaxValue, int.MaxValue)]
		public void RecycleTests(int value, int min, int max, int expectedValue)
		{
			// Act & Assert.
			Assert.Equal(expectedValue, value.Recycle(max, min));
		}

		/// <summary>
		/// <see cref="RecycleExtensions.Recycle(int?, int, int)"/>
		/// (nullable) tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(5, 5, 5, 5)]
		[InlineData(-534, 0, 11, 0)]
		[InlineData(5, 0, 5, 5)]
		[InlineData(5, -5, 5, 5)]
		[InlineData(5, -5, 4, -4)]
		[InlineData(5, 1000, 4, 1000)]
		[InlineData(5, 1000, 1000, 1000)]
		[InlineData(5, -1000, -1000, -1000)]
		[InlineData(5, -1000, -2000, -1000)]
		[InlineData(int.MinValue, int.MinValue, int.MaxValue, int.MinValue)]
		[InlineData(int.MaxValue, int.MinValue, int.MaxValue, int.MaxValue)]
		[InlineData(null, int.MinValue, int.MaxValue, null)]
		public void RecycleNullableTests(int? value, int min, int max, int? expectedValue)
		{
			// Act & Assert.
			Assert.Equal(expectedValue, value.Recycle(max, min));
		}

		/// <summary>
		/// <see cref="RecycleExtensions.RecycleOne(int, int)"/>
		/// tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(5, 5, 5)]
		[InlineData(-534, 11, 1)]
		[InlineData(5, 4, 2)]
		[InlineData(5, 3, 3)]
		[InlineData(5, -1000, 1)]
		[InlineData(int.MinValue, int.MaxValue, 1)]
		[InlineData(int.MaxValue, int.MaxValue, int.MaxValue)]
		public void RecycleOneTests(int value, int max, int expectedValue)
		{
			// Act & Assert.
			Assert.Equal(expectedValue, value.RecycleOne(max));
		}

		/// <summary>
		/// <see cref="RecycleExtensions.RecycleOne(int?, int)"/>
		/// (nullable) tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(5, 5, 5)]
		[InlineData(-534, 11, 1)]
		[InlineData(5, 4, 2)]
		[InlineData(5, 3, 3)]
		[InlineData(5, -1000, 1)]
		[InlineData(int.MinValue, int.MaxValue, 1)]
		[InlineData(int.MaxValue, int.MaxValue, int.MaxValue)]
		[InlineData(null, int.MaxValue, null)]
		public void RecycleOneNullableTests(int? value, int max, int? expectedValue)
		{
			// Act & Assert.
			Assert.Equal(expectedValue, value.RecycleOne(max));
		}
	}
}
