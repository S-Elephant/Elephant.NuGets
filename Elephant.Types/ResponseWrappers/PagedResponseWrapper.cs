using Elephant.Types.Interfaces.ResponseWrappers;
using System.Collections.Generic;

namespace Elephant.Types.ResponseWrappers
{
    /// <inheritdoc/>
    public class PagedResponseWrapper<TData> : ResponseWrapper<TData>, IPagedResponseWrapper<TData>
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
        public new List<TData>? Data { get; set; }

        /// <inheritdoc cref="Offset"/>
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
        public PagedResponseWrapper()
        {
            Reset();
        }

        /// <inheritdoc/>
        public void Reset()
        {
            Data = new List<TData>();
            Offset = DefaultOffset;
            Limit = DefaultLimit;
            TotalPageCount = 0;
            IsFirstPage = true;
            IsLastPage = true;
            TotalItems = 0;
            PageUrlFirst = PageUrlLast = string.Empty;
            PageUrlPrevious = PageUrlNext = null;
        }

        /// <summary>
        /// Constructor with initializers.
        /// </summary>
        public PagedResponseWrapper(List<TData>? data, bool isSuccess, int statusCode, string? message,
            int pageNumber, int pageSize, int totalPageCount, bool isFirstPage, bool isLastPage,
            int totalRecords, string? pageUrlFirst = null, string? pageUrlLast = null, string? pageUrlPrevious = null, string? pageUrlNext = null)
            : base(default, statusCode, message)
        {
            Data = data ?? new List<TData>();
            Offset = pageNumber;
            Limit = pageSize;
            TotalPageCount = totalPageCount;
            IsFirstPage = isFirstPage;
            IsLastPage = isLastPage;
            TotalItems = totalRecords;
            PageUrlFirst = pageUrlFirst;
            PageUrlLast = pageUrlLast;
            PageUrlPrevious = pageUrlPrevious;
            PageUrlNext = pageUrlNext;
        }
    }
}
