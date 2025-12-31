namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="HeapSortExtensions"/> array tests.
	/// </summary>
	public class HeapSortArrayTests
	{
		/// <summary>
		/// Tests the HeapSort extension method to ensure it sorts an array correctly.
		/// This test method uses different sets of input and expected values provided by the SortCorrectlyTestData method.
		/// Each set of data is used to verify that the HeapSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			_ = unsortedArray.HeapSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}
	}
}