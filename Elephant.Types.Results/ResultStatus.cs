using System.Diagnostics;
using System.Net;
using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results
{
	/// <inheritdoc/>
	[DebuggerDisplay("Statuscode: {StatusCode} Message: {Message}")]
	public class ResultStatus<TData> : IResultStatus<TData>
	{
		/// <inheritdoc/>
		public TData? Data { get; protected set; } = default;

		/// <inheritdoc/>
		public bool IsSuccess => IsSuccessStatusCode(StatusCode);

		/// <inheritdoc/>
		public bool IsError => IsErrorStatusCode(StatusCode);

		/// <inheritdoc/>
		public bool IsInformativeRedirectionOrCustom => IsInformativeRedirectionOrCustomStatusCode(StatusCode);

		/// <inheritdoc/>
		public int StatusCode { get; protected set; } = (int)HttpStatusCode.OK;

		/// <inheritdoc/>
		public string? Message => _resultMessage.Message;

		/// <inheritdoc cref="IResultMessage"/>
		private readonly IResultMessage _resultMessage = new ResultMessage();

		/// <summary>
		/// Success Constructor with default <see cref="Data"/>.
		/// </summary>
		public ResultStatus()
		{
		}

		/// <summary>
		/// Success constructor with just <paramref name="data"/>.
		/// </summary>
		public ResultStatus(TData? data)
		{
			Data = data;
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		public ResultStatus(TData? data, int statusCode, string? message = null)
		{
			Data = data;
			StatusCode = statusCode;
			_resultMessage = new ResultMessage(message);
		}

		/// <summary>
		/// Returns true if the specified <paramref name="statusCode"/> is a success status code.
		/// </summary>
		protected virtual bool IsSuccessStatusCode(int statusCode)
		{
			return statusCode >= 200 && statusCode < 300;
		}

		/// <summary>
		/// Returns true if the specified <paramref name="statusCode"/> is an error status code.
		/// </summary>
		protected virtual bool IsErrorStatusCode(int statusCode)
		{
			return statusCode >= 400 && statusCode <= 599;
		}

		/// <summary>
		/// Indicates if the operations were neither successful nor unsuccessful (thus, is informative, redirectional or custom).
		/// </summary>
		protected virtual bool IsInformativeRedirectionOrCustomStatusCode(int statusCode)
		{
			return !IsSuccessStatusCode(statusCode) && !IsErrorStatusCode(statusCode);
		}
	}
}
