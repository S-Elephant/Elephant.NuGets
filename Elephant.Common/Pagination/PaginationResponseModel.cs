namespace Elephant.Common.Pagination
{
    /// <summary>
    /// Pagination response model.
    /// </summary>
    public class PaginationResponseModel<T> : IPaginationResponseModel<T>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<T> Records { get; set; } = new List<T>();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int TotalPageCount { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsFirstPage { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsLastPage { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string PageUrlSuffixFirst { get; set; } = string.Empty;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string PageUrlSuffixLast { get; set; } = string.Empty;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string? PageUrlSuffixPrevious { get; set; } = null;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string? PageUrlSuffixNext { get; set; } = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PaginationResponseModel()
        {
        }
    }
}
