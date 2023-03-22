using Elephant.Types.Interfaces.Paginations;

namespace Elephant.Models.ResponseModels
{
	/// <inheritdoc cref="IPaginationResponseWrapper{TData}"/>
	public class PaginationResponseWrapper<TData> : PaginationResponseModel, IPaginationResponseWrapper<TData>
		where TData : new()
	{
		/// <inheritdoc/>
		public TData? Data { get; set; } = default;

		/// <summary>
		/// Constructor.
		/// </summary>
		public PaginationResponseWrapper()
		{
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		/// <param name="data">Wrapped data.</param>
		/// <param name="offset">The offset (=page number). Starts at 0 and defaults to 0. Cannot be smaller than 0.</param>
		/// <param name="limit">
		/// Maximum amount of results per page.
		/// If this value is set to a value smaller than 1 then the value <see cref="int.MaxValue"/> will be assigned instead.
		/// Defaults to <see cref="int.MaxValue"/>.
		/// </param>
		public PaginationResponseWrapper(TData? data, int offset = 0, int limit = int.MaxValue)
		: base(offset, limit)
		{
			Data = data;
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		public PaginationResponseWrapper(TData? data, int offset, int limit, int totalItems,
			int totalPageCount, bool isFirstPage, bool isLastPage, string? pageUriFirst = null,
			string? pageUriLast = null, string? pageUriPrevious = null, string? pageUriNext = null)
		: base(offset, limit, totalItems, totalPageCount, isFirstPage, isLastPage, pageUriFirst, pageUriLast, pageUriPrevious, pageUriNext)
		{
			Data = data;
		}
	}
}
