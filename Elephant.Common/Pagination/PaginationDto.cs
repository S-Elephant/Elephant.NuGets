namespace Elephant.Common.Pagination
{
    /// <summary>
    /// Pagination response DTO.
    /// </summary>
    /// <typeparam name="T">The records types.</typeparam>
    public class PaginationDto<T>
	{
		/// <summary>
		/// Records.
		/// </summary>
		public List<T> Records { get; set; } = new List<T>();

		/// <summary>
		/// Starts at 1.
		/// </summary>
		public int CurrentPage { get; private set; }

		/// <summary>
		/// The total page count given the current <see cref="PageSize"/>.
		/// </summary>
		public int TotalPageCount { get; set; }

		/// <summary>
		/// Maximum amount of items inside a page.
		/// </summary>
		public int PageSize { get; private set; } = 1;

		/// <summary>
		/// Indicates if this is the first page.
		/// </summary>
		public bool IsFirstPage { get; set; }

		/// <summary>
		/// Indicates if this is the last page.
		/// </summary>
		public bool IsLastPage { get; set; }

		/// <summary>
		/// The total record cound in the database.
		/// </summary>
		public int TotalRecords { get; set; }

		/// <summary>
		/// First page API url suffix.
		/// </summary>
		public string PageUrlSuffixFirst { get; set; } = string.Empty;

		/// <summary>
		/// Last page API url suffix.
		/// </summary>
		public string PageUrlSuffixLast { get; set; } = string.Empty;

		/// <summary>
		/// Previous page API url suffix.
		/// </summary>
		public string PageUrlSuffixPrevious { get; set; } = string.Empty;

		/// <summary>
		/// Next page API url suffix.
		/// </summary>
		public string PageUrlSuffixNext { get; set; } = string.Empty;

		/// <summary>
		/// Constructor.
		/// </summary>
		public PaginationDto()
		{
		}

		/// <summary>
		/// Constructor that paginates all records.
		/// </summary>
		public PaginationDto(List<T> allUnpaginatedRecords, int currentPage, int pageSize)
		{
			TotalRecords = allUnpaginatedRecords.Count;
			CurrentPage = currentPage;
			TotalPageCount = PaginationHelper.LastPageNumber(TotalRecords, pageSize);
			PageSize = pageSize;
			IsFirstPage = currentPage == 1;
			IsLastPage = PaginationHelper.IsLastPageNumber(TotalRecords, currentPage, pageSize);

			Records = PaginationHelper.Paginate<T>(allUnpaginatedRecords, currentPage, pageSize);
		}

		/// <summary>
		/// Constructor that takes a list of pre-paginated records.
		/// </summary>
		public PaginationDto(List<T> allPrePaginatedRecords, int currentPage, int pageSize, int totalRecordCount)
		{
			TotalRecords = totalRecordCount;
			CurrentPage = currentPage;
			TotalPageCount = PaginationHelper.LastPageNumber(TotalRecords, pageSize);
			PageSize = pageSize;
			IsFirstPage = currentPage == 1;
			IsLastPage = PaginationHelper.IsLastPageNumber(TotalRecords, currentPage, pageSize);

			Records = allPrePaginatedRecords;
		}
	}
}
