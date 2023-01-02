namespace Elephant.Types.ResponseWrappers
{
    /// <summary>
    /// Success (HTTP response code 201) <see cref="ResponseWrapper{TData}"/>.
    /// </summary>
    public class ResponseWrapperCreated<TData> : ResponseWrapper<TData>
        where TData : new()
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperCreated(TData? data = default, string message = "Created.") :
            base(data, 201, message)
        {
        }
    }

	/// <summary>
	/// Success (HTTP response code 201) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperCreated : ResponseWrapper
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperCreated(string message = "Created.") :
			base(201, message)
		{
		}
	}
}
