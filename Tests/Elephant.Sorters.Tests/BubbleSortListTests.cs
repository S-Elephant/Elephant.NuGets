namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="BubbleSortExtensions"/> list tests.
	/// </summary>
	public class BubbleSortListTests
	{
		/// <summary>
		/// Tests the BubbleSort extension method to ensure it sorts a list correctly.
		/// Each set of data is used to verify that the BubbleSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			_ = unsortedList.BubbleSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}
	}
}