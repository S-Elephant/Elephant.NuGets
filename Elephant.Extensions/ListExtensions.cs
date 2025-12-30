namespace Elephant.Extensions
{
	/// <summary>
	/// <see cref="IList{T}"/> extensions.
	/// </summary>
	public static class ListExtensions
	{
		/// <summary>
		/// Add <paramref name="itemToAdd"/> only if it doesn't already exist in <paramref name="list"/>.
		/// </summary>
		/// <returns>Altered <paramref name="list"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="list"/> is null.</exception>
		public static List<T> AddIfNotExists<T>(this List<T> list, T itemToAdd)
		{
			if (list == null)
				throw new ArgumentNullException(nameof(list));

			if (!list.Contains(itemToAdd))
				list.Add(itemToAdd);

			return list;
		}

		/// <summary>
		/// Add the item to the <paramref name="list"/> unless it already
		/// exists in that list in which case it will remove it instead.
		/// </summary>
		/// <returns>Altered <paramref name="list"/>.</returns>
		public static IList<TSource> AddOrRemoveIfExists<TSource>(this IList<TSource> list, TSource item)
		{
			if (list.Contains(item))
				_ = list.Remove(item);
			else
				list.Add(item);

			return list;
		}

		/// <summary>
		/// Add the item to the <paramref name="list"/> unless it already exists in that
		/// list in which case it will remove it instead.
		/// If <paramref name="list"/> is null then nothing happens.
		/// </summary>
		/// <returns>Altered <paramref name="list"/>.</returns>
		public static IList<TSource>? AddOrRemoveIfExistsNullable<TSource>(this IList<TSource>? list, TSource item)
		{
			if (list == null)
				return null;

			if (list.Contains(item))
				_ = list.Remove(item);
			else
				list.Add(item);

			return list;
		}


		/// <summary>
		/// Determines if the <paramref name="list"/> contains at least 1 item and isn't null.
		/// </summary>
		/// <remarks>Is typically faster than Enumerable.Any() for lists and prevents the CA1860 info message.</remarks>
		/// <returns>True if <paramref name="list"/> contains at least 1 item and isn't null</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool HasAny<TSource>(this IList<TSource>? list)
		{
			return list != null && list.Count > 0;
		}
	}
}
