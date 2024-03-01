namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for insertion sort sorting operations.
	/// </summary>
	public static class InsertionSortExtensions
	{
		/// <summary>
		/// Sorts the specified collection using the insertion sort algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <remarks>
		/// This method extends any <![CDATA[IList<T>]]> with the ability to sort itself using the insertion sort algorithm.
		/// This is a generic method, making it applicable to lists of any type that implements <see cref="IComparable{T}"/>,
		/// ensuring elements can be compared to each other.
		/// </remarks>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		public static IList<T> InsertionSort<T>(this IList<T> collection)
			where T : IComparable<T>
		{
			int collectionCount = collection.Count;

			// Ignore collections with a size of 1 or less.
			if (collectionCount < 2)
				return collection;

			// Iterate over all elements in the collection, starting from the first element after the
			// initial one, since the first element is trivially considered sorted.
			for (int currentIndex = 1; currentIndex < collectionCount; currentIndex++)
			{
				// Store the current element to be positioned.
				T currentItem = collection[currentIndex];
				int positionToInsert = currentIndex - 1;

				// Shift elements that are greater than the current element to the right by one position.
				// This loop continues as long as we haven't reached the beginning of the collection
				// and the element to the left of the current position is greater than the current element.
				while (positionToInsert >= 0 && collection[positionToInsert].CompareTo(currentItem) > 0)
				{
					collection[positionToInsert + 1] = collection[positionToInsert];
					positionToInsert--;
				}

				// Insert the current element into its correct position.
				// This is one position forward from where we stopped in the collection (as
				// positionToInsert has been decremented one too far).
				collection[positionToInsert + 1] = currentItem;
			}

			return collection;
		}
	}
}
