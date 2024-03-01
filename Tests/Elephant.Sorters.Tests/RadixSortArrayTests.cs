namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="RadixSortExtensions"/> array tests.
	/// </summary>
	public class RadixSortArrayTests
	{
		/// <summary>
		/// Tests the RadixSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the RadixSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			unsortedArray.RadixSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}
	}
}