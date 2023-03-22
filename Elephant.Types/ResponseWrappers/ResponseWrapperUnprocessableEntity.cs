using System.Diagnostics.CodeAnalysis;

namespace Elephant.Types.ResponseWrappers
{
	/// <summary>
	/// Error (HTTP response code 422) <see cref="ResponseWrapper{TData}"/>.
	/// </summary>
	[SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic and non-generic version belong together.")]
	public class ResponseWrapperUnprocessableEntity<TData> : ResponseWrapper<TData>
        where TData : new()
    {
        private const int StatusCodeUnprocessableEntity = 422;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperUnprocessableEntity(TData? data = default, string message = "Unprocessable entity.")
            : base(data, StatusCodeUnprocessableEntity, message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperUnprocessableEntity(string message)
            : base(default, StatusCodeUnprocessableEntity, message)
        {
        }
    }

	/// <summary>
	/// Error (HTTP response code 422) <see cref="ResponseWrapper"/>
	/// </summary>
	public class ResponseWrapperUnprocessableEntity : ResponseWrapper
	{
		private const int StatusCodeUnprocessableEntity = 422;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperUnprocessableEntity(string message = "Unprocessable entity.")
            : base(StatusCodeUnprocessableEntity, message)
		{
		}
	}
}
