namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="RadixSortExtensions"/> list tests.
	/// </summary>
	public class RadixSortListTests
	{
		/// <summary>
		/// Tests the RadixSort extension method to ensure it sorts a list correctly.
		/// Each set of data is used to verify that the RadixSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			unsortedList.RadixSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}
	}
}