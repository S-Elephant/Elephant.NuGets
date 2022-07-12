namespace Elephant.Common
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Obsolete("Use ResponseWrappers.ResponseWrapper instead.")]
    public class ResultStatus<T> : ResultSuccess<T>, IResultStatus<T>
    {
        /// <summary>
        /// Status code 200.
        /// </summary>
        private const int CodeStatus200OK = 200;

        /// <summary>
        /// Status code 400.
        /// </summary>
        private const int CodeStatus400BadRequest = 400;

        /// <summary>
        /// Status code 401.
        /// </summary>
        private const int CodeStatus401Unauthorized = 401;

        /// <summary>
        /// Status code 404.
        /// </summary>
        private const int CodeStatus404NotFound = 404;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int StatusCode { get; set; } = CodeStatus200OK;

        /// <summary>
        /// Creates an empty but successful result.
        /// </summary>
        public ResultStatus()
        {
        }

        /// <summary>
        /// Create a succesfull <see cref="ResultStatus"/> with a <paramref name="value"/>.
        /// </summary>
        public ResultStatus(T value, string message = "")
        {
            Value = value;
            Message = message;
        }

        /// <summary>
        /// Create a <see cref="ResultStatus"/> with a value. <see cref="IsSuccess"/> is set to true if the status code equals 200.
        /// </summary>
        public ResultStatus(int statusCode, string message)
        {
            StatusCode = statusCode;
            IsSuccess = statusCode == CodeStatus200OK;
            Message = message;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void BadRequest(string message = "")
        {
            Message = message;
            IsSuccess = false;
            StatusCode = CodeStatus400BadRequest;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Unauthorized(string message = "")
        {
            Message = message;
            IsSuccess = false;
            StatusCode = CodeStatus401Unauthorized;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ResultStatus<TNewType> ConvertToNewTypeWithNewValue<TNewType>(TNewType newValue)
        {
            return new ResultStatus<TNewType>()
            {
                IsSuccess = IsSuccess,
                Message = Message,
                StatusCode = StatusCode,
                Value = newValue,
            };
        }

        /// <summary>
        /// Static constructor for the NotFound result.
        /// </summary>
        public static ResultStatus<T> NotFound()
        {
            return new ResultStatus<T>()
            {
                IsSuccess = false,
                StatusCode = CodeStatus404NotFound,
            };
        }
    }
}
