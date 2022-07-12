namespace Elephant.Common.ResponseWrappers
{
    /// <inheritdoc/>
    public class PagedResponseWrapper<TData> : ResponseWrapper<TData>, IPagedResponseWrapper<TData> where TData : new()
    {
        /// <inheritdoc/>
        public new List<TData>? Data { get; set; }

        private int _pageNumber;

        /// <inheritdoc/>
        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                _pageNumber = value < 1 ? 1 : _pageNumber;
            }
        }

        private int _pageSize;

        /// <inheritdoc/>
        public int PageSize {
            get => _pageSize;
            set
            {
                _pageSize = value < 1 ? 1 : _pageSize;
            }
        }

        /// <inheritdoc/>
        public int TotalPageCount { get; set; }

        /// <inheritdoc/>
        public bool IsFirstPage { get; set; }

        /// <inheritdoc/>
        public bool IsLastPage { get; set; }

        /// <inheritdoc/>
        public int TotalRecords { get; set; }

        /// <inheritdoc/>
        public string PageUrlFirst { get; set; }

        /// <inheritdoc/>
        public string PageUrlLast { get; set; }

        /// <inheritdoc/>
        public string? PageUrlPrevious { get; set; }

        /// <inheritdoc/>
        public string? PageUrlNext { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PagedResponseWrapper()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Reset();
        }

        public void Reset()
        {
            Data = new List<TData>();
            PageNumber = 1;
            PageSize = 1;
            TotalPageCount = 0;
            IsFirstPage = true;
            IsLastPage = true;
            TotalRecords = 0;
            PageUrlFirst = PageUrlLast = string.Empty;
            PageUrlPrevious = PageUrlNext = null;
        }

        /// <summary>
        /// Constructor with initializers.
        /// </summary>
        public PagedResponseWrapper(List<TData>? data, bool isSuccess, int statusCode, List<string>? errors, string? message,
            int pageNumber, int pageSize, int totalPageCount, bool isFirstPage, bool isLastPage,
            int totalRecords, string pageUrlFirst, string pageUrlLast, string? pageUrlPrevious = null, string? pageUrlNext = null)
            : base(default(TData), isSuccess, statusCode, errors, message)
        {
            Data = data ?? new List<TData>();
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPageCount = totalPageCount;
            IsFirstPage = isFirstPage;
            IsLastPage = isLastPage;
            TotalRecords = totalRecords;
            PageUrlFirst = pageUrlFirst;
            PageUrlLast = pageUrlLast;
            PageUrlPrevious = pageUrlPrevious;
            PageUrlNext = pageUrlNext;
        }

        /// <inheritdoc/>
        public override ResponseWrapper<TData> Error(int statusCode, List<string>? errors = null, string? message = null)
        {
            base.Error(statusCode, errors, message);
            Data = null;

            return this;
        }

        /// <inheritdoc/>
        public override ResponseWrapper<TData> BadRequest(List<string>? errors = null, string? message = null)
        {
            base.BadRequest(errors, message);
            Data = null;

            return this;
        }

        /// <inheritdoc/>
        public override ResponseWrapper<TData> Unauthorized(List<string>? errors = null, string? message = null)
        {
            base.Unauthorized(errors, message);
            Data = null;

            return this;
        }
    }
}
