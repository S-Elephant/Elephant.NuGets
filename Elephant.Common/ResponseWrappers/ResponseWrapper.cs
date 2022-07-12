namespace Elephant.Common.ResponseWrappers
{
    /// <inheritdoc/>
    public class ResponseWrapper<TData> : IResponseWrapper<TData> where TData : new()
    {
        /// <summary>
        /// Generic success HTTP success status code.
        /// </summary>
        public const int Status200OK = 200;

        /// <summary>
        /// Created HTTP success status code.
        /// </summary>
        public const int Status201Created = 201;

        /// <summary>
        /// Bad request HTTP error status code.
        /// </summary>
        public const int Status400BadRequest = 400;

        /// <summary>
        /// Unauthorized HTTP error status code.
        /// </summary>
        public const int Status401Unauthorized = 401;

        /// <inheritdoc/>
        public TData? Data { get; set; } = default;

        /// <inheritdoc/>
        public bool IsSuccess { get; set; } = true;

        /// <inheritdoc/>
        public int StatusCode { get; set; } = Status200OK;

        /// <inheritdoc/>
        public List<string> Errors { get; set; } = new();

        /// <inheritdoc/>
        public string? Message { get; set; } = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapper()
        {
        }

        /// <summary>
        /// Constructor with initializers.
        /// </summary>
        public ResponseWrapper(TData? data, bool isSuccess, int statusCode, List<string>? errors = null, string? message = null)
        {
            Data = data;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Errors = errors ?? new List<string>();
            Message = message;
        }

        /// <summary>
        /// Returns true if the specified <paramref name="statusCode"/> is a success status code.
        /// </summary>
        private bool IsSuccessStatusCode(int statusCode)
        {
            return statusCode >= 200 && statusCode < 300;
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> Error(int statusCode, List<string>? errors = null, string? message = null)
        {
            // If for some faulty reason a success status code was provided...
            if (IsSuccessStatusCode(statusCode))
            {
                IsSuccess = true;
                StatusCode = statusCode;
                Errors.Add($"{nameof(Error)} was called but a success status was provided. Status provided: {statusCode}.");
                return this;
            }

            Data = default;
            StatusCode = statusCode;
            IsSuccess = false;
            Message = message;
            Errors = errors ?? new List<string>();

            return this;
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> BadRequest(List<string>? errors = null, string? message = null)
        {
            return Error(Status400BadRequest, errors, message);
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> Unauthorized(List<string>? errors = null, string? message = null)
        {
            return Error(Status401Unauthorized, errors, message);
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> Success(int statusCode, string message = "Success.", bool clearErrors = true)
        {
            StatusCode = statusCode;
            Message = message;
            if (clearErrors)
                Errors.Clear();

            return this;
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> Ok(string message = "Success.", bool clearErrors = true)
        {
            return Success(Status200OK, message, clearErrors);
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> Created(string message = "Created success.", bool clearErrors = true)
        {
            return Success(Status201Created, message, clearErrors);
        }
    }
}
