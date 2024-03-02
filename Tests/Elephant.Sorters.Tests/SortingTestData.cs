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
		/// <returns>A series of test data.</returns>
		public static IEnumerable<object[]> ArraySortingData()
		{
			yield return new object[] { Array.Empty<int>(), Array.Empty<int>() }; // Test with an empty array.
			yield return new object[] { new[] { 1 }, new[] { 1 } }; // Test with a single element array.
			yield return new object[] { new[] { 2, 1 }, new[] { 1, 2 } }; // Test with two elements.
			yield return new object[] { new[] { 3, 2, 1 }, new[] { 1, 2, 3 } }; // Test with three elements.
			yield return new object[] { new[] { 5, 3, 8, 4, 2 }, new[] { 2, 3, 4, 5, 8 } }; // Test with multiple elements.
		}

		/// <summary>
		/// Provides a series of test data for testing sorters.
		/// Each item in the series represents a test case, consisting of an unsorted list and the list's expected state after sorting.
		/// </summary>
		/// <returns>A series of test data.</returns>
		public static IEnumerable<object[]> ListSortingData()
		{
			yield return new object[] { new List<int>(), new List<int>() }; // Test with an empty list.
			yield return new object[] { new List<int> { 1 }, new List<int> { 1 } }; // Test with a single element list.
			yield return new object[] { new List<int> { 2, 1 }, new List<int> { 1, 2 } }; // Test with two elements.
			yield return new object[] { new List<int> { 3, 2, 1 }, new List<int> { 1, 2, 3 } }; // Test with three elements.
			yield return new object[] { new List<int> { 5, 3, 8, 4, 2 }, new List<int> { 2, 3, 4, 5, 8 } }; // Test with multiple elements.
		}
	}
}
