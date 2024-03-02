[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Sorters.svg)](https://www.nuget.org/packages/Elephant.Sorters/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Sorters.svg)](https://www.nuget.org/packages/Elephant.Sorters/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Sorters/LICENSE.txt)

# About

Implements various sorters. I made this mostly for legacy systems and educational purposes. I recommend using the build-in Sort() from C# which uses a combination of insertion sort, heapsort, quicksort as described [here](https://learn.microsoft.com/en-us/dotnet/api/system.array.sort?view=net-8.0) and [here](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=net-8.0).

# How to use

```c#
using Elephant.Sorters;

..
    
private void BucketSortExample()
{
    List<int> unsortedList = new() { 1, 5, 3, 2 };
    unsortedList.BubbleSort(); // Returns 1, 2, 3, 5
}
```

Other examples

```c#
// Bucket sort example.
int min = 0;
int max = 0;
if (unsortedList.Any())
{
    min = unsortedList.Min();
    max = unsortedList.Max();
}

unsortedList.BucketSort(BucketSortNormalizers.IntNormalizer(min, max));

// Counting sort example.
private static int IntKeySelector(int item)
{
	return item;
}
unsortedList.CountingSort(IntKeySelector);

// Other sorting examples.
unsortedList.CubeSort();
unsortedArray.HeapSort();
unsortedArray.InsertionSort();
unsortedArray.MergeSort();
unsortedArray.QuickSort();
unsortedArray.QuickSort(1, 3); // With left and right indices.
unsortedArray.RadixSort();
unsortedArray.SelectionSort();
unsortedArray.ShellSort();
unsortedArray.TreeSort();
```

# Performance differences

| Algorithm | Time Complexity<br />Best | Time Complexity<br />Average | Time Complexity<br />Worst | Space Complexity<br />Best, average and worst |
| :-------: | :-----------------------: | :--------------------------: | :------------------------: | :-------------------------------------------: |
|  Bubble   |           O(n)            |       O(n<sup>2</sup>)       |      O(n<sup>2</sup>)      |                     O(1)                      |
|  Bucket   |          O(n+k)           |            O(n+k)            |      O(n<sup>2</sup>)      |                     O(n)                      |
| Counting  |          O(n+k)           |            O(n+k)            |           O(n+k)           |                     O(k)                      |
|   Cube    |           O(n)            |          O(n log n)          |         O(n log n)         |                     O(n)                      |
|   Heap    |        O(n log n)         |          O(n log n)          |         O(n log n)         |                     O(1)                      |
| Insertion |           O(n)            |       O(n<sup>2</sup>)       |      O(n<sup>2</sup>)      |                     O(1)                      |
|   Merge   |        O(n log n)         |          O(n log n)          |         O(n log n)         |                     O(n)                      |
| Quicksort |        O(n log n)         |          O(n log n)          |      O(n<sup>2</sup>)      |                   O(log n)                    |
|   Radix   |           O(nk)           |            O(nk)             |           O(nk)            |                    O(n+k)                     |
| Selection |     O(n<sup>2</sup>)      |       O(n<sup>2</sup>)       |      O(n<sup>2</sup>)      |                     O(1)                      |
|   Shell   |        O(n log n)         |   Depends on gap sequence    |  O(n(log n)<sup>2</sup>)   |                     O(1)                      |
|    Tim    |           O(n)            |          O(n log n)          |         O(n log n)         |                     O(n)                      |
|   Tree    |        O(n log n)         |          O(n log n)          |      O(n<sup>2</sup>)      |                     O(n)                      |

| Complexity       | Performance     |
|------------------|-----------------|
| O(1)             | Good            |
| O(log n)         | Good            |
| O(n)             | Average         |
| O(n+k)           | Variable        |
| O(n log n)       | Good            |
| O(nk)            | Variable        |
| O(n<sup>2</sup>) | Bad             |
| O(2<sup>n</sup>) | Bad             |
| O(n!)            | Bad             |

# Algorithms explained

*Disclaimer: Most explanations here are generated using AI.*

## Bubble sort

1. **Compare Adjacent Elements:** Start at the beginning of the array and compare the first two elements. If the first element is greater than the second, swap them.
2. **Continue Comparisons:** Move to the next pair of adjacent elements, compare their values, and swap them if necessary. Continue this process for each pair of adjacent elements in the list.
3. **Repeat the Process:** Once you reach the end of the array, start again from the beginning. This time, you can ignore the last element, as it is already in its correct place.
4. **Optimization (Optional):** Keep track of whether any swaps were made during the previous pass. If no swaps were made, the list is already sorted, and you can stop the algorithm.
5. **Completion:** Repeat these steps until the entire array is sorted. In the worst-case scenario, this will require n-1 passes through the list (where n is the number of elements in the list), with each pass ensuring that the largest unsorted element "bubbles up" to its correct position.

## Bucket sort

1. **Create Buckets:** First, divide the range of possible values into a number of equally spaced intervals, or "buckets." For example, if you're sorting numbers between 0 and 100, you might create 10 buckets for the intervals 0-10, 11-20, etc.
2. **Distribute Elements:** Go through the original array and place each element into the bucket corresponding to its value. For instance, a value of 15 would go into the second bucket because it falls between 11 and 20.
3. **Sort Buckets:** Individually sort the elements within each bucket. This can be done using any sorting method, like insertion sort, as the expectation is that each bucket has fewer elements and hence sorting would be faster.
4. **Concatenate Buckets:** Finally, combine the contents of all the buckets into a single array. This array will be sorted if all individual buckets are sorted.

## Cube sort

Is a parallel sorting algorithm that builds a self-balancing multi-dimensional array from the keys to be sorted. As the axes are of similar length the structure resembles a cube. After each key is inserted the cube can be rapidly converted to an array. In my case, the collection size must be a power of 2.

## Heap sort

1. **Building the Heap:** This phase transforms the unsorted array into a max-heap, a complete binary tree where each node is greater than or equal to its children. The process starts from the middle of the array and works backward, ensuring each subtree maintains the max-heap property.
2. **Sorting the Array:** Once the max-heap is built, the algorithm repeatedly removes the largest element (the root of the heap), places it at the end of the array, and reduces the heap size. After removing the largest element, the heap properties are restored by adjusting the new root's position as necessary. This process continues until the heap size is reduced to one, leaving all elements sorted.

## Insertion sort

1. **Starting from the second element**: The array is imagined as divided into a sorted and an unsorted part. Initially, the sorted part consists of the first element only.
2. **Key Element**: In each iteration, the algorithm considers a new element from the unsorted portion, calling this the 'key'. It compares the key with the elements in the sorted part, moving each element one step to the right whenever the key is smaller than an element in the sorted section.
3. **Insertion**: Once the algorithm finds the correct position (all elements to the left are smaller, and all to the right are larger), it inserts the key into this position.
4. **Repetition**: This process repeats for each element in the array until the array is entirely sorted.

## Merge sort

1. **Divide**: Split the list into two halves until each sublist contains only one element. A list with one element is considered sorted.
2. **Conquer**: Recursively sort both halves of the list.
3. **Combine**: Merge the two halves back together in a sorted manner.

## Quick sort

1. **Choosing a Pivot**: The pivot can be chosen in various ways, either as the first, last, or a random element of the array, or by more sophisticated methods like the median of three. The choice of pivot affects the efficiency of the sorting.
2. **Partitioning**: Rearrange the array so that all elements less than the pivot come before it and all elements greater come after it. After partitioning, the pivot is in its final position.
3. **Recursively Sort Subarrays**: Apply Quicksort recursively to the subarrays formed by partitioning, excluding the pivot since it's already in its correct position.

## Radix sort

1. **Choose Significant Digit**: Decide whether to start sorting by the least significant digit (LSD) or the most significant digit (MSD).
2. **Create Buckets**: Make 10 buckets for each digit (0-9).
3. **Distribute Elements**: Place each number in the corresponding bucket based on the current digit being considered.
4. **Collect Elements**: Collect the numbers from the buckets, preserving the order within each bucket.
5. **Repeat**: Repeat steps 3-4 for each digit, moving to the next significant digit each time, until all digits have been considered.

## Selection sort

1. **Find Minimum**: Locate the smallest element in the array.
2. **Swap Elements**: Exchange this smallest element with the first element.
3. **Advance Starting Position**: Move the starting position one index forward.
4. **Repeat**: Continue the process for each position in the array until the entire array is sorted.

## Shell sort

1. **Initial Gap Selection**: Choose an initial gap, typically the length of the array divided by 2.
2. **Gap Reduction**: For each gap, sort the elements using a gap-based insertion sort (elements are moved only if they are in the wrong order).
3. **Shrinking the Gap**: Reduce the gap size, usually by dividing it by 2, and repeat the sorting process.
4. **Final Stage**: Continue reducing the gap until it becomes 1; at this point, the algorithm performs a standard insertion sort and finalizes the array's order.

## Tree sort

1. **Build Tree**: Create a Binary Search Tree (BST) by inserting elements from the array.
2. **In-order Traversal**: Recursively perform an in-order traversal of the BST.
3. **Create Sorted Array**: During traversal, collect and arrange elements in sequence, resulting in a sorted array.
