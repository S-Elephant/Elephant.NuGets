namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="MergeSortExtensions"/> list tests.
	/// </summary>
	public class MergeSortListTests
	{
		/// <summary>
		/// Tests the MergeSort extension method to ensure it correctly sorts a list of integers.
		/// This test is parameterized to run multiple scenarios using different sets of input lists
		/// and expected outcomes, which are provided by the SortingTestData method.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			unsortedList.MergeSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}
	}
}
