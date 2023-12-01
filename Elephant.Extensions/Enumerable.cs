namespace Elephant.Extensions
{
	/// <summary>
	/// <see cref="IEnumerable{T}"/> extensions.
	/// </summary>
	public static class Enumerable
	{
		/// <summary>
		/// Determines if the <paramref name="source"/> is empty.
		/// </summary>
		/// <returns>True if empty.</returns>
		public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return !source.Any();
		}

		/// <summary>
		/// Return true if ALL of <paramref name="values"/> are contained in <paramref name="source"/>.
		/// Returns true if <paramref name="values"/> is empty.
		/// </summary>
		public static bool ContainsAll<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> values)
		{
			return values.All(value => source.Contains(value));
		}

		/// <summary>
		/// Return true if NONE of <paramref name="values"/> are contained in <paramref name="source"/>.
		/// Returns true if either <paramref name="source"/> or <paramref name="values"/> is empty.
		/// </summary>
		public static bool ContainsNone<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> values)
		{
			// ReSharper disable once PossibleMultipleEnumeration (B). Because otherwise it will execute the query.
			if (!source.Any() || !values.Any())
				return true;

			// ReSharper disable once PossibleMultipleEnumeration (B). Because otherwise it will execute the query.
			return !source.Intersect(values).Any(); // Note: LINQ Intersect returns the common elements from both collections.
		}

		/// <summary>
		/// Determines if the <paramref name="source"/> is empty.
		/// </summary>
		/// <returns>True if empty.</returns>
		public static bool None<TSource>(this IEnumerable<TSource> source)
		{
			return !source.Any();
		}

		/// <summary>
		/// Determines whether any element of a sequence does not satisfy a condition. This is the same as !source.Any(..).
		/// </summary>
		/// <typeparam name="TSource">An <see cref="IEnumerable{TSource}"/> whose elements to apply the predicate to.</typeparam>
		/// <param name="source">Source.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>True if the source sequence is empty or none of its elements passes the test in the specified predicate; otherwise, false.</returns>
		public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return !source.Any(predicate);
		}

		/// <summary>
		/// Return true if the first item of <paramref name="source"/> equals <paramref name="itemToCompare"/>.
		/// </summary>
		/// <typeparam name="TSource">An <see cref="IEnumerable{TSource}"/> whose elements to apply the predicate to.</typeparam>
		/// <param name="source">Source.</param>
		/// <param name="itemToCompare">Item to compare with the last item.</param>
		/// <returns>True if the source sequence is empty or none of its elements passes the test in the specified predicate; otherwise, false.</returns>
		public static bool IsFirst<TSource>(this IEnumerable<TSource> source, TSource itemToCompare)
		{
			return EqualityComparer<TSource>.Default.Equals(source.First(), itemToCompare);
		}

		/// <summary>
		/// Return true if the last item of <paramref name="source"/> equals <paramref name="itemToCompare"/>.
		/// </summary>
		/// <typeparam name="TSource">An <see cref="IEnumerable{TSource}"/> whose elements to apply the predicate to.</typeparam>
		/// <param name="source">Source.</param>
		/// <param name="itemToCompare">Item to compare with the last item.</param>
		/// <returns>True if the source sequence is empty or none of its elements passes the test in the specified predicate; otherwise, false.</returns>
		public static bool IsLast<TSource>(this IEnumerable<TSource> source, TSource itemToCompare)
		{
			return EqualityComparer<TSource>.Default.Equals(source.Last(), itemToCompare);
		}

		/// <summary>
		/// Return true if EVERY item in <paramref name="source"/> is unique or if it is empty or null.
		/// If every item must always be unique then consider using a <see cref="HashSet{T}"/>.
		/// </summary>
		/// <typeparam name="TSource">IEnumerable type.</typeparam>
		/// <param name="source">Source list.</param>
		/// <returns>True if EVERY item in <paramref name="source"/> is unique or if it is empty or null.</returns>
		public static bool AreAllItemsUnique<TSource>(this IEnumerable<TSource>? source)
		{
			if (source == null)
				return true;

			// ReSharper disable once PossibleMultipleEnumeration (B). Because otherwise it will execute the query.
			return source.Distinct().Count() == source.Count();
		}
	}
}
