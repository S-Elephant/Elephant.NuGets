namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="QuickSortExtensions"/> array tests.
	/// </summary>
	public class QuickSortArrayTests
	{
		/// <summary>
		/// Tests the QuickSort method with various arrays to ensure it correctly sorts them.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			unsortedArray.QuickSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}

		/// <summary>
		/// Tests the QuickSort method with various arrays to ensure it correctly sorts them.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(QuickSortWithIndicesSortsCorrectlyTestData))]
		public void QuickSortWithIndicesSortsCorrectly(int[] unsorted, int[] expected, int leftIndex, int rightIndex)
		{
			// Act.
			unsorted.QuickSort(leftIndex, rightIndex);

			// Assert.
			Assert.Equal(expected, unsorted);
		}

		/// <summary>
		/// Provides different sets of unsorted and expected sorted arrays for testing the QuickSort method.
		/// Each item is an object array where the first element is an unsorted array of integers
		/// and the second element is the expected result after sorting.
		/// </summary>
		/// <returns>A series of test data.</returns>
		public static IEnumerable<object[]> QuickSortWithIndicesSortsCorrectlyTestData()
		{
			yield return new object[] { Array.Empty<int>(), Array.Empty<int>(), 0, -1 }; // No elements to sort.
			yield return new object[] { new[] { 1 }, new[] { 1 }, 0, 0 }; // Single element.
			yield return new object[]
			{
				new[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 99, 5 },
				new[] { 1, 1, 2, 3, 3, 4, 5, 5, 5, 6, 9, 99 },
				0, 11,
			}; // Full array.
			yield return new object[]
			{
				new[] { 2, 3, 1 },
				new[] { 2, 1, 3 },
				1, 2,
			}; // Partial sort, only sort positions 1 to 2.
			yield return new object[]
			{
				new[] { 8, 4, 3, 7, 6, 5, 2 },
				new[] { 8, 3, 4, 5, 6, 7, 2 },
				1, 5,
			}; // Sort from position 1 to 5.
			yield return new object[]
			{
				new[] { 10, 20, 30, 40, 50 },
				new[] { 10, 20, 30, 40, 50 },
				0, 4,
			}; // Already sorted.
			yield return new object[]
			{
				new[] { 99, 85, 70, 65, 30, 25 },
				new[] { 99, 25, 30, 65, 70, 85 },
				1, 5,
			}; // Reverse sorted segment.
		}

		/// <summary>
		/// Tests the QuickSort method throws a <see cref="ArgumentOutOfRangeException"/>
		/// if the right index parameter is out of range.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ThrowsIfRightIndexOutOfRange()
		{
			// Arrange.
			int[] data = { 0, 1, 2 };

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
			int[] data = { 0, 1, 2 };

			// Act.
			Exception? exception = Record.Exception(() => data.QuickSort(-1000, 2));

			// Assert.
			Assert.IsNotType<ArgumentOutOfRangeException>(exception);
		}
	}
}