using System.Diagnostics.CodeAnalysis;

namespace Elephant.Types.ResponseWrappers
{
	/// <summary>
	/// Error (HTTP response code 404) <see cref="ResponseWrapper{TData}"/>.
	/// </summary>
	[SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic and non-generic version belong together.")]
	public class ResponseWrapperNotFound<TData> : ResponseWrapper<TData>
        where TData : new()
    {
        private const int StatusCodeNotFound = 404;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperNotFound(TData? data = default, string message = "Not found.")
            : base(data, StatusCodeNotFound, message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperNotFound(string message)
            : base(default, StatusCodeNotFound, message)
        {
        }
    }

	/// <summary>
	/// Error (HTTP response code 404) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperNotFound : ResponseWrapper
	{
		private const int StatusCodeNotFound = 404;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperNotFound(string message = "Not found.")
            : base(StatusCodeNotFound, message)
		{
		}
	}
}
