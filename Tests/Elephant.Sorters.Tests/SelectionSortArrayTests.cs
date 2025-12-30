namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="SelectionSortExtensions"/> array tests.
	/// </summary>
	public class SelectionSortArrayTests
	{
		/// <summary>
		/// Tests the SelectionSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the SelectionSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			_ = unsortedArray.SelectionSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}
	}
}