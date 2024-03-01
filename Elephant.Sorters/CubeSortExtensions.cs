using Elephant.Sorters.Utilities;

namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for bucket sort sorting operations.
	/// </summary>
	public static class CubeSortExtensions
	{
		/// <summary>
		/// Performs a cube sort on a collection.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">The to sort. Collection size must be a power of 2.</param>
		/// <exception cref="ArgumentException"><paramref name="collection"/> size must be a power of 2 (empty collection is allowed).</exception>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		public static IList<T> CubeSort<T>(this IList<T> collection)
			where T : IComparable
		{
			int collectionCount = collection.Count;

			// Ignore collections with 1 or less elements.
			if (collectionCount < 2)
				return collection;

			if (!IsCountPowerOfTwo(collectionCount))
				throw new ArgumentException("Collection size must be power of 2.");

			// Start with the smallest size sequence(2), and double it every iteration.
			// This loop sets the size of the sequences to be merged into bitonic sequences.
			for (int sequenceSize = 2; sequenceSize <= collectionCount; sequenceSize <<= 1)
			{
				// Divide the collection into k/2 bitonic sequences.
				for (int distanceBetweenElements = sequenceSize >> 1; distanceBetweenElements > 0; distanceBetweenElements >>= 1)
				{
					// Loop over all elements in the collection.
					for (int firstElementIndex = 0; firstElementIndex < collectionCount; firstElementIndex++)
					{
						// Calculate index of the element to compare with.
						int ixj = firstElementIndex ^ distanceBetweenElements; // XOR to find the element to compare with.

						// Check if the other index is greater to ensure each pair is unique.
						if (ixj > firstElementIndex)
						{
							// Determine if we're sorting in ascending or descending order.
							// This depends on which part of the bitonic sequence the element is in.
							bool up = ((firstElementIndex & sequenceSize) == 0);

							// Perform the compare-and-swap operation.
							// If 'up' is true, sort ascending; if false, sort descending.
							CubeSortUtilities.CompareAndSwap(collection, firstElementIndex, ixj, up);
						}
					}
				}
			}

			return collection;
		}

		private static bool IsCountPowerOfTwo(int collectionCount)
		{
			// Check if the count is not zero and the count ANDed with one less than the count equals zero.
			return collectionCount != 0 && (collectionCount & (collectionCount - 1)) == 0;
		}
	}
}
