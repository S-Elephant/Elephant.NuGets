using Elephant.Sorters.Utilities;

namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for heap sort sorting operations.
	/// </summary>
	public static class HeapSortExtensions
	{
		/// <summary>
		/// Performs the HeapSort algorithm on a collection.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		public static IList<T> HeapSort<T>(this IList<T> collection)
			where T : IComparable<T>
		{
			int count = collection.Count;

			// Ignore collections with a size of 1 or less.
			if (count < 2)
				return collection;

			// Build the heap by rearranging the array.
			// Starting from the first non-leaf node all the way to the root node.
			for (int heapStartIndex = (count / 2) - 1; heapStartIndex >= 0; heapStartIndex--)
				HeapSortUtilities.Heapify(collection, count, heapStartIndex);

			// One by one, extract elements from the heap.
			for (int heapEndIndex = count - 1; heapEndIndex >= 0; heapEndIndex--)
			{
				// Swap the current root (largest value) with the last item of the heap
				// using deconstruction.
				(collection[0], collection[heapEndIndex]) = (collection[heapEndIndex], collection[0]);

				// Apply heapify to the reduced heap, thus ensuring the remaining
				// elements form a valid heap before the next extraction.
				HeapSortUtilities.Heapify(collection, heapEndIndex, 0);
			}

			return collection;
		}
	}
}
