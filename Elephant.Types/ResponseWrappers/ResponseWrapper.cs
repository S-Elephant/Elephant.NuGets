using System.Collections.Generic;

namespace Elephant.Types.ResponseWrappers
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

        /// <summary>
        /// Not found HTTP error status code.
        /// </summary>
        public const int Status404NotFound = 404;

        /// <inheritdoc/>
        public TData? Data { get; set; } = default;

        /// <inheritdoc/>
        public bool IsSuccess { get; set; } = true;

        /// <inheritdoc/>
        public int StatusCode { get; set; } = Status200OK;

        /// <inheritdoc/>
        public string? Message { get; set; } = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapper()
        {
        }

        /// <summary>
        /// Constructor with just data.
        /// </summary>
        public ResponseWrapper(TData? data)
        {
            Data = data;
        }

        /// <summary>
        /// Constructor with initializers.
        /// </summary>
        public ResponseWrapper(TData? data, bool isSuccess, int statusCode, string? message = null)
        {
            Data = data;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
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
        public virtual ResponseWrapper<TData> Success(int statusCode, string message = "Success.", TData? data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;

            return this;
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> Error(int statusCode, string? message = null, TData? data = default)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
            IsSuccess = IsSuccessStatusCode(statusCode);

            return this;
        }

        #region Assign status

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> Ok(string message = "Success.")
        {
            return Success(Status200OK, message);
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> Created(string message = "Creation success.")
        {
            return Success(Status201Created, message);
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> BadRequest(string? message = null)
        {
            return Error(Status400BadRequest, message);
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> Unauthorized(string? message = null)
        {
            return Error(Status401Unauthorized, message);
        }

        /// <inheritdoc/>
        public virtual ResponseWrapper<TData> NotFound(string? message = null)
        {
            return Error(Status404NotFound, message);
        }

        #endregion

        #region Static constructors

        /// <summary>
        /// Create a new <see cref="ResponseWrapper{TData}"/> with the 200 HTTP status code.
        /// </summary>
        public static ResponseWrapper<TData> NewOk(TData? data = default, string message = "Success.")
        {
            return new ResponseWrapper<TData>(data).Ok(message);
        }

        /// <summary>
        /// Create a new <see cref="ResponseWrapper{TData}"/> with the 201 HTTP status code.
        /// </summary>
        public static ResponseWrapper<TData> NewCreated(TData? data = default, string message = "Creation success.")
        {
            return new ResponseWrapper<TData>(data).Created(message);
        }

        /// <summary>
        /// Create a new <see cref="ResponseWrapper{TData}"/> with the 400 HTTP status code.
        /// </summary>
        public static ResponseWrapper<TData> NewBadRequest(string? message = null, TData? data = default)
        {
            return new ResponseWrapper<TData>(data).BadRequest(message);
        }

        /// <summary>
        /// Create a new <see cref="ResponseWrapper{TData}"/> with the 401 HTTP status code.
        /// </summary>
        public static ResponseWrapper<TData> NewUnauthorized(string? message = null, TData? data = default)
        {
            return new ResponseWrapper<TData>(data).Unauthorized(message);
        }

        /// <summary>
        /// Create a new <see cref="ResponseWrapper{TData}"/> with the 404 HTTP status code.
        /// </summary>
        public static ResponseWrapper<TData> NewNotFound(string? message = null, TData? data = default)
        {
            return new ResponseWrapper<TData>(data).NotFound(message);
        }

        #endregion
    }
}
