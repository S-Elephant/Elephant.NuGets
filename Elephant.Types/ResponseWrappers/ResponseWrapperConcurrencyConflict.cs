using System.Diagnostics.CodeAnalysis;

namespace Elephant.Types.ResponseWrappers
{
	/// <summary>
	/// Error (HTTP response code 409) <see cref="ResponseWrapper{TData}"/>.
	/// </summary>
	[SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic and non-generic version belong together.")]
	public class ResponseWrapperConcurrencyConflict<TData> : ResponseWrapper<TData>
        where TData : new()
    {
	    // ReSharper disable once InconsistentNaming
        private const int StatusCodeConflict = 409;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperConcurrencyConflict(TData? data = default, string message = "Concurrency conflict detected.")
            : base(data, StatusCodeConflict, message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperConcurrencyConflict(string message)
            : base(default, StatusCodeConflict, message)
        {
        }
    }

	/// <summary>
	/// Error (HTTP response code 409) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperConcurrencyConflict : ResponseWrapper
	{
		// ReSharper disable once InconsistentNaming
		private const int StatusCodeConflict = 409;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperConcurrencyConflict(string message = "Concurrency conflict detected.")
            : base(StatusCodeConflict, message)
		{
		}
	}
}
