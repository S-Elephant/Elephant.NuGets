namespace Elephant.Sorters.Utilities
{
	/// <summary>
	/// Radix sort utilities.
	/// </summary>
	internal static class RadixSortUtilities
	{
		/// <summary>
		/// A function to do counting sort of collection according to the digit represented by exp.
		/// </summary>
		/// <param name="collection">Collection to sort.</param>
		/// <param name="exp">The exponent representing the digit's place.</param>
		internal static void CountingSort(IList<long> collection, long exp)
		{
			int collectionCount = collection.Count;

			// Output array where sorted elements will be placed.
			long[] output = new long[collectionCount];

			// Array to store the count of each digit (0-9).
			int[] count = new int[10];

			// Initialize count array as 0 for all possible digits from 0 to 9.
			for (int i = 0; i < 10; i++)
				count[i] = 0;

			// Store count of occurrences of each digit in the corresponding index.
			// The digit is determined by the expression (list[i] / exp) % 10.
			// 'exp' is the current exponent value indicating the digit position being sorted.
			for (int i = 0; i < collectionCount; i++)
				count[(collection[i] / exp) % 10]++;

			// Modify the count array by adding the count of the current digit with the count of the previous digit.
			// This modification will convert the count array into a prefix sum array.
			// After this step, count[i] contains the number of elements up to digit i.
			for (int i = 1; i < 10; i++)
				count[i] += count[i - 1];

			// Build the output array. Traverse the original array from end to start (to maintain stability).
			// Calculate the position of each element in the output array based on the count array and place it accordingly.
			// Then, decrease the count by one to update the position for the next element with the same digit.
			for (int i = collectionCount - 1; i >= 0; i--)
			{
				output[count[(collection[i] / exp) % 10] - 1] = collection[i];
				count[(collection[i] / exp) % 10]--;
			}

			// Copy the sorted elements from the output array back to the original collection.
			// After this step, the collection contains the elements sorted according to the current digit.
			for (int i = 0; i < collectionCount; i++)
				collection[i] = output[i];
		}
	}
}
