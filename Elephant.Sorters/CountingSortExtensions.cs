namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for counting sort sorting operations.
	/// </summary>
	public static class CountingSortExtensions
	{
		/// <summary>
		/// Performs a Counting Sort on a collection.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <param name="keySelector">A function that converts elements of the collection to an integer. This integer will be used for sorting.</param>
		/// <remarks>
		/// Counting Sort is a non-comparative sorting algorithm suitable for sorting elements within a specific range.
		/// It operates by counting the number of objects that have a distinct key value, then doing some arithmetic to calculate the position of each object in the output sequence.
		/// </remarks>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		public static IList<T> CountingSort<T>(this IList<T> collection, Func<T, int> keySelector)
		{
			// Ignore collections with a size of 1 or less.
			if (collection.Count < 2)
				return collection;

			// Find the range of the data to determine the size of the counting array.
			// Initialize 'min' and 'max' with the key of the first element to start comparisons.
			int min = keySelector(collection[0]);
			int max = min;
			foreach (T item in collection)
			{
				// Convert the item to its key value.
				int key = keySelector(item);

				if (key < min)
				{
					// Update 'min' if a smaller key is found.
					min = key;
				}
				else if (key > max)
				{
					// Update 'max' if a larger key is found.
					max = key;
				}
			}

			// Initialize the count array with a size based on the 'min' and 'max' values found.
			// This array is used to count the occurrences of each key in the collection.
			int[] count = new int[max - min + 1];

			// Count each element by incrementing the count at the index corresponding to the element's key.
			foreach (T item in collection)
			{
				// Subtract 'min' to normalize keys to start from 0.
				count[keySelector(item) - min]++;
			}

			// Sum up counts to convert the count array into a prefix sum array.
			// After this, count[i] represents the number of elements <= i (after normalization).
			for (int i = 1; i < count.Length; i++)
				count[i] += count[i - 1];

			// Place the elements in the sorted order into a new array 'sorted'.
			// Iterate backwards through the original collection to maintain stability (original order of equal elements).
			T[] sorted = new T[collection.Count];
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				T item = collection[i];
				int normalizedKey = keySelector(item) - min; // Normalize the key.
				sorted[count[normalizedKey] - 1] = item; // Place item in its sorted position in the 'sorted' array.
				count[normalizedKey]--; // Decrement the count for the next item with the same key.
			}

			// Copy the sorted items back into the original collection to complete the sort.
			for (int i = 0; i < collection.Count; i++)
			{
				// Replace the original item with the sorted item.
				collection[i] = sorted[i];
			}

			return collection;
		}
	}
}
