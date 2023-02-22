using Elephant.Types.Interfaces.Paginations;

namespace Elephant.Models.ResponseModels
{
	/// <inheritdoc cref="IPaginationResponseModel"/>
	public class PaginationResponseModel : IPaginationResponseModel
	{
		/// <summary>
		/// Default <see cref="Offset"/> value.
		/// </summary>
		private const int DefaultOffset = 0;

		/// <summary>
		/// Default <see cref="Limit"/> value.
		/// </summary>
		private const int DefaultLimit = int.MaxValue;

		/// <inheritdoc cref="Offset"/>
		private int _offset = DefaultOffset;

		/// <inheritdoc/>
		public int Offset
		{
			get => _offset;
			set
			{
				_offset = value < 0 ? 0 : value;
			}
		}

		/// <inheritdoc cref="Limit"/>
		private int _limit = DefaultLimit;

		/// <inheritdoc/>
		public int Limit
		{
			get => _limit;
			set
			{
				_limit = value < 1 ? int.MaxValue : value;
			}
		}

		/// <inheritdoc/>
		public int TotalPageCount { get; set; }

		/// <inheritdoc/>
		public bool IsFirstPage { get; set; }

		/// <inheritdoc/>
		public bool IsLastPage { get; set; }

		/// <inheritdoc/>
		public int TotalItems { get; set; }

		/// <inheritdoc/>
		public string? PageUriFirst { get; set; } = null;

		/// <inheritdoc/>
		public string? PageUriLast { get; set; } = null;

		/// <inheritdoc/>
		public string? PageUriPrevious { get; set; } = null;

		/// <inheritdoc/>
		public string? PageUriNext { get; set; } = null;

		/// <summary>
		/// Constructor.
		/// </summary>
		public PaginationResponseModel()
		{
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		/// <param name="offset">The offset (=page number). Starts at 0 and defaults to 0. Cannot be smaller than 0.</param>
		/// <param name="limit">
		/// Maximum amount of results per page.
		/// If this value is set to a value smaller than 1 then the value <see cref="int.MaxValue"/> will be assigned instead.
		/// Defaults to <see cref="int.MaxValue"/>.
		/// </param>
		public PaginationResponseModel(int offset = 0, int limit = int.MaxValue)
		{
			Offset = offset < 0 ? DefaultOffset : offset;
			Limit = limit < 1 ? DefaultLimit : limit;
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		public PaginationResponseModel(int offset, int limit, int totalItems,
			int totalPageCount, bool isFirstPage, bool isLastPage, string? pageUrlFirst = null,
			string? pageUrlLast = null, string? pageUrlPrevious = null, string? pageUrlNext = null)
		{
			Offset = offset;
			Limit = limit;

			TotalItems = totalItems;
			TotalPageCount = totalPageCount;
			IsFirstPage = isFirstPage;
			IsLastPage = isLastPage;
			PageUriFirst = pageUrlFirst;
			PageUriLast = pageUrlLast;
			PageUriPrevious = pageUrlPrevious;
			PageUriNext = pageUrlNext;
		}
	}
}
