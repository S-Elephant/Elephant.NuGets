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
	/// Success (HTTP response code 201) <see cref="ResponseWrapper{TData}"/> without data.
	/// </summary>
	public class ResponseWrapperCreated : ResponseWrapper<bool>
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperCreated(string message = "Created.") :
			base(false, 201, message)
		{
		}
	}
}
