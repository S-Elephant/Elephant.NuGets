namespace Elephant.Sorters.Utilities
{
	/// <summary>
	/// Quick sort utilities.
	/// </summary>
	internal static class QuickSortUtilities
	{
		/// <summary>
		/// Partitions the specified range of the collection, used as part of the QuickSort algorithm.
		/// </summary>
		/// <typeparam name="T">Type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <param name="left">Starting index of the range to partition. Use 0 for an empty collection.</param>
		/// <param name="right">Ending index of the range to partition. Use -1 for an empty collection.</param>
		/// <returns>Index of the pivot element after partitioning.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if the right index is outside of the collection.</exception>
		internal static int Partition<T>(IList<T> collection, int left, int right)
			where T : IComparable<T>
		{
			// Choose the last element as the pivot.
			T pivot = collection[right];

			// The lastSmallerElementIndex is used to track the boundary between elements smaller than the pivot and those
			// larger or equal to it. Initially, we assume that this boundary is just before the start of the segment to
			// be partitioned, hence left - 1. There are no elements known to be less than the pivot when we start.
			int lastSmallerElementIndex = left - 1;

			for (int currentElementIndex = left; currentElementIndex < right; currentElementIndex++)
			{
				// If current element is smaller than the pivot.
				if (collection[currentElementIndex].CompareTo(pivot) < 0)
				{
					lastSmallerElementIndex++;
					(collection[lastSmallerElementIndex], collection[currentElementIndex]) = (collection[currentElementIndex], collection[lastSmallerElementIndex]); // Swap the elements.
				}
			}

			// Swap the pivot element with the element at i + 1.
			(collection[lastSmallerElementIndex + 1], collection[right]) = (collection[right], collection[lastSmallerElementIndex + 1]);

			return lastSmallerElementIndex + 1;
		}
	}
}
