namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="MergeSortExtensions"/> array tests.
	/// </summary>
	public class MergeSortArrayTests
	{
		/// <summary>
		/// Tests the MergeSort extension method to ensure it correctly sorts an array of integers.
		/// This test is parameterized to run multiple scenarios using different sets of input arrays
		/// and expected outcomes, which are provided by the SortingTestData method.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			_ = unsortedArray.MergeSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}
	}
}
