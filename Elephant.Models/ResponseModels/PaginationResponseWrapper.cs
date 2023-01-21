using Elephant.Types.Interfaces.Paginations;

namespace Elephant.Models.RequestModels
{
	/// <inheritdoc cref="IPaginationResponseWrapper{TData}"/>
	public class PaginationResponseWrapper<TData> : IPaginationResponseWrapper<TData>
		where TData : new()
	{
		/// <summary>
		/// Default <see cref="Offset"/> value.
		/// </summary>
		private const int DefaultOffset = 0;

		/// <summary>
		/// Default <see cref="Limit"/> value.
		/// </summary>
		private const int DefaultLimit = int.MaxValue;

		/// <inheritdoc/>
		public TData? Data { get; set; } = default;

		/// <summary>
		/// The offset (=page number) that this <see cref="Data"/>
		/// is taken from. Starts at 0 and defaults to 0.
		/// </summary>
		private int _offset = DefaultOffset;

		/// <inheritdoc/>
		public int Offset
		{
			get => _offset;
			set
			{
				_offset = value < 0 ? 0 : _offset;
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
				_limit = value < 1 ? int.MaxValue : _limit;
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
		public string? PageUrlFirst { get; set; } = null;

		/// <inheritdoc/>
		public string? PageUrlLast { get; set; } = null;

		/// <inheritdoc/>
		public string? PageUrlPrevious { get; set; } = null;

		/// <inheritdoc/>
		public string? PageUrlNext { get; set; } = null;

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
		{
			Data = data;
			Offset = offset < 0 ? DefaultOffset : offset;
			Limit = limit < 1 ? DefaultLimit : limit;
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		public PaginationResponseWrapper(TData? data, int offset, int limit, int totalItems,
			int totalPageCount, bool isFirstPage, bool isLastPage, string? pageUrlFirst = null,
			string? pageUrlLast = null, string? pageUrlPrevious = null, string? pageUrlNext = null)
		{
			Data = data;
			Offset = offset;
			Limit = limit;

			TotalItems = totalItems;
			TotalPageCount = totalPageCount;
			IsFirstPage = isFirstPage;
			IsLastPage = isLastPage;
			PageUrlFirst = pageUrlFirst;
			PageUrlLast = pageUrlLast;
			PageUrlPrevious = pageUrlPrevious;
			PageUrlNext = pageUrlNext;
		}
	}
}
