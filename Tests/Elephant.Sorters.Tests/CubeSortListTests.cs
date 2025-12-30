namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="CubeSortExtensions"/> list tests.
	/// </summary>
	public class CubeSortListTests
	{
		/// <summary>
		/// Tests the CubeSort extension method to ensure it sorts a list correctly.
		/// Each set of data is used to verify that the CubeSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(CorrectListSortingData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Arrange.
			List<int> unsortedList = new(unsortedArray);
			List<int> expectedSortedList = new(expectedSortedArray);

			// Act.
			_ = unsortedList.CubeSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}

		/// <summary>
		/// Provides a series of test data for testing sorters.
		/// Each item in the series represents a test case, consisting of an unsorted list and the list's expected state after sorting.
		/// </summary>
		/// <returns>Series of test data.</returns>
		public static TheoryData<int[], int[]> CorrectListSortingData { get; } = new TheoryData<int[], int[]>
		{
			{ Array.Empty<int>(), Array.Empty<int>() }, // Test with an empty list.
			{ new[] { 1 }, new[] { 1 } }, // Test with a single element list.
			{ new[] { 2, 1 }, new[] { 1, 2 } }, // Test with two elements.
			{ new[] { 3, 2, 1, 5 }, new[] { 1, 2, 3, 5 } }, // Test with three elements.
			{ new[] { 5, 3, 8, 4, 2, 5, 4, 1 }, new[] { 1, 2, 3, 4, 4, 5, 5, 8 } }, // Test with multiple elements.
		};

		/// <summary>
		/// Tests the CubeSort extension method to ensure it throws when the collection size is invalid.
		/// Each set of data is used to verify that the CubeSort method correctly throws an exception for invalid input.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(BadSortingData))]
		public void ThrowsBecauseOfCollectionSize(int[] unsortedArray)
		{
			// Arrange.
			List<int> unsortedList = new(unsortedArray);

			// Act & Assert.
			_ = Assert.Throws<ArgumentException>(() => unsortedList.CubeSort());
		}

		/// <summary>
		/// Provides a series of test data for testing sorters with invalid collection sizes.
		/// Each item in the series represents a test case consisting of an unsorted array that should cause an exception.
		/// </summary>
		/// <returns>Series of test data.</returns>
		public static TheoryData<int[]> BadSortingData { get; } = new TheoryData<int[]>
		{
			new[] { 3, 2, 1 }, // Test with three elements.
			new[] { 3, 2, 1, 2, 6 }, // Test with five elements.
		};
	}
}