namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="ShellSortExtensions"/> list tests.
	/// </summary>
	public class ShellSortListTests
	{
		/// <summary>
		/// Tests the ShellSort extension method to ensure it sorts a list correctly.
		/// Each set of data is used to verify that the ShellSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			_ = unsortedList.ShellSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}
	}
}