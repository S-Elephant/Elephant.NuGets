namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for shell sort sorting operations.
	/// </summary>
	public static class ShellSortExtensions
	{
		/// <summary>
		/// Sorts the elements in the collection using the shell sort algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		/// <remarks>
		/// Shell Sort is an in-place comparison sort. It is mainly a variation of sorting by exchange (bubble sort)
		/// or sorting by insertion (insertion sort). The method starts by sorting pairs of elements far apart from each other,
		/// then progressively reducing the gap between elements to be compared.
		/// </remarks>
		public static IList<T> ShellSort<T>(this IList<T> collection)
			where T : IComparable<T>
		{
			int collectionCount = collection.Count;

			// Ignore collections with a size of 1 or less.
			if (collectionCount < 2)
				return collection;

			// Start with a big gap, then reduce the gap.
			for (int gap = collectionCount / 2; gap > 0; gap /= 2)
			{
				// Do a gapped insertion sort for this gap size.
				// The first gap elements list[0..gap-1] are already in gapped order
				// keep adding one more element until the entire list is gap sorted
				for (int currentIndex = gap; currentIndex < collectionCount; currentIndex += 1)
				{
					// Add list[i] to the elements that have been gap sorted
					// Save list[i] in temp and make a hole at position i
					T temp = collection[currentIndex];

					// Iterate backwards through the list, starting from the current index, comparing each element with the element 'gap' positions behind it.
					// This loop continues as long as there are elements 'gap' positions behind the current one (ensured by 'sortedIndex >= gap')
					// and the element 'gap' positions behind is greater than the temporary element (ensured by 'list[sortedIndex - gap].CompareTo(temp) > 0').
					// In each iteration, the loop decrements 'sortedIndex' by 'gap', effectively moving the higher value elements forward by 'gap' positions
					// to make space for the 'temp' element to be inserted into its correct sorted position in the gapped sequence.
					int sortedIndex;
					for (sortedIndex = currentIndex; sortedIndex >= gap && collection[sortedIndex - gap].CompareTo(temp) > 0; sortedIndex -= gap)
						collection[sortedIndex] = collection[sortedIndex - gap];

					// Put temp (the original list[i]) in its correct location
					collection[sortedIndex] = temp;
				}
			}

			return collection;
		}
	}
}
