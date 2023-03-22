namespace Elephant.Types.Interfaces.Paginations
{
	/// <summary>
	/// Pagination response model.
	/// </summary>
	/// <remarks>
	/// May be used as a base class.
	/// </remarks>
	public interface IPaginationResponseModel
	{
		/// <summary>
		/// Indicates if this is the first page.
		/// </summary>
		bool IsFirstPage { get; set; }

		/// <summary>
		/// Indicates if this is the last page.
		/// </summary>
		bool IsLastPage { get; set; }

		/// <summary>
		/// The offset (=page number - 1).
		/// Starts at 0 and defaults to 0.
		/// Can't be smaller than 0.
		/// </summary>
		int Offset { get; set; }

		/// <summary>
		/// Maximum amount of items per page. If this value is set to a value smaller than 1 then the value <see cref="int.MaxValue"/> will be assigned instead.
		/// Defaults to <see cref="int.MaxValue"/>.
		/// </summary>
		int Limit { get; set; }

		/// <summary>
		/// First page API URI.
		/// Is null if unused.
		/// </summary>
		string? PageUriFirst { get; set; }

		/// <summary>
		/// Last page API URI.
		/// Is null if unused.
		/// </summary>
		string? PageUriLast { get; set; }

		/// <summary>
		/// Next page API URI. Is null if there's no next or if this property is unused.
		/// </summary>
		string? PageUriNext { get; set; }

		/// <summary>
		/// Previous page API URI. Is null if there's no previous or if this property is unused.
		/// </summary>
		string? PageUriPrevious { get; set; }

		/// <summary>
		/// The total item count in the storage (= usually the database).
		/// </summary>
		int TotalItems { get; set; }

		/// <summary>
		/// The total page count given the current <see cref="Limit"/>.
		/// </summary>
		int TotalPageCount { get; set; }
	}
}