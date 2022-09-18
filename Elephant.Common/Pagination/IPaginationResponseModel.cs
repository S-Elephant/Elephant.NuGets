namespace Elephant.Common.Pagination
{
    /// <summary>
    /// Pagination response model.
    /// </summary>
    public interface IPaginationResponseModel<T>
    {
        /// <summary>
        /// The current page. Starts at 1.
        /// </summary>
        int CurrentPage { get; set; }

        /// <summary>
        /// Indicates if this is the first page.
        /// </summary>
        bool IsFirstPage { get; set; }

        /// <summary>
        /// Indicates if this is the last page.
        /// </summary>
        bool IsLastPage { get; set; }

        /// <summary>
        /// Maximum amount of items inside a page.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// First page API URL suffix.
        /// </summary>
        string PageUrlSuffixFirst { get; set; }

        /// <summary>
        /// Last page API URL suffix.
        /// </summary>
        string PageUrlSuffixLast { get; set; }

        /// <summary>
        /// Next page API URL suffix.
        /// </summary>
        string? PageUrlSuffixNext { get; set; }

        /// <summary>
        /// Previous page API URL suffix.
        /// </summary>
        string? PageUrlSuffixPrevious { get; set; }

        /// <summary>
        /// Records.
        /// </summary>
        List<T> Records { get; set; }

        /// <summary>
        /// The total page count given the current <see cref="PageSize"/>.
        /// </summary>
        int TotalPageCount { get; set; }

        /// <summary>
        /// The total record count in the database.
        /// </summary>
        int TotalRecords { get; set; }
    }
}