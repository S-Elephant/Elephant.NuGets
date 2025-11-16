using Elephant.Types.Interfaces.Paginations;

namespace Elephant.Common.Pagination
{
	/// <summary>
	/// Pagination utilities and extensions using offsets and limits.
	/// Page numbers (=offset) start at 0 and pagesize = limit.
	/// If limit is smaller than 1 then no limit will be applied.
	/// </summary>
	public static class PaginationHelper
	{
		/// <summary>
		/// Paginates <paramref name="source"/> and returns the paginated <paramref name="source"/>.
		/// <paramref name="offset"/> is capped to the maximum possible value.
		/// </summary>
		/// <param name="source">The unpaginated elements.</param>
		/// <param name="offset">Starts at 0.</param>
		/// <param name="limit">
		/// Maximum number of items per page. If it's zero or less or equals to
		/// <see cref="int.MaxValue"/> then no limit will be applied.
		/// </param>
		public static List<TSource> Paginate<TSource>(IList<TSource> source, int offset, int limit)
		{
			// Handle special limit cases early to avoid integer overflow.
			if (limit <= 0)
				return new List<TSource>(); // Return empty list for zero or negative limit.

			if (limit == int.MaxValue)
				return source.ToList(); // Return all elements when limit is MaxValue.

			if (offset < 0)
				offset = 0;

			// Max offset.
			int totalPageCount = TotalPageCount(source.Count, limit);
			if (offset > totalPageCount)
				offset = totalPageCount - 1;

			return source.Skip(offset * limit).Take(limit).ToList();
		}

		/// <summary>
		/// Use this overload for database Linq queries. Returns the paginated <paramref name="source"/>.
		/// <paramref name="offset"/> is capped to the maximum possible value.
		/// </summary>
		/// <param name="source">The unpaginated elements.</param>
		/// <param name="offset">Starts at 0.</param>
		/// <param name="limit">
		/// Maximum number of items per page. If it's zero or less or equals to
		/// <see cref="int.MaxValue"/> then no limit will be applied.
		/// </param>
		public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> source, int offset, int limit)
		{
			// Handle special limit cases early to avoid integer overflow.
			if (limit <= 0)
				return source.Take(0); // Return empty result for zero or negative limit.

			if (offset < 0)
				offset = 0;

			// Max offset.
			int totalPageCount = TotalPageCount(source.Count(), limit);
			if (offset > totalPageCount)
				offset = totalPageCount - 1;

			IQueryable<TSource> resultWithOffset = source.Skip(offset * limit);

			if (limit <= 0 || limit == int.MaxValue)
				return resultWithOffset;
			return resultWithOffset.Take(limit);
		}

		/// <summary>
		/// Use this overload for database Linq queries. Returns the paginated <paramref name="source"/>.
		/// <paramref name="paginationRequest"/> its offset is capped to the maximum possible value.
		/// </summary>
		/// <param name="source">The unpaginated elements.</param>
		/// <param name="paginationRequest">It's offset starts at 0.
		/// It's limit is the maximum number of items per page. If it's zero or less or equals to
		/// <see cref="int.MaxValue"/> then no limit will be applied.
		/// </param>
		public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> source, IPaginationRequest paginationRequest)
		{
			return source.Paginate(paginationRequest.Offset, paginationRequest.Limit);
		}

		/// <summary>
		/// Calculates the last page number (index-based).
		/// Page numbers start at 0 and the minimum page number returned is 0.
		/// So basically the total amount of pages minus 1.
		/// </summary>
		/// <param name="sourceCount">Total item count. Returns 0 if this value is 0 or smaller.</param>
		/// <param name="limit">Maximum number of items per page. Returns 0 if this value is 0 or smaller.</param>
		public static int LastOffset(int sourceCount, int limit)
		{
			if (sourceCount <= 0 || limit <= 0)
				return 0;

			return TotalPageCount(sourceCount, limit) - 1;
		}

		/// <summary>
		/// Return how many pages there are given the specified <paramref name="sourceCount"/> and <paramref name="limit"/> values.
		/// </summary>
		/// <param name="sourceCount">Total item count. Returns 0 if this value is 0 or smaller.</param>
		/// <param name="limit">Maximum number of items per page. Returns 0 if this value is 0 or smaller.</param>
		public static int TotalPageCount(int sourceCount, int limit)
		{
			if (sourceCount <= 0 || limit <= 0)
				return 0;

			// Handle int.MaxValue as unlimited. This also avoids overflows.
			if (limit == int.MaxValue)
				return 1; // With no limit, there's only 1 page containing all items.

			// Use long arithmetic to avoid overflow in the calculation.
			long pageCount = ((long)sourceCount + limit - 1) / limit;

			// Cap to int.MaxValue if result exceeds it.
			return pageCount > int.MaxValue ? int.MaxValue : (int)pageCount;
		}

		/// <summary>
		/// Return the current offset (= page number - 1), taking into account the <paramref name="sourceCount"/> and <paramref name="limit"/> values.
		/// If either <paramref name="sourceCount"/> or <paramref name="limit"/> is 0 then 0 is returned.
		/// If <paramref name="offset"/> is smaller than 0 then 0 is returned.
		/// If <paramref name="offset"/> is equal or greater than the total page count then the total pagecount minus 1 is returned.
		/// </summary>
		/// <param name="sourceCount">Total item count.</param>
		/// <param name="offset">Starts at 0.</param>
		/// <param name="limit">Maximum number of items per page.</param>
		public static int CurrentOffset(int sourceCount, int offset, int limit)
		{
			if (offset <= 0)
				return 0;

			int totalPageCount = TotalPageCount(sourceCount, limit);

			return offset < totalPageCount ? offset : Math.Max(0, totalPageCount - 1);
		}

		/// <summary>
		/// Determine if <paramref name="offset"/> is the last page.
		/// Pages start at 0 because they are offsets (index based).
		/// A negative <paramref name="limit"/> counts as 0.
		/// Returns false if the offset is larger than the total amount of pages.
		/// </summary>
		/// <param name="sourceCount">Total item count. Returns <c>true</c> if this is 0 or smaller.</param>
		/// <param name="offset">Starts at 0.</param>
		/// <param name="limit">Maximum number of items per page. Returns <c>true</c> if this is 0 or smaller
		/// or if this is <see cref="int.MaxValue"/>.</param>
		public static bool IsLastPage(int sourceCount, int offset, int limit)
		{
			if (limit <= 0 || sourceCount <= 0 || limit == int.MaxValue)
				return true;

			int lastPageIndex = LastOffset(sourceCount, limit);

			// offset is basically the current page index.
			return offset == lastPageIndex;
		}

		/// <summary>
		/// Determine if <paramref name="offset"/> is the last page.
		/// Pages start at 0 because they are offsets (index based).
		/// A negative <paramref name="limit"/> counts as 0.
		/// Returns true if the offset is equal or larger than the total amount of pages.
		/// </summary>
		/// <param name="sourceCount">Total item count. Returns <c>true</c> if this is 0 or smaller.</param>
		/// <param name="offset">Starts at 0.</param>
		/// <param name="limit">Maximum number of items per page. Returns <c>true</c> if this is 0 or smaller
		/// or if this is <see cref="int.MaxValue"/>.</param>
		public static bool IsLastPageIgnoreOverflow(int sourceCount, int offset, int limit)
		{
			if (limit <= 0 || sourceCount <= 0 || limit == int.MaxValue)
				return true;

			return ((long)offset + 1) * limit >= sourceCount;
		}
	}
}
