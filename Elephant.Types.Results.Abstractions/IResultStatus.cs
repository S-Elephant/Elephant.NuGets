namespace Elephant.Types.Results.Abstractions
{
	/// <summary>
	/// Result status.
	/// </summary>
	// ReSharper disable once TypeParameterCanBeVariant
	public interface IResultStatus<TData>
	{
		/// <summary>
		/// HTTP status code. Defaults to 200.
		/// </summary>
		int StatusCode { get; }

		/// <summary>
		/// Message.
		/// </summary>
		string? Message { get; }

		/// <summary>
		/// Data.
		/// </summary>
		TData? Data { get; }

		/// <summary>
		/// Returns true if the specified <see cref="StatusCode"/> is a success status code.
		/// </summary>
		bool IsSuccess { get; }

		/// <summary>
		/// Returns true if the specified <see name="StatusCode"/> is an error status code.
		/// </summary>
		bool IsError { get; }

		/// <summary>
		/// Indicates if the operations were neither successful nor unsuccessful (thus, is informative, redirectional or custom).
		/// </summary>
		bool IsInformativeRedirectionOrCustom { get; }
	}
}
