using Elephant.Sorters.Utilities;

namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="BucketSortExtensions"/> list tests.
	/// </summary>
	public class BucketSortListTests
	{
		/// <summary>
		/// Tests the BucketSort extension method to ensure it sorts a list correctly.
		/// Each set of data is used to verify that the BucketSort method correctly sorts the input list.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ListSortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(List<int> unsortedList, List<int> expectedSortedList)
		{
			// Arrange.
			int min = 0;
			int max = 0;
			if (unsortedList.Count > 0)
			{
				min = unsortedList.Min();
				max = unsortedList.Max();
			}

			// Act.
			_ = unsortedList.BucketSort(BucketSortNormalizers.IntNormalizer(min, max));

			// Assert.
			Assert.Equal(expectedSortedList, unsortedList);
		}
	}
}