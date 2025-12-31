using Elephant.Sorters.Utilities;

namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for quick sort sorting operations.
	/// </summary>
	public static class QuickSortExtensions
	{
		/// <summary>
		/// Sorts the elements in the collection using the quick sort algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		/// <remarks>
		/// Time complexities:<br/>
		/// <para>Best case: O(n log n). This occurs when the pivot divides the collection into two nearly equal halves, leading to a balanced partitioning at each level of recursion.</para>
		/// <para>Average case: O(n log n). Despite the variability in pivot choice, on average, the pivot tends to split the array into parts that are about the ratio of 1:9 or better, which results in logarithmic depth of the recursion.</para>
		/// <para>Worst case: O(n²). This happens when the pivot happens to be the smallest or the largest element of the array at each step of the partitioning process, leading to very unbalanced partitions. For example, this can occur if the array is already sorted (either in ascending or descending order) and the pivot is always chosen as the first or last element of the array.</para>
		/// </remarks>
		public static IList<T> QuickSort<T>(this IList<T> collection)
			where T : IComparable<T>
		{
			// Calls the QuickSort method on the entire collection.
			_ = QuickSort(collection, 0, collection.Count - 1);

			return collection;
		}

		/// <summary>
		/// Sorts the elements in a range of a <see cref="List{T}"/> using the QuickSort algorithm.
		/// </summary>
		/// <typeparam name="T">Type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <param name="leftIndex">Starting index of the range to sort. If this value is smaller than 0 it will be considered as 0. Use 0 for an empty collection.</param>
		/// <param name="rightIndex">Ending index of the range to sort. Use -1 for an empty collection.</param>
		/// <returns>Modified <paramref name="collection"/></returns>
		/// <remarks>
		/// Time complexities:<br/>
		/// <para>Best case: O(n log n). This occurs when the pivot divides the collection into two nearly equal halves, leading to a balanced partitioning at each level of recursion.</para>
		/// <para>Average case: O(n log n). Despite the variability in pivot choice, on average, the pivot tends to split the array into parts that are about the ratio of 1:9 or better, which results in logarithmic depth of the recursion.</para>
		/// <para>Worst case: O(n²). This happens when the pivot happens to be the smallest or the largest element of the array at each step of the partitioning process, leading to very unbalanced partitions. For example, this can occur if the array is already sorted (either in ascending or descending order) and the pivot is always chosen as the first or last element of the array.</para>
		/// </remarks>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if the right index is outside of the collection.</exception>
		public static IList<T> QuickSort<T>(this IList<T> collection, int leftIndex, int rightIndex)
			where T : IComparable<T>
		{
			// Ignore collections with a size of 1 or less.
			if (collection.Count < 2)
				return collection;

			if (leftIndex < 0)
				leftIndex = 0;

			if (leftIndex < rightIndex)
			{
				int pivot = QuickSortUtilities.Partition(collection, leftIndex, rightIndex);

				_ = collection.QuickSort(leftIndex, pivot - 1);
				_ = collection.QuickSort(pivot + 1, rightIndex);
			}

			return collection;
		}
	}
}
