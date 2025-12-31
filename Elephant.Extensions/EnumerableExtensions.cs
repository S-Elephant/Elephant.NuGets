namespace Elephant.Extensions
{
	/// <summary>
	/// <see cref="IEnumerable{T}"/> extensions.
	/// </summary>
	public static class EnumerableExtensions
	{
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

			// If the sequence is already a set, it guarantees uniqueness.
			if (source is ISet<TSource>)
				return true;

			// Fast-path: if collection exposes a Count and it's 0 or 1 then it's unique.
			if (source is ICollection<TSource> col)
			{
				if (col.Count <= 1)
					return true;
			}
			else if (source is IReadOnlyCollection<TSource> roCol)
			{
				if (roCol.Count <= 1)
					return true;
			}
			else if (source is System.Collections.ICollection nonGeneric)
			{
				if (nonGeneric.Count <= 1)
					return true;
			}

			// General single-pass check: try to add each element to a HashSet and bail out
			// as soon as a duplicate is detected. This enumerates source only once and
			// stops early when a duplicate exists.
			HashSet<TSource> seen = [];
			foreach (TSource item in source)
			{
				if (!seen.Add(item))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Return true if ALL of <paramref name="values"/> are contained in <paramref name="source"/>.
		/// Returns true if <paramref name="values"/> is empty or null.
		/// Returns true if <paramref name="source"/> is null.
		/// </summary>
		/// <remarks>
		/// Is generally faster than using Linq All() + Contains() for large inputs because this
		/// implementation builds a HashSet for O(1) lookups; complexity is O(n + m) (build + lookups)
		/// versus O(n * m) for repeated Contains calls. Actual speedup depends on the collection sizes
		/// and types.
		/// </remarks>
		public static bool ContainsAll<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource>? values)
		{
			// If source is null then it cannot contain all.
			if (source == null)
				return false;

			// If no values to check then all values are contained.
			if (values == null)
				return true;

			// If values is empty then return true.
			if (values.IsEmpty())
				return true;

			// Fast path: if source is a set, use its efficient Contains implementation directly. This
			// avoids allocating another HashSet and uses whatever comparer the set uses.
			if (source is ISet<TSource> sourceSet)
			{
				foreach (TSource value in values)
					if (!sourceSet.Contains(value))
						return false;
				return true;
			}

			// Fast path: if source exposes ICollection<T>, use its Contains.
			// Note: ICollection<T>.Contains may be O(n) for lists; this path is chosen to avoid
			// allocating a HashSet when the source collection's Contains is already optimized.
			if (source is ICollection<TSource> sourceCol)
			{
				foreach (TSource value in values)
					if (!sourceCol.Contains(value))
						return false;
				return true;
			}

			// General fallback: build a HashSet from source to get O(1) lookups for all values.
			// This enumerates the source once (cost O(n)) and then performs O(1) lookups for each value (cost O(m)).
			// Overall complexity: O(n + m). Uses EqualityComparer<TSource>.Default for element comparison.
			HashSet<TSource> set = new(source);
			foreach (TSource value in values)
				if (!set.Contains(value))
					return false;

			return true;
		}

		/// <summary>
		/// Return true if NONE of <paramref name="values"/> are contained in <paramref name="source"/>.
		/// Returns true if either or both <paramref name="source"/> or <paramref name="values"/> is empty or null.
		/// </summary>
		public static bool ContainsNone<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> values)
		{
			if (source == null || values == null)
				return true;

			// Fast-paths for empty collections (when Count is available).
			if (source is ICollection<TSource> srcCol && srcCol.Count == 0)
				return true;
			if (values is ICollection<TSource> valCol && valCol.Count == 0)
				return true;

			// If source is a set, use its efficient Contains implementation.
			if (source is ISet<TSource> sourceSet)
			{
				foreach (TSource value in values)
					if (sourceSet.Contains(value))
						return false;

				return true;
			}

			// Obtain counts (if available) using distinct variable names.
			int sourceCount = -1;
			int valuesCount = -1;

			if (source is ICollection<TSource> sourceCollection)
				sourceCount = sourceCollection.Count;
			else if (source is IReadOnlyCollection<TSource> readonlySourceCollection)
				sourceCount = readonlySourceCollection.Count;

			if (values is ICollection<TSource> valueCollection)
				valuesCount = valueCollection.Count;
			else if (values is IReadOnlyCollection<TSource> readonlyValueCollection)
				valuesCount = readonlyValueCollection.Count;

			// If both counts known, build HashSet from the smaller collection.
			if (sourceCount >= 0 && valuesCount >= 0)
			{
				if (sourceCount <= valuesCount)
				{
					HashSet<TSource> set = new(source);
					foreach (TSource value in values)
						if (set.Contains(value))
							return false;

					return true;
				}
				else
				{
					HashSet<TSource> set = new(values);
					foreach (TSource src in source)
						if (set.Contains(src))
							return false;

					return true;
				}
			}

			// If only values count known, build HashSet from values and scan source.
			if (valuesCount >= 0)
			{
				HashSet<TSource> set = new(values);
				foreach (TSource s in source)
					if (set.Contains(s))
						return false;

				return true;
			}

			// If only source count known, build HashSet from source and scan values.
			if (sourceCount >= 0)
			{
				HashSet<TSource> set = new(source);
				foreach (TSource v in values)
					if (set.Contains(v))
						return false;

				return true;
			}

			// Fallback: no counts available — build HashSet from values and scan source (one enumeration each).
			HashSet<TSource> fallbackSet = new(values);
			foreach (TSource s in source)
				if (fallbackSet.Contains(s))
					return false;

			return true;
		}

		/// <summary>
		/// Determines if the <paramref name="source"/> is empty.
		/// </summary>
		/// <returns>True if empty.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
		public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (source is ICollection<TSource> col)
				return col.Count == 0;

			if (source is IReadOnlyCollection<TSource> roCol)
				return roCol.Count == 0;

			if (source is System.Collections.ICollection nonGeneric)
				return nonGeneric.Count == 0;

			using (IEnumerator<TSource> en = source.GetEnumerator())
				return !en.MoveNext();
		}

		/// <summary>
		/// Return true if the first item of <paramref name="source"/> equals <paramref name="itemToCompare"/>.
		/// </summary>
		/// <typeparam name="TSource">An <see cref="IEnumerable{TSource}"/> whose elements to apply the predicate to.</typeparam>
		/// <param name="source">Source.</param>
		/// <param name="itemToCompare">Item to compare with the first item.</param>
		/// <returns>True if the first element equals <paramref name="itemToCompare"/>; otherwise false.</returns>
		public static bool IsFirst<TSource>(this IEnumerable<TSource> source, TSource itemToCompare)
		{
			if (source == null)
				return false;

			// Fast path for random-access collections.
			if (source is IList<TSource> list)
			{
				if (list.Count == 0)
					return false;
				return EqualityComparer<TSource>.Default.Equals(list[0], itemToCompare);
			}

			if (source is IReadOnlyList<TSource> readonlyList)
			{
				if (readonlyList.Count == 0)
					return false;
				return EqualityComparer<TSource>.Default.Equals(readonlyList[0], itemToCompare);
			}

			// Fast path for collections that expose Count.
			if (source is ICollection<TSource> collection)
			{
				if (collection.Count == 0)
					return false;
			}
			else if (source is System.Collections.ICollection nonGeneric)
			{
				if (nonGeneric.Count == 0)
					return false;
			}

			// Single-pass fallback: obtain first element without re-enumerating.
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
					return false;

				return EqualityComparer<TSource>.Default.Equals(enumerator.Current, itemToCompare);
			}
		}

		/// <summary>
		/// Return true if the last item of <paramref name="source"/> equals <paramref name="itemToCompare"/>.
		/// </summary>
		/// <typeparam name="TSource">An <see cref="IEnumerable{TSource}"/> whose elements to apply the predicate to.</typeparam>
		/// <param name="source">Source.</param>
		/// <param name="itemToCompare">Item to compare with the last item.</param>
		/// <returns>True if the last element equals <paramref name="itemToCompare"/>; otherwise false.</returns>
		public static bool IsLast<TSource>(this IEnumerable<TSource> source, TSource itemToCompare)
		{
			if (source == null)
				return false;

			// Fast path for random-access collections.
			if (source is IList<TSource> list)
			{
				if (list.Count == 0)
					return false;
				return EqualityComparer<TSource>.Default.Equals(list[list.Count - 1], itemToCompare);
			}

			if (source is IReadOnlyList<TSource> readonlyList)
			{
				if (readonlyList.Count == 0)
					return false;
				return EqualityComparer<TSource>.Default.Equals(readonlyList[readonlyList.Count - 1], itemToCompare);
			}

			// Fast path for collections that expose Count.
			if (source is ICollection<TSource> collection)
			{
				if (collection.Count == 0)
					return false;
			}
			else if (source is System.Collections.ICollection nonGeneric)
			{
				if (nonGeneric.Count == 0)
					return false;
			}

			// Single-pass fallback: iterate once keeping the last element.
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
					return false;

				TSource last = enumerator.Current;
				while (enumerator.MoveNext())
					last = enumerator.Current;

				return EqualityComparer<TSource>.Default.Equals(last, itemToCompare);
			}
		}

		/// <inheritdoc cref="IsEmpty{TSource}(IEnumerable{TSource})"/>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool None<TSource>(this IEnumerable<TSource> source)
		{
			return IsEmpty(source);
		}

		/// <summary>
		/// Determines whether any element of a sequence does not satisfy a condition. This is the same as !source.Any(..).
		/// </summary>
		/// <typeparam name="TSource">An <see cref="IEnumerable{TSource}"/> whose elements to apply the predicate to.</typeparam>
		/// <param name="source">Source.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>True if the source sequence is empty or none of its elements passes the test in the specified predicate; otherwise, false.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return !source.Any(predicate);
		}

		/// <summary>
		/// Split the <paramref name="source"/> into chunks of maximum <paramref name="maxChunkSize"/>.
		/// Preserves the original order of elements.
		/// </summary>
		/// <typeparam name="T">IEnumerable type.</typeparam>
		/// <param name="source">Source IEnumerable.</param>
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
					yield return concreteList;
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
					yield return new List<T>(indexedList);
					yield break;
				}

				for (int i = 0; i < count; i += maxChunkSize)
				{
					int len = Math.Min(maxChunkSize, count - i);
					List<T> chunk = new(len);
					for (int j = 0; j < len; j++)
						chunk.Add(indexedList[i + j]);
					yield return chunk;
				}
				yield break;
			}

			// General enumerable path (single-pass).
			List<T> bucket = new(maxChunkSize);
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
