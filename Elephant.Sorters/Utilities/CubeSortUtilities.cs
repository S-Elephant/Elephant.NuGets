namespace Elephant.Sorters.Utilities
{
	/// <summary>
	/// Cube sort utilities.
	/// </summary>
	internal class CubeSortUtilities
	{
		/// <summary>
		/// Compares two elements and swaps them if necessary to ensure the collection is sorted.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <param name="firstElementIndex">Index of the first element.</param>
		/// <param name="secondElementIndex">Index of the second element.</param>
		/// <param name="isDirectionUp">Direction of sorting, true for ascending, false for descending.</param>
		internal static void CompareAndSwap<T>(IList<T> collection, int firstElementIndex, int secondElementIndex, bool isDirectionUp)
			where T : IComparable
		{
			if ((collection[firstElementIndex].CompareTo(collection[secondElementIndex]) > 0 && isDirectionUp) || (collection[firstElementIndex].CompareTo(collection[secondElementIndex]) < 0 && !isDirectionUp))
			{
				// Swap using deconstruction.
				(collection[firstElementIndex], collection[secondElementIndex]) = (collection[secondElementIndex], collection[firstElementIndex]);
			}
		}
	}
}
