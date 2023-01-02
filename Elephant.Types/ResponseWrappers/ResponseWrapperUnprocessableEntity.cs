namespace Elephant.Types.ResponseWrappers
{
    /// <summary>
    /// Error (HTTP response code 422) <see cref="ResponseWrapper{TData}"/>.
    /// </summary>
    public class ResponseWrapperUnprocessableEntity<TData> : ResponseWrapper<TData>
        where TData : new()
    {
        private const int StatusCodeUnprocessableEntity = 422;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperUnprocessableEntity(TData? data = default, string message = "Unprocessable entity.") :
            base(data, StatusCodeUnprocessableEntity, message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperUnprocessableEntity(string message) :
            base(default, StatusCodeUnprocessableEntity, message)
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
		public ResponseWrapperUnprocessableEntity(string message = "Unprocessable entity.") :
			base(StatusCodeUnprocessableEntity, message)
		{
		}
	}
}
