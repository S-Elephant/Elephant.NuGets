namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="CountingSortExtensions"/> array tests.
	/// </summary>
	public class CountingSortArrayTests
	{
		/// <summary>
		/// Tests the CountingSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the CountingSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			unsortedArray.CountingSort(IntKeySelector);

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}

		/// <summary>
		/// Provides a key selector function for sorting integer lists using CountingSort.
		/// This selector simply returns the integer itself, as CountingSort requires
		/// a key selector that maps elements of the list to integers.
		/// </summary>
		/// <param name="item">The integer item from the list.</param>
		/// <returns>The original integer, to be used as its own sorting key.</returns>
		private static int IntKeySelector(int item)
		{
			return item;
		}
	}
}