namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="CountingSortExtensions"/> list tests.
	/// </summary>
	public class CountingSortListTests
	{
		/// <summary>
		/// Tests the CountingSort extension method to ensure it sorts a list correctly.
		/// Each set of data is used to verify that the CountingSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			unsortedList.CountingSort(IntKeySelector);

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
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