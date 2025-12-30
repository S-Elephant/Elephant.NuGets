namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// Provides a series of test data for testing array and list sorters.
	/// </summary>
	public static class SortingTestData
	{
		/// <summary>
		/// Provides a series of test data for testing sorters.
		/// Each item in the series represents a test case, consisting of an unsorted array and the array tis expected state after sorting.
		/// </summary>
		/// <returns>Series of test data.</returns>
		public static TheoryData<int[], int[]> ArraySortingData => new()
		{
			{ new int[] { 3, 2, 1 }, new int[] { 1, 2, 3 } },
			{ new int[] { 5, 4, 6, 2 }, new int[] { 2, 4, 5, 6 } },
			{ new int[] { 1 }, new int[] { 1 } },
			{ Array.Empty<int>(), Array.Empty<int>() },
			{ new int[] { 2, 2, 1 }, new int[] { 1, 2, 2 } },
		};

		/// <summary>
		/// Provides a series of test data for testing sorters.
		/// Each item in the series represents a test case, consisting of an unsorted list and the list's expected state after sorting.
		/// </summary>
		/// <returns>Series of test data.</returns>
		public static TheoryData<List<int>, List<int>> ListSortingData => new()
		{
			{ new List<int> { 3, 2, 1 }, new List<int> { 1, 2, 3 } },
			{ new List<int> { 5, 4, 6, 2 }, new List<int> { 2, 4, 5, 6 } },
			{ new List<int> { 1 }, new List<int> { 1 } },
			{ new List<int> { }, new List<int> { } },
			{ new List<int> { 2, 2, 1 }, new List<int> { 1, 2, 2 } },
		};
	}
}
