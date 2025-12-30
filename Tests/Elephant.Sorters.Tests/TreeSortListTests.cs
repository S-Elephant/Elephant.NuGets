namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="TreeSortExtensions"/> list tests.
	/// </summary>
	public class TreeSortListTests
	{
		/// <summary>
		/// Tests the TreeSort extension method to ensure it sorts a list correctly.
		/// Each set of data is used to verify that the TreeSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			_ = unsortedList.TreeSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}
	}
}