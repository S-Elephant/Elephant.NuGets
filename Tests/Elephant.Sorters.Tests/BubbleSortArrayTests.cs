namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="BubbleSortExtensions"/> array tests.
	/// </summary>
	public class BubbleSortArrayTests
	{
		/// <summary>
		/// Tests the BubbleSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the BubbleSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			unsortedArray.BubbleSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}
	}
}