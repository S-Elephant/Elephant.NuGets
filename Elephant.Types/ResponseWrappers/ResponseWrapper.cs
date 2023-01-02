using System.Xml.Linq;

namespace Elephant.Types.ResponseWrappers
{
	/// <inheritdoc cref="IResponseWrapper{TData}"/>
	public class ResponseWrapper<TData> : IResponseWrapper<TData>
		where TData : new()
	{
		/// <summary>
		/// Generic success HTTP success status code.
		/// </summary>
		private const int Status200OK = 200;

		/// <summary>
		/// Created HTTP success status code.
		/// </summary>
		private const int Status201Created = 201;

		/// <summary>
		/// Bad request HTTP error status code.
		/// </summary>
		private const int Status400BadRequest = 400;

		/// <summary>
		/// Unauthorized HTTP error status code.
		/// </summary>
		private const int Status401Unauthorized = 401;

		/// <summary>
		/// Unauthorized HTTP error status code.
		/// </summary>
		private const int Status422UnprocessableEntity = 422;

		/// <summary>
		/// Not found HTTP error status code.
		/// </summary>
		private const int Status404NotFound = 404;

		/// <summary>
		/// Internal server HTTP error status code.
		/// </summary>
		private const int Status500InternalServerError = 500;

		/// <inheritdoc/>
		public TData? Data { get; protected set; } = default;

		/// <inheritdoc/>
		public bool IsSuccess { get { return IsSuccessStatusCode(StatusCode); } }

		/// <inheritdoc/>
		public bool IsError { get { return IsErrorStatusCode(StatusCode); } }

		/// <inheritdoc/>
		public bool IsInformativeRedirectionOrCustom { get { return IsInformativeRedirectionOrCustomStatusCode(StatusCode); } }

		/// <inheritdoc/>
		public int StatusCode { get; protected set; } = Status200OK;

		/// <inheritdoc/>
		public string? Message { get; set; } = null;

		/// <inheritdoc/>
		public bool UsesData { get; protected set; } = true;

		/// <summary>
		/// Success Constructor with default <see cref="Data"/>.
		/// </summary>
		public ResponseWrapper()
		{
		}

		/// <summary>
		/// Success constructor with just <paramref name="data"/>.
		/// </summary>
		public ResponseWrapper(TData? data)
		{
			Data = data;
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		public ResponseWrapper(TData? data, int statusCode, string? message = null)
		{
			Data = data;
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

		/// <summary>
		/// Returns true if the specified <paramref name="statusCode"/> is an error status code.
		/// </summary>
		private bool IsErrorStatusCode(int statusCode)
		{
			return statusCode >= 400 && statusCode <= 599;
		}

		/// <summary>
		/// Indicates if the operations were neither successful nor unsuccessful (thus, is informative, redirectional or custom).
		/// </summary>
		private bool IsInformativeRedirectionOrCustomStatusCode(int statusCode)
		{
			return !IsSuccessStatusCode(statusCode) && !IsErrorStatusCode(statusCode);
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
			StatusCode = statusCode;
			Message = message;
			Data = data;

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

		/// <inheritdoc/>
		public virtual ResponseWrapper<TData> UnprocessableEntity(string? message = null)
		{
			return Error(Status422UnprocessableEntity, message);
		}

		/// <inheritdoc/>
		public virtual ResponseWrapper<TData> InternalServerError(string? message = null)
		{
			return Error(Status500InternalServerError, message);
		}

		/// <inheritdoc/>
		public virtual ResponseWrapper<TData> NoRecordsAffected(string? message = "No records affected.")
		{
			return Error(Status500InternalServerError, message);
		}

		/// <inheritdoc/>
		public virtual ResponseWrapper<TData> NoContent(string? message = "No content.")
		{
			return Error(Status500InternalServerError, message);
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

		/// <summary>
		/// Create a new <see cref="ResponseWrapper{TData}"/> with the 422 HTTP status code.
		/// </summary>
		public static ResponseWrapper<TData> NewUnprocessableEntity(string? message = null, TData? data = default)
		{
			return new ResponseWrapper<TData>(data).UnprocessableEntity(message);
		}

		/// <summary>
		/// Create a new <see cref="ResponseWrapper{TData}"/> with the 500 HTTP status code.
		/// </summary>
		public static ResponseWrapper<TData> NewNoRecordsAffected(string? message = "No records affected.", TData? data = default)
		{
			return new ResponseWrapper<TData>(data).NoRecordsAffected(message);
		}

		/// <summary>
		/// Create a new <see cref="ResponseWrapper{TData}"/> with the 204 HTTP status code.
		/// </summary>
		public static ResponseWrapper<TData> NewNoContent(string? message = "No content.", TData? data = default)
		{
			return new ResponseWrapper<TData>(data).NoContent(message);
		}

		#endregion
	}

	/// <summary>
	/// <see cref="ResponseWrapper{TData}"/> without data.
	/// </summary>
	public class ResponseWrapper : ResponseWrapper<bool>
	{
		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		public ResponseWrapper(int statusCode, string? message = null)
		{
			UsesData = false;
			Data = false;
			StatusCode = statusCode;
			Message = message;
		}
	}
}
