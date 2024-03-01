namespace Elephant.Sorters.Utilities
{
	/// <summary>
	/// Heap sort utilities.
	/// </summary>
	internal static class HeapSortUtilities
	{
		/// <summary>
		/// To heapify a subtree rooted with node i which is an index in collection[]. n is size of heap.
		/// </summary>
		/// <typeparam name="T">Type of elements in the collection.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <param name="n">Number of elements in the heap.</param>
		/// <param name="i">Index of the root element of the heap.</param>
		internal static void Heapify<T>(IList<T> collection, int n, int i)
			where T : IComparable<T>
		{
			// Initialize largest as root of the heap or sub-heap being considered
			int largest = i;

			// Calculate the index of the left child of the node at index i.
			int left = 2 * i + 1; // left = 2*i + 1

			// Calculate the index of the right child of the node at index i.
			int right = 2 * i + 2; // right = 2*i + 2

			// If left child exists and is larger than the current node (root of this sub-heap),
			// update 'largest' to be the index of the left child.
			if (left < n && collection[left].CompareTo(collection[largest]) > 0)
				largest = left;

			// If right child exists and is larger than the current largest (which could be the
			// root or the left child), update 'largest' to be the index of the right child.
			if (right < n && collection[right].CompareTo(collection[largest]) > 0)
				largest = right;

			// If largest is not the root (i.e., if either child was larger than the root),
			// swap the root with the largest child to maintain heap property.
			if (largest != i)
			{
				// Swap using deconstruction.
				(collection[i], collection[largest]) = (collection[largest], collection[i]);

				// Recursively apply Heapify to the subtree affected by the swap to ensure it's
				// a valid max heap. This is necessary as the original root (now a child) might
				// be smaller than its new children.
				Heapify(collection, n, largest);
			}
		}
	}
}
