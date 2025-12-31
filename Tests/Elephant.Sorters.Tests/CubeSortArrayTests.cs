namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="CubeSortExtensions"/> array tests.
	/// </summary>
	public class CubeSortArrayTests
	{
		/// <summary>
		/// Tests the CubeSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the CubeSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(CorrectArraySortingData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Act.
			_ = unsortedArray.CubeSort();

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}

		/// <summary>
		/// Provides a series of test data for testing sorters.
		/// Each item in the series represents a test case, consisting of an unsorted array and the array tis expected state after sorting.
		/// </summary>
		/// <returns>A series of test data.</returns>
		public static TheoryData<int[], int[]> CorrectArraySortingData { get; } = new TheoryData<int[], int[]>
		{
			{ Array.Empty<int>(), Array.Empty<int>() }, // Test with an empty array.
			{ new[] { 1 }, new[] { 1 } }, // Test with a single element array.
			{ new[] { 2, 1 }, new[] { 1, 2 } }, // Test with two elements.
			{ new[] { 3, 2, 1, 1 }, new[] { 1, 1, 2, 3 } }, // Test with four elements.
			{ new[] { 5, 3, 8, 4, 2, 5, 4, 1 }, new[] { 1, 2, 3, 4, 4, 5, 5, 8 } }, // Test with multiple elements.
		};

		/// <summary>
		/// Tests the CubeSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the CubeSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(BadSortingData))]
		public void ThrowsBecauseOfCollectionSize(int[] unsortedArray)
		{
			// Act & Assert.
			_ = Assert.Throws<ArgumentException>(() => unsortedArray.CubeSort());
		}

		/// <summary>
		/// Provides a series of test data for testing sorters.
		/// Each item in the series represents a test case, consisting of an unsorted array and the array tis expected state after sorting.
		/// </summary>
		/// <returns>A series of test data.</returns>
		public static TheoryData<int[]> BadSortingData { get; } = new TheoryData<int[]>
		{
			new[] { 3, 2, 1 }, // Test with three elements.
		};
	}
}