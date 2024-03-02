namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="TreeSortExtensions"/> array tests.
	/// </summary>
	public class TreeSortArrayTests
	{
		/// <summary>
		/// Tests the TreeSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the TreeSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			unsortedArray.TreeSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}
	}
}