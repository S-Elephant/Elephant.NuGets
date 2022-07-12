namespace Elephant.Common.ResponseWrappers
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
        /// The (error) messages. It's empty if there are no errors.
        /// </summary>
        List<string> Errors { get; set; }

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
        ResponseWrapper<TData> BadRequest(List<string>? errors = null, string? message = null);

        /// <summary>
        /// Converts this wrapper into a Created success result.
        /// </summary>
        ResponseWrapper<TData> Created(string message = "Created success.", bool clearErrors = true);

        /// <summary>
        /// Converts this wrapper into a specific HTTP status error result.
        /// </summary>
        ResponseWrapper<TData> Error(int statusCode, List<string>? errors = null, string? message = null);

        /// <summary>
        /// Converts this wrapper into an OK success result.
        /// </summary>
        ResponseWrapper<TData> Ok(string message = "Success.", bool clearErrors = true);

        /// <summary>
        /// Converts this wrapper into a generic success result.
        /// </summary>
        ResponseWrapper<TData> Success(int statusCode, string message = "Success.", bool clearErrors = true);

        /// <summary>
        /// Converts this wrapper into an Unauthorized error result.
        /// </summary>
        ResponseWrapper<TData> Unauthorized(List<string>? errors = null, string? message = null);
    }
}