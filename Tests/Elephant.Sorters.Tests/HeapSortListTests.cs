namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="HeapSortExtensions"/> list tests.
	/// </summary>
	public class HeapSortListTests
	{
		/// <summary>
		/// Tests the HeapSort extension method to ensure it sorts a list correctly.
		/// This test method uses different sets of input and expected values provided by the SortCorrectlyTestData method.
		/// Each set of data is used to verify that the HeapSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			unsortedList.HeapSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}
	}
}