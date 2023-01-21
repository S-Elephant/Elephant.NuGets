﻿namespace Elephant.Types.Interfaces.Paginations
{
	/// <summary>
	/// Pagination request model.
	/// </summary>
	/// <example><![CDATA[public async Task<IActionResult> All([FromQuery] PaginationRequestModel pagination, CancellationToken cancellationToken)]]></example>
	public interface IPaginationResponseWrapper<TData>
		where TData : new()
	{
		/// <summary>
		/// Wrapped data.
		/// </summary>
		TData? Data { get; set; }

		/// <summary>
		/// Indicates if this is the first page.
		/// </summary>
		bool IsFirstPage { get; set; }

		/// <summary>
		/// Indicates if this is the last page.
		/// </summary>
		bool IsLastPage { get; set; }

		/// <summary>
		/// The offset (=page number) that this <see cref="Data"/> is taken from. Starts at 0 and defaults to 0.
		/// </summary>
		int Offset { get; set; }

		/// <summary>
		/// Maximum amount of items per page. If this value is set to a value smaller than 1 then the value <see cref="int.MaxValue"/> will be assigned instead.
		/// Defaults to <see cref="int.MaxValue"/>.
		/// </summary>
		int Limit { get; set; }

		/// <summary>
		/// First page API URL.
		/// </summary>
		string? PageUrlFirst { get; set; }

		/// <summary>
		/// Last page API URL.
		/// </summary>
		string? PageUrlLast { get; set; }

		/// <summary>
		/// Next page API URL. Is null if there's no next.
		/// </summary>
		string? PageUrlNext { get; set; }

		/// <summary>
		/// Previous page API URL. Is null if there's no previous.
		/// </summary>
		string? PageUrlPrevious { get; set; }

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