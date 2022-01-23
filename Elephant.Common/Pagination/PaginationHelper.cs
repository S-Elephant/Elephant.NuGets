namespace Elephant.Common.Pagination
{
    /// <summary>
    /// Pagination utilities and extensions.
    /// </summary>
    public static class PaginationHelper
	{
		/// <summary>
		/// Returns the paginated <paramref name="source"/>.
		/// </summary>
		/// <param name="source">The unpaginated elements.</param>
		/// <param name="pageNumber">Starts at 1.</param>
		/// <param name="pageSize">Must be greater than 0.</param>
		public static List<T> Paginate<T>(IList<T> source, int pageNumber, int pageSize)
		{
			return source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
		}

		/// <summary>
		/// Use this overload for database Linq queries. Returns the paginated <paramref name="source"/>.
		/// </summary>
		/// <param name="source">The unpaginated elements.</param>
		/// <param name="pageNumber">Starts at 1.</param>
		/// <param name="pageSize">Must be greater than 0.</param>
		public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> source, int pageNumber, int pageSize)
		{
			return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
		}

		/// <summary>
		/// Use this overload for database Linq queries. Returns the paginated <paramref name="source"/>.
		/// </summary>
		public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> source, IPaginationRequest paginationRequest)
		{
			return source.Skip((paginationRequest.CurrentPage - 1) * paginationRequest.PageSize).Take(paginationRequest.PageSize);
		}

		/// <summary>
		/// Calculates the last page number. Page numbers start at 1 and the minimum page number returned is 1.
		/// </summary>
		public static int LastPageNumber(int sourceCount, int pageSize)
		{
			if (sourceCount <= 0 || pageSize <= 0)
				return 1;

			return (int)Math.Ceiling(sourceCount / (float)pageSize);
		}

		/// <summary>
		/// Determine if <paramref name="pageNumber"/> is the last page.
		/// </summary>
		public static bool IsLastPageNumber(int sourceCount, int pageNumber, int pageSize)
		{
			return pageNumber == LastPageNumber(sourceCount, pageSize);
		}
	}
}
