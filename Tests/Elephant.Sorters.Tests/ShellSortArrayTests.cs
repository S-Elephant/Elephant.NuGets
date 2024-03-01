namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="ShellSortExtensions"/> array tests.
	/// </summary>
	public class ShellSortArrayTests
	{
		/// <summary>
		/// Tests the ShellSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the ShellSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			unsortedArray.ShellSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}
	}
}