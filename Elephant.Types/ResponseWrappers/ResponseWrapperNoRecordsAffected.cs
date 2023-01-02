namespace Elephant.Types.ResponseWrappers
{
    /// <summary>
    /// Error (HTTP response code 500) <see cref="ResponseWrapper{TData}"/>.
    /// </summary>
    public class ResponseWrapperNoRecordsAffected<TData> : ResponseWrapper<TData>
        where TData : new()
    {
        private const int StatusCodeInternalServerError = 500;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperNoRecordsAffected(TData? data = default, string message = "No records affected.") :
            base(data, StatusCodeInternalServerError, message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperNoRecordsAffected(string message) :
            base(default, StatusCodeInternalServerError, message)
        {
        }
    }

	/// <summary>
	/// Error (HTTP response code 500) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperNoRecordsAffected : ResponseWrapper
	{
		private const int StatusCodeInternalServerError = 500;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperNoRecordsAffected(string message = "No records affected.") :
			base(StatusCodeInternalServerError, message)
		{
		}
	}
}
