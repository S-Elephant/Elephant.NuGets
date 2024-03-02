using Elephant.Sorters.Utilities;

namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for radix sort sorting operations.
	/// </summary>
	public static class RadixSortExtensions
	{
		/// <summary>
		/// Sorts the elements in the collection using the radix sort algorithm.
		/// </summary>
		/// <param name="collection">Collection to sort.</param>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		/// <remarks>
		/// Radix sort is a non-comparative integer sorting algorithm that sorts data with integer keys by grouping keys by the individual digits which share the same significant position and value.
		/// This implementation works for integers. If you're working with other types, you might need to adapt the algorithm accordingly.
		/// This method modifies the original collection.
		/// </remarks>
		public static IList<T> RadixSort<T>(this IList<T> collection)
			where T : IComparable<T>
		{
			int collectionCount = collection.Count;

			// Ignore collections with a size of 1 or less.
			if (collectionCount < 2)
				return collection;

			// Convert all items in the collection to a sortable integer form.
			// This requires the type to be convertible to long for meaningful radix sort application.
			// In practical scenarios, this step should be adapted to the nature of T.
			List<long> intList = collection.Select(item => Convert.ToInt64(item)).ToList();

			// Find the maximum number to know the number of digits.
			long max = intList.Max();

			// Perform counting sort for every digit. Note that instead of passing digit number,
			// exp (10^i) is passed. exp is 10^i where i is current digit number.
			for (long exp = 1; max / exp > 0; exp *= 10)
				RadixSortUtilities.CountingSort(intList, exp);

			// Convert back to the original collection.
			for (int i = 0; i < collectionCount; i++)
			{
				// This step requires the type to have a constructor that takes a long,
				// which may not be true for all types. This would need to be customized based on T.
				collection[i] = (T)Convert.ChangeType(intList[i], typeof(T));
			}

			return collection;
		}
	}
}
