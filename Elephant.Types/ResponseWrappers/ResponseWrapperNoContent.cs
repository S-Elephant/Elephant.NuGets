using System.Diagnostics.CodeAnalysis;

namespace Elephant.Types.ResponseWrappers
{
	/// <summary>
	/// Success (HTTP response code 204) <see cref="ResponseWrapper{TData}"/>.
	/// </summary>
	[SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic and non-generic version belong together.")]
	public class ResponseWrapperNoContent<TData> : ResponseWrapper<TData>
		where TData : new()
	{
		/// <summary>
		/// No content success HTTP status code.
		/// </summary>
		// ReSharper disable once InconsistentNaming
		private const int StatusCodeNoContent = 204;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperNoContent(TData? data = default, string message = "No content.")
			: base(data, StatusCodeNoContent, message)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperNoContent(string message)
			: base(default, StatusCodeNoContent, message)
		{
		}
	}

	/// <summary>
	/// Success (HTTP response code 204) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperNoContent : ResponseWrapper
	{
		/// <summary>
		/// No content success HTTP status code.
		/// </summary>
		// ReSharper disable once InconsistentNaming
		private const int StatusCodeNoContent = 204;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperNoContent(string message = "No content.")
			: base(StatusCodeNoContent, message)
		{
		}
	}
}
