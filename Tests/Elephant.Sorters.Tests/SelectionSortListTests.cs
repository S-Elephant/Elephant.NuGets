namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="SelectionSortExtensions"/> list tests.
	/// </summary>
	public class SelectionSortListTests
	{
		/// <summary>
		/// Tests the SelectionSort extension method to ensure it sorts a list correctly.
		/// Each set of data is used to verify that the SelectionSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			_ = unsortedList.SelectionSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}
	}
}