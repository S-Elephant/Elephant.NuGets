namespace Elephant.Types.Interfaces.ResponseWrappers
{
    /// <summary>
    /// A wrapper for returning data along with a message and success status.
    ///
    /// Response ranges:
    /// Informational responses (100 – 199).
    /// Successful responses(200 – 299).
    /// Redirection messages(300 – 399).
    /// Client error responses(400 – 499).
    /// Server error responses(500 – 599).
    /// </summary>
    /// <typeparam name="TData">Your data type.</typeparam>
    public interface IResponseWrapper<TData>
        where TData : new()
    {
        /// <summary>
        /// The wrapped data. Default: default(T).
        /// </summary>
        TData? Data { get; }

        /// <summary>
        /// Indicates if the operations were successful.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Indicates if the operations were unsuccessful.
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Indicates if the operations were neither successful nor unsuccessful (thus, is informative, redirectional or custom).
        /// </summary>
        bool IsInformativeRedirectionOrCustom { get; }

        /// <summary>
        /// Your optional custom message.
        /// </summary>
        string? Message { get; set; }

        /// <summary>
        /// HTTP status code. Defaults to 200.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        int StatusCode { get; }

        /// <summary>
        /// If true then this Response Wrapper may use have useful <see cref="Data"/>. If false, it will NEVER have any useful <see cref="Data"/> and <see cref="Data"/> should be ignored.
        /// </summary>
        bool UsesData { get; }

        /// <summary>
        /// Converts this wrapper into a BadRequest error result.
        /// </summary>
        IResponseWrapper<TData> BadRequest(string? message = null);

        /// <summary>
        /// Converts this wrapper into a generic success result.
        /// </summary>
        IResponseWrapper<TData> Success(int statusCode, string? message = "Success.", TData? data = default);

        /// <summary>
        /// Converts this wrapper into a specific HTTP status error result.
        /// </summary>
        IResponseWrapper<TData> Error(int statusCode, string? message = null, TData? data = default);

        /// <summary>
        /// Converts this wrapper into an OK success result.
        /// </summary>
        IResponseWrapper<TData> Ok(string message = "Success.");

        /// <summary>
        /// Converts this wrapper into a Created success result.
        /// </summary>
        IResponseWrapper<TData> Created(string message = "Creation success.");

        /// <summary>
        /// Converts this wrapper into an Unauthorized error result.
        /// </summary>
        IResponseWrapper<TData> Unauthorized(string? message = null);

        /// <summary>
        /// Converts this wrapper into a Not Found error result.
        /// </summary>
        IResponseWrapper<TData> NotFound(string? message = null);

        /// <summary>
        /// Converts this wrapper into a Not Found error result.
        /// </summary>
        IResponseWrapper<TData> UnprocessableEntity(string? message = null);

        /// <summary>
        /// Converts this wrapper into an Internal Server error result.
        /// </summary>
        IResponseWrapper<TData> InternalServerError(string? message = null);

        /// <summary>
        /// Converts this wrapper into an Internal Server error result.
        /// </summary>
        IResponseWrapper<TData> NoRecordsAffected(string? message = "No records affected.");

        /// <summary>
        /// Converts this wrapper into a no content success result.
        /// </summary>
        IResponseWrapper<TData> NoContent(string? message = "No content.");
    }

    /// <summary>
    /// A wrapper for returning data along with a message and success status.
    ///
    /// Response ranges:
    /// Informational responses (100 – 199).
    /// Successful responses(200 – 299).
    /// Redirection messages(300 – 399).
    /// Client error responses(400 – 499).
    /// Server error responses(500 – 599).
    /// </summary>
    public interface IResponseWrapper : IResponseWrapper<bool>
    {
    }
}