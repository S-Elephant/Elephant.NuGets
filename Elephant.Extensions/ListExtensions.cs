namespace Elephant.Extensions
{
    /// <summary>
    /// <see cref="IList{T}"/> extensions.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Add the item to the <paramref name="list"/> unless it already exists in that list in which case it will remove it instead.
        /// </summary>
        /// <returns>Altered <paramref name="list"/>.</returns>
        public static IList<TSource> AddOrRemoveIfExists<TSource>(this IList<TSource> list, TSource item)
        {
            if (list.Contains(item))
                list.Remove(item);
            else
                list.Add(item);

            return list;
        }

        /// <summary>
        /// Add the item to the <paramref name="list"/> unless it already exists in that list in which case it will remove it instead.
        /// If <paramref name="list"/> is null then nothing happens.
        /// </summary>
        /// <returns>Altered <paramref name="list"/>.</returns>
        public static IList<TSource>? AddOrRemoveIfExistsNullable<TSource>(this IList<TSource>? list, TSource item)
        {
            if (list == null)
                return null;

            if (list.Contains(item))
                list.Remove(item);
            else
                list.Add(item);

            return list;
        }
    }
}
