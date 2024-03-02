namespace Elephant.Sorters.Utilities
{
	/// <summary>
	/// Merge sort utilities.
	/// </summary>
	internal static class MergeSortUtilities
	{
		/// <summary>
		/// Performs the actual recursive merge sort on a portion of the collection.
		/// </summary>
		/// <param name="collection">Collection to sort.</param>
		/// <param name="left">Starting index of the collection segment to sort.</param>
		/// <param name="right">Ending index of the collection segment to sort.</param>
		/// <remarks>
		/// This method is called recursively for each half of the collection to be sorted,
		/// and it calls the Merge method to combine the sorted halves.
		/// </remarks>
		internal static void MergeSortInternal<T>(IList<T> collection, int left, int right)
			where T : IComparable<T>
		{
			// Check if the current segment of the collection has more than one element.
			// If left is not less than right, the segment has zero or one element and is therefore already sorted.
			if (left < right)
			{
				// Calculate the middle point to divide the collection into two halves.
				// This avoids integer overflow which can occur when calculating the midpoint as (left + right) / 2.
				// This is the same as (left + right) / 2, but avoids overflow for large left and right.
				int middle = left + (right - left) / 2;

				// Recursively sort the first half of the collection.
				// This call continues to split and sort the left half of the collection segment until it is broken down
				// to individual elements.
				MergeSortInternal(collection, left, middle);

				// Recursively sort the second half of the collection.
				// Similar to the first half, this continues to split and sort the right half of the collection segment
				// until it consists of single elements.
				MergeSortInternal(collection, middle + 1, right);

				// Merge the two sorted halves.
				// After the two halves are individually sorted, this function merges them into a single sorted
				// sequence. The original collection segment from 'left' to 'right' will now contain the sorted elements.
				Merge(collection, left, middle, right);
			}
		}

		/// <summary>
		/// Merges two sorted sub-lists into a single sorted collection.
		/// </summary>
		/// <param name="collection">The original list containing the sub-lists to be merged.</param>
		/// <param name="left">The starting index of the first sub-list.</param>
		/// <param name="middle">The ending index of the first sub-list and one less than the starting index of the second sub-list.</param>
		/// <param name="right">The ending index of the second sub-list.</param>
		/// <remarks>
		/// This method is used by the MergeSortInternal method after it has recursively sorted the sub-lists.
		/// It combines two adjacent sorted sub-lists into a single sorted collection.
		/// </remarks>
		internal static void Merge<T>(IList<T> collection, int left, int middle, int right)
			where T : IComparable<T>
		{
			// Calculate the number of elements in each temporary list.
			int leftListSize = middle - left + 1;
			int rightListSize = right - middle;

			// Create temporary lists to hold the values that will be merged.
			List<T> tempLeft = new(new T[leftListSize]);
			List<T> tempRight = new(new T[rightListSize]);

			// Copy the data from the main list to the temporary lists.
			// tempLeft[] holds elements from list[left..middle]
			// tempRight[] holds elements from list[middle+1..right]
			for (int i = 0; i < leftListSize; i++)
				tempLeft[i] = collection[left + i];
			for (int i = 0; i < rightListSize; i++)
				tempRight[i] = collection[middle + 1 + i];

			// Initial indexes for merging:
			// leftIndex for traversing tempLeft[], rightIndex for traversing tempRight[], mergedIndex for the main list.
			int leftIndex = 0, rightIndex = 0;
			int mergedIndex = left;

			// Merge the temp list back into the original list[left..right]
			// by comparing the elements of tempLeft and tempRight.
			while (leftIndex < leftListSize && rightIndex < rightListSize)
			{
				// If current element of tempLeft[] is less than or equal to current element of tempRight[],
				// copy it to the main list.
				if (tempLeft[leftIndex].CompareTo(tempRight[rightIndex]) <= 0)
				{
					collection[mergedIndex] = tempLeft[leftIndex];
					leftIndex++;
				}
				else
				{
					collection[mergedIndex] = tempRight[rightIndex];
					rightIndex++;
				}
				mergedIndex++;
			}

			// Copy the remaining elements of tempLeft[], if there are any.
			// This happens if tempLeft[] has elements left after tempRight[] is exhausted.
			while (leftIndex < leftListSize)
			{
				collection[mergedIndex] = tempLeft[leftIndex];
				leftIndex++;
				mergedIndex++;
			}

			// Similarly, copy the remaining elements of tempRight[], if there are any.
			while (rightIndex < rightListSize)
			{
				collection[mergedIndex] = tempRight[rightIndex];
				rightIndex++;
				mergedIndex++;
			}
		}
	}
}
