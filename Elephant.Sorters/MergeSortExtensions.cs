using Elephant.Sorters.Utilities;

namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for merge sort sorting operations.
	/// </summary>
	public static class MergeSortExtensions
	{
		/// <summary>
		/// Sorts a collection of using the merge sort algorithm.
		/// </summary>
		/// <param name="collection">Collection to sort.</param>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		/// <remarks>
		/// Merge sort is a divide and conquer algorithm. It divides the input array into two halves,
		/// calls itself for the two halves, and then merges the two sorted halves.
		/// The merge function is used for merging two halves.
		/// The merge(arr, l, m, r) is a key process that assumes that arr[l..m] and arr[m+1..r]
		/// are sorted and merges the two sorted sub-arrays into one.
		/// </remarks>
		public static IList<T> MergeSort<T>(this IList<T> collection)
			where T : IComparable<T>
		{
			int collectionCount = collection.Count;

			// Ignore collections with a size of 1 or less.
			if (collectionCount < 2)
				return collection;

			MergeSortUtilities.MergeSortInternal(collection, 0, collectionCount - 1);

			return collection;
		}
	}
}
