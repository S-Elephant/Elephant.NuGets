using System.Diagnostics.CodeAnalysis;

namespace Elephant.Types.ResponseWrappers
{
	/// <summary>
	/// Error (HTTP response code 400) <see cref="ResponseWrapper{TData}"/>.
	/// </summary>
	[SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic and non-generic version belong together.")]
	public class ResponseWrapperBadRequest<TData> : ResponseWrapper<TData>
		where TData : new()
	{
		private const int StatusCodeBadRequest = 400;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperBadRequest(TData? data = default, string message = "Bad request.")
            : base(data, StatusCodeBadRequest, message)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperBadRequest(string message)
            : base(default, StatusCodeBadRequest, message)
		{
		}
	}

	/// <summary>
	/// Error (HTTP response code 400) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperBadRequest : ResponseWrapper
	{
		private const int StatusCodeBadRequest = 400;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperBadRequest(string message = "Bad request.")
            : base(StatusCodeBadRequest, message)
		{
		}
	}
}
