namespace Elephant.Types.ResponseWrappers
{
	/// <summary>
	/// Error (HTTP response code 500) <see cref="ResponseWrapper{TData}"/>.
	/// </summary>
	public class ResponseWrapperInternalServerError<TData> : ResponseWrapper<TData>
		where TData : new()
	{
		private const int StatusCodeInternalServerError = 500;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperInternalServerError(TData? data = default, string message = "Internal server error.") :
			base(data, StatusCodeInternalServerError, message)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperInternalServerError(string message) :
			base(default, StatusCodeInternalServerError, message)
		{
		}
	}

	/// <summary>
	/// Error (HTTP response code 500) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperInternalServerError : ResponseWrapper
	{
		private const int StatusCodeInternalServerError = 500;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperInternalServerError(string message = "Internal server error.") :
			base(StatusCodeInternalServerError, message)
		{
		}
	}
}
