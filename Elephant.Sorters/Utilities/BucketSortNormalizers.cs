namespace Elephant.Sorters.Utilities
{
	/// <summary>
	/// Contains a few normalizers for the bucket sort algorithm.
	/// </summary>
	public static class BucketSortNormalizers
	{
		/// <summary>
		/// Creates and returns a normalization function for integers based on a specified range.
		/// This function is intended for use with the BucketSort algorithm to normalize integer values
		/// to a range between 0 and 1. The normalization is based on the minimum and maximum values
		/// provided, which should represent the range of the data set being sorted.
		/// </summary>
		/// <param name="min">Minimum value in the range of data. This value will be normalized to 0.</param>
		/// <param name="max">Maximum value in the range of data. This value will be normalized to 1.</param>
		/// <returns>Function that takes an integer and returns a float between 0 and 1 representing
		/// the normalized value within the specified range. This function can be passed to the BucketSort
		/// method as the normalizer parameter.</returns>
		public static Func<int, float> IntNormalizer(int min, int max)
		{
			return (value) => NormalizeInt(value, min, max);
		}

		/// <summary>
		/// Normalizes an integer value to a float between 0 and 1 based on the known range of the data.
		/// </summary>
		/// <param name="value">Integer value to normalize.</param>
		/// <param name="min">Minimum integer value in the range.</param>
		/// <param name="max">Maximum integer value in the range.</param>
		/// <returns>Float between 0 and 1 representing the normalized value.</returns>
		private static float NormalizeInt(int value, int min, int max)
		{
			// Avoid division by zero if all values are the same.
			if (max == min)
				return 0; // You may also return 0.5f instead to place all elements in the middle bucket.

			return (float)(value - min) / (max - min);
		}
	}
}
