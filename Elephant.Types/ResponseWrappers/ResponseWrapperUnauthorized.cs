namespace Elephant.Types.ResponseWrappers
{
    /// <summary>
    /// Error (HTTP response code 401) <see cref="ResponseWrapper{TData}"/>.
    /// </summary>
    public class ResponseWrapperUnauthorized<TData> : ResponseWrapper<TData>
        where TData : new()
    {
        private const int StatusCodeUnauthorized = 401;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperUnauthorized(TData? data = default, string message = "Unauthorized.") :
            base(data, StatusCodeUnauthorized, message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperUnauthorized(string message) :
            base(default, StatusCodeUnauthorized, message)
        {
        }
    }

	/// <summary>
	/// Error (HTTP response code 401) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperUnauthorized : ResponseWrapper
	{
		private const int StatusCodeUnauthorized = 401;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperUnauthorized(string message = "Unauthorized.") :
			base(StatusCodeUnauthorized, message)
		{
		}
	}
}
