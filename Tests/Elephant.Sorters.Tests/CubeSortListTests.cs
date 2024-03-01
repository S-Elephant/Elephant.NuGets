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
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Act.
			unsortedList.CubeSort();

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}

		/// <summary>
		/// Provides a series of test data for testing sorters.
		/// Each item in the series represents a test case, consisting of an unsorted list and the list's expected state after sorting.
		/// </summary>
		/// <returns>A series of test data.</returns>
		public static IEnumerable<object[]> CorrectListSortingData()
		{
			yield return new object[] { new List<int>(), new List<int>() }; // Test with an empty list.
			yield return new object[] { new List<int> { 1 }, new List<int> { 1 } }; // Test with a single element list.
			yield return new object[] { new List<int> { 2, 1 }, new List<int> { 1, 2 } }; // Test with two elements.
			yield return new object[] { new List<int> { 3, 2, 1, 5 }, new List<int> { 1, 2, 3, 5 } }; // Test with three elements.
			yield return new object[] { new List<int> { 5, 3, 8, 4, 2, 5, 4, 1 }, new List<int> { 1, 2, 3, 4, 4, 5, 5, 8 } }; // Test with multiple elements.
		}

		/// <summary>
		/// Tests the CubeSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the CubeSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(BadSortingData))]
		public void ThrowsBecauseOfCollectionSize(List<int> unsortedList)
		{
			// Arrange.
			Action sortAction = () => unsortedList.CubeSort();

			// Act & Assert.
			Assert.Throws<ArgumentException>(sortAction);
		}

		/// <summary>
		/// Provides a series of test data for testing sorters.
		/// Each item in the series represents a test case, consisting of an unsorted array and the array tis expected state after sorting.
		/// </summary>
		/// <returns>A series of test data.</returns>
		public static IEnumerable<object[]> BadSortingData()
		{
			yield return new object[] { new List<int> { 3, 2, 1 } };
			yield return new object[] { new List<int> { 3, 2, 1, 2, 6 } };
		}
	}
}