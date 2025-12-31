namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="InsertionSortExtensions"/> list tests.
	/// </summary>
	public class InsertionSortListTests
	{
		/// <summary>
		/// Tests the InsertionSort extension method to ensure it sorts a list correctly.
		/// Each set of data is used to verify that the InsertionSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			_ = unsortedList.InsertionSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}
	}
}