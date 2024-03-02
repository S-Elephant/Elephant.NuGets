namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for bucket sort sorting operations.
	/// </summary>
	public static class BucketSortExtensions
	{
		/// <summary>
		/// Performs the Bucket Sort algorithm on a collection.
		/// Elements are expected to be normalized into a range [0, 1] for effective bucket sorting.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <param name="normalizer">A function that converts elements from T to float, expected to return values between 0 and 1. May not be null.</param>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		public static IList<T> BucketSort<T>(this IList<T> collection, Func<T, float> normalizer)
			where T : IComparable<T>
		{
			// Determine the number of buckets to use, which is typically the same as the number of elements for optimal distribution.
			int bucketCount = collection.Count;

			// Ignore collections with a size of 1 or less.
			if (bucketCount < 2)
				return collection;

			// Create an array where each element is a list that will serve as a bucket.
			List<T>[] buckets = new List<T>[bucketCount];

			// Initialize each bucket as an empty list to prepare them for element distribution.
			for (int i = 0; i < bucketCount; i++)
				buckets[i] = new List<T>();

			// Distribute elements into buckets based on their normalized value.
			foreach (T item in collection)
			{
				// Normalize the element value to a float between 0 and 1.
				float normValue = normalizer(item);
				// Calculate the index of the bucket the element belongs to.
				int bucketIndex = (int)(normValue * bucketCount);
				// Ensure the bucket index is within the valid range.
				bucketIndex = Math.Min(bucketIndex, bucketCount - 1);
				// Add the element to the appropriate bucket.
				buckets[bucketIndex].Add(item);
			}

			// Sort each bucket and concatenate all buckets back into the original list.
			int index = 0; // Index to track the position in the original list.
			foreach (List<T> bucket in buckets)
			{
				// Sort the bucket. The sorting method depends on the nature of T.
				// We can use the default List<T>.Sort() method since T implements IComparable<T>.
				bucket.Sort();

				// Place the sorted elements back into the original list.
				foreach (T item in bucket)
					collection[index++] = item;
			}

			return collection;
		}
	}
}
