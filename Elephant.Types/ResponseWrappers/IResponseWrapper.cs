namespace Elephant.Types.ResponseWrappers
{
    /// <summary>
    /// A wrapper for returning data along with a message and success status.
    /// </summary>
    /// <typeparam name="TData">Your data type.</typeparam>
    public interface IResponseWrapper<TData> where TData : new()
    {
        /// <summary>
        /// The wrapped data. Default: default(T).
        /// </summary>
        TData? Data { get; set; }

        /// <summary>
        /// Indicates if the operations were successful.
        /// </summary>
        bool IsSuccess { get; set; }

        /// <summary>
        /// Your optional custom message.
        /// </summary>
        string? Message { get; set; }

        /// <summary>
        /// HTTP status code. Defaults to 200.
        /// </summary>
        int StatusCode { get; set; }

        /// <summary>
        /// Converts this wrapper into a BadRequest error result.
        /// </summary>
        ResponseWrapper<TData> BadRequest(string? message = null);

        /// <summary>
        /// Converts this wrapper into a generic success result.
        /// </summary>
        ResponseWrapper<TData> Success(int statusCode, string message = "Success.", TData? data = default);

        /// <summary>
        /// Converts this wrapper into a specific HTTP status error result.
        /// </summary>
        ResponseWrapper<TData> Error(int statusCode, string? message = null, TData? data = default);

        /// <summary>
        /// Converts this wrapper into an OK success result.
        /// </summary>
        ResponseWrapper<TData> Ok(string message = "Success.");

        /// <summary>
        /// Converts this wrapper into a Created success result.
        /// </summary>
        ResponseWrapper<TData> Created(string message = "Creation success.");

        /// <summary>
        /// Converts this wrapper into an Unauthorized error result.
        /// </summary>
        ResponseWrapper<TData> Unauthorized(string? message = null);

        /// <summary>
        /// Converts this wrapper into a Not Found error result.
        /// </summary>
        ResponseWrapper<TData> NotFound(string? message = null);

        /// <summary>
        /// Converts this wrapper into an Internal Server error result.
        /// </summary>
        ResponseWrapper<TData> InternalServerError(string? message = null);
    }
}