namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for bubble sort sorting operations.
	/// </summary>
	public static class BubbleSortExtensions
	{
		/// <summary>
		/// Sorts the elements in the collection using bubble sort algorithm.
		/// Bubble sort is a simple sorting algorithm that repeatedly steps through the collection, compares adjacent elements and swaps them if they are in the wrong order.
		/// The pass through the collection is repeated until the collection is sorted.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		public static IList<T> BubbleSort<T>(this IList<T> collection)
			where T : IComparable
		{
			// Cache the count here to improve performance.
			int collectionCount = collection.Count;

			// Ignore collections with a size of 1 or less.
			if (collectionCount < 2)
				return collection;

			// Set to true to ensure that the sorting process enters the while loop at least once. This is a
			// common practice in sorting algorithms that require at least one pass through the collection to
			// either perform necessary swaps or to verify that no swaps are needed because the collection is
			// already in sorted order.
			bool swapped = true;

			// A variable to keep track of how many times we've gone through the list without making any swaps,
			// which means the collection is already sorted.
			int noSwapsCount = 0;

			// Continue looping through the collection until no swaps are made.
			while (swapped)
			{
				// Reset swapped to false to check if there will be any swaps in this pass.
				swapped = false;

				// Iterate through the collection from the beginning to the 'end' (end is shrinking as the
				// largest elements "bubble up" to their correct positions).
				for (int passIndex = 1; passIndex < collectionCount - noSwapsCount; passIndex++)
				{
					// Compare the current element with the next element
					if (collection[passIndex - 1].CompareTo(collection[passIndex]) > 0)
					{
						// Swap the elements if they are in the wrong order using deconstruction.
						(collection[passIndex - 1], collection[passIndex]) = (collection[passIndex], collection[passIndex - 1]);
						swapped = true; // Mark that we've made a swap.
					}
				}
				// If no elements were swapped in this pass, increment the noSwapsCount.
				// This optimization helps to reduce the number of unnecessary comparisons.
				if (!swapped)
					noSwapsCount++;
			}

			return collection;
		}
	}
}
