namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="QuickSortExtensions"/> list tests.
	/// </summary>
	public class QuickSortListTests
	{
		/// <summary>
		/// Tests the QuickSort method with various lists to ensure it correctly sorts them.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			_ = unsortedList.QuickSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}

		/// <summary>
		/// Tests the QuickSort method with various lists to ensure it correctly sorts them.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(QuickSortWithIndicesSortsCorrectlyTestData))]
		public void QuickSortWithIndicesSortsCorrectly(int[] unsortedArray, int[] expectedArray, int leftIndex, int rightIndex)
		{
			// Arrange.
			List<int> unsorted = new(unsortedArray);
			List<int> expected = new(expectedArray);

			// Act.
			_ = unsorted.QuickSort(leftIndex, rightIndex);

			// Assert.
			Assert.Equal(expected, unsorted);
		}

		/// <summary>
		/// Provides different sets of unsorted and expected sorted arrays for testing the QuickSort method.
		/// Each item contains an unsorted array, expected sorted array, and index range for sorting.
		/// </summary>
		/// <returns>Series of test data.</returns>
		public static TheoryData<int[], int[], int, int> QuickSortWithIndicesSortsCorrectlyTestData { get; } = new()
		{
			{ Array.Empty<int>(), Array.Empty<int>(), 0, -1 }, // No elements to sort.
			{ new[] { 1 }, new[] { 1 }, 0, 0 }, // Single element.
			{ new[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 99, 5 }, new[] { 1, 1, 2, 3, 3, 4, 5, 5, 5, 6, 9, 99 }, 0, 11 }, // Full list.
			{ new[] { 2, 3, 1 }, new[] { 2, 1, 3 }, 1, 2 }, // Partial sort, only sort positions 1 to 2.
			{ new[] { 8, 4, 3, 7, 6, 5, 2 }, new[] { 8, 3, 4, 5, 6, 7, 2 }, 1, 5 }, // Sort from position 1 to 5.
			{ new[] { 10, 20, 30, 40, 50 }, new[] { 10, 20, 30, 40, 50 }, 0, 4 }, // Already sorted.
			{ new[] { 99, 85, 70, 65, 30, 25 }, new[] { 99, 25, 30, 65, 70, 85 }, 1, 5 }, // Reverse sorted segment.
		};

		/// <summary>
		/// Tests the QuickSort method throws a <see cref="ArgumentOutOfRangeException"/>
		/// if the right index parameter is out of range.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ThrowsIfRightIndexOutOfRange()
		{
			// Arrange.
			List<int> data = new() { 0, 1, 2 };

			// Act.
			Exception? exception = Record.Exception(() => data.QuickSort(1, 7));

			// Assert.
			Assert.NotNull(exception);
		}

		/// <summary>
		/// Tests the QuickSort method throws a <see cref="ArgumentOutOfRangeException"/>
		/// if the right index parameter is out of range.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void DoesNotThrowIfLeftIndexOutOfRange()
		{
			// Arrange.
			List<int> data = new() { 0, 1, 2 };

			// Act.
			Exception? exception = Record.Exception(() => data.QuickSort(-1000, 2));

			// Assert.
			Assert.IsNotType<ArgumentOutOfRangeException>(exception);
		}
	}
}