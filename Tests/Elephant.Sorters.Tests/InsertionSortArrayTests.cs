namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="InsertionSortExtensions"/> array tests.
	/// </summary>
	public class InsertionSortArrayTests
	{
		/// <summary>
		/// Tests the InsertionSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the InsertionSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			unsortedArray.InsertionSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}
	}
}