namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for selection sort sorting operations.
	/// </summary>
	public static class SelectionSortExtensions
	{
		/// <summary>
		/// Sorts the elements in the collection using the selection sort algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		/// <remarks>
		/// Selection sort is not suitable for large collections as it has O(n^2) time complexity.
		/// However, it is a simple and straightforward sorting algorithm.
		/// </remarks>
		public static IList<T> SelectionSort<T>(this IList<T> collection)
			where T : IComparable<T>
		{
			int collectionCount = collection.Count;

			// Ignore collections with a size of 1 or less.
			if (collectionCount < 2)
				return collection;

			// Loop through each item in the list, except the last one (no need to sort the last item in selection sort).
			for (int currentIndex = 0; currentIndex < collectionCount - 1; currentIndex++)
			{
				// Assume the first unsorted element is the smallest.
				int minIndex = currentIndex;

				// Test against items after i to find the smallest.
				for (int searchIndex = currentIndex + 1; searchIndex < collectionCount; searchIndex++)
				{
					// Compare the items.
					if (collection[searchIndex].CompareTo(collection[minIndex]) < 0)
					{
						// Found new minimum; remember its index.
						minIndex = searchIndex;
					}
				}

				// If minIndex is not the position of the current element, swap them.
				if (minIndex != currentIndex)
				{
					// Swap elements at positions i and minIndex using deconstruction.
					(collection[currentIndex], collection[minIndex]) = (collection[minIndex], collection[currentIndex]);
				}
			}

			return collection;
		}
	}
}
