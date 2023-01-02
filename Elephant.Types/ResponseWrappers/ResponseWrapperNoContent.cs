namespace Elephant.Types.ResponseWrappers
{
    /// <summary>
    /// Success (HTTP response code 204) <see cref="ResponseWrapper{TData}"/>.
    /// </summary>
    public class ResponseWrapperNoContent<TData> : ResponseWrapper<TData>
        where TData : new()
    {
        /// <summary>
        /// No content success HTTP status code.
        /// </summary>
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
		private const int StatusCodeNoContent = 204;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperNoContent(string message = "No content.")
			: base( StatusCodeNoContent, message)
		{
		}
	}
}
