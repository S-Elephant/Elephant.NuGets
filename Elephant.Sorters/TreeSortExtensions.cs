using Elephant.Sorters.Utilities;

namespace Elephant.Sorters
{
	/// <summary>
	/// Provides extension methods for tree sort sorting operations.
	/// </summary>
	public static class TreeSortExtensions
	{
		/// <summary>
		/// Sorts the elements in the collection using the Tree Sort algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection, must implement <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="collection">Collection to sort.</param>
		/// <returns>Modified <paramref name="collection"/>.</returns>
		public static IList<T> TreeSort<T>(this IList<T> collection)
			where T : IComparable<T>
		{
			// Ignore collections with a size of 1 or less.
			if (collection.Count < 2)
				return collection;

			TreeSortUtilities.BinarySearchTree<T> tree = new();

			foreach (T item in collection)
				tree.Insert(item);

			// Handling the case for arrays specifically (by overwriting existing items),
			// as they don't support Clear() and Add().
			// Yeah... A C# array inherits from IList and thus has a Clear() method
			// but you may never call it!
			if (collection is T[] array)
			{
				int index = 0;
				tree.InOrderTraversal(item => array[index++] = item);
			}
			else
			{
				collection.Clear();
				tree.InOrderTraversal(collection.Add);
			}

			return collection;
		}
	}
}
