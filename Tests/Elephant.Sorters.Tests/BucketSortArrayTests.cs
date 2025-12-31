using Elephant.Sorters.Utilities;

namespace Elephant.Sorters.Tests
{
	/// <summary>
	/// <see cref="BucketSortExtensions"/> array tests.
	/// </summary>
	public class BucketSortArrayTests
	{
		/// <summary>
		/// Tests the BucketSort extension method to ensure it sorts an array correctly.
		/// Each set of data is used to verify that the BucketSort method correctly sorts the input array.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(SortingTestData.ArraySortingData), MemberType = typeof(SortingTestData))]
		public void SortsCorrectly(int[] unsortedArray, int[] expectedSortedArray)
		{
			// Arrange.
			int min = 0;
			int max = 0;
			if (unsortedArray.Length > 0)
			{
				min = unsortedArray.Min();
				max = unsortedArray.Max();
			}

			// Act.
			_ = unsortedArray.BucketSort(BucketSortNormalizers.IntNormalizer(min, max));

			// Assert.
			Assert.Equal(expectedSortedArray, unsortedArray);
		}
	}
}