namespace Elephant.Common.Pagination
{
    /// <summary>
    /// Pagination request.
    /// </summary>
    public interface IPaginationRequest
    {
        /// <summary>
        /// The current requested page. Starts at 1.
        /// </summary>
        int CurrentPage { get; set; }

        /// <summary>
        /// The maximum amount of items per page for this request.
        /// </summary>
        int PageSize { get; set; }
    }
}
