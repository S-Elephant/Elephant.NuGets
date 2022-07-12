namespace Elephant.Common.ResponseWrappers
{
    /// <summary>
    /// A wrapper for returning paginated data (for 1 page) along with a message and success status.
    /// The <see cref="PageNumber"/> starts at 1.
    /// </summary>
    /// <remarks>This class does not paginate, it only contains paginated data (or it contains nothing).</remarks>
    /// <typeparam name="TData">Your data type.</typeparam>
    public interface IPagedResponseWrapper<TData>
        : IResponseWrapper<TData>
        where TData : new()
    {
        /// <summary>
        /// The data items.
        /// </summary>
        new List<TData>? Data { get; set; }

        /// <summary>
        /// Indicates if this is the first page.
        /// </summary>
        bool IsFirstPage { get; set; }

        /// <summary>
        /// Indicates if this is the last page.
        /// </summary>
        bool IsLastPage { get; set; }
        
        /// <summary>
        /// The page number that this <see cref="Data"/> is taken from. Starts at 1.
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// Maximum amount of items inside a page.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// First page API URL.
        /// </summary>
        string PageUrlFirst { get; set; }

        /// <summary>
        /// Last page API URL.
        /// </summary>
        string PageUrlLast { get; set; }

        /// <summary>
        /// Next page API URL. Is null if there's no next.
        /// </summary>
        string? PageUrlNext { get; set; }

        /// <summary>
        /// Previous page API URL. Is null if there's no previous.
        /// </summary>
        string? PageUrlPrevious { get; set; }

        /// <summary>
        /// The total page count given the current <see cref="PageSize"/>.
        /// </summary>
        int TotalPageCount { get; set; }

        /// <summary>
        /// The total record count in the storage (=usually the database).
        /// </summary>
        int TotalRecords { get; set; }
    }
}