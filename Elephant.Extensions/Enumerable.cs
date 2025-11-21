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

		/// <summary>
		/// Split the <paramref name="source"/> into chunks of maximum <paramref name="maxChunkSize"/>.
		/// Preserves the original order of elements.
		/// </summary>
		/// <typeparam name="T">IEnumerable type.</typeparam>
		/// <param name="source">Source list.</param>
		/// <param name="maxChunkSize">Maximum chunk size.</param>
		/// <returns>Chunked <paramref name="source"/> with the original order of elements preserved.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="maxChunkSize"/> is less than or equal to zero.</exception>
		public static IEnumerable<List<T>> SplitIntoChunks<T>(this IEnumerable<T> source, int maxChunkSize)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source), "Null cannot be chunked.");

			if (maxChunkSize <= 0)
				throw new ArgumentOutOfRangeException(nameof(maxChunkSize), "Chunk size must be greater than zero.");

			// Fast path for concrete List<T>.
			if (source is List<T> concreteList)
			{
				int count = concreteList.Count;
				if (maxChunkSize >= count)
				{
					yield return source.ToList();
					yield break;
				}

				for (int i = 0; i < count; i += maxChunkSize)
				{
					int len = Math.Min(maxChunkSize, count - i);
					yield return concreteList.GetRange(i, len);
				}
				yield break;
			}

			// Fast path for other random-access lists (IList<T>) to avoid enumerator overhead.
			if (source is IList<T> indexedList)
			{
				int count = indexedList.Count;
				if (maxChunkSize >= count)
				{
					yield return source.ToList();
					yield break;
				}

				for (int i = 0; i < count; i += maxChunkSize)
				{
					int len = Math.Min(maxChunkSize, count - i);
					List<T> chunk = new List<T>(len);
					for (int j = 0; j < len; j++)
						chunk.Add(indexedList[i + j]);
					yield return chunk;
				}
				yield break;
			}

			// General enumerable path (single-pass).
			List<T> bucket = new List<T>(maxChunkSize);
			int bucketCount = 0;
			using (IEnumerator<T> en = source.GetEnumerator())
			{
				while (en.MoveNext())
				{
					bucket.Add(en.Current);
					if (++bucketCount == maxChunkSize)
					{
						yield return bucket;
						bucket = new List<T>(maxChunkSize);
						bucketCount = 0;
					}
				}
			}

			if (bucket.Count > 0)
				yield return bucket;
		}
	}
}
