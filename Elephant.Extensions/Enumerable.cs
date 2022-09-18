namespace Elephant.Extensions
{
    public static class Enumerable
    {
        /// <summary>
        /// Determines whether any element of a sequence does not satisfy a condition. This is the same as !source.Any(..).
        /// </summary>
        /// <typeparam name="TSource">An System.Collections.Generic.IEnumerable`1 whose elements to apply the predicate to.</typeparam>
        /// <param name="source"></param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>true if the source sequence is empty or none of its elements passes the test in the specified predicate; otherwise, false.</returns>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return !source.Any(predicate);
        }
    }
}
