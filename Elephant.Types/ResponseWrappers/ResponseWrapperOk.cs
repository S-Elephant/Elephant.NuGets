namespace Elephant.Types.ResponseWrappers
{
    /// <summary>
    /// Success (HTTP response code 200) <see cref="ResponseWrapper{TData}"/>.
    /// </summary>
    public class ResponseWrapperOk<TData> : ResponseWrapper<TData>
        where TData : new()
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperOk(TData? data = default, string message = "Success.") :
            base(data, 200, message)
        {
        }
    }

	/// <summary>
	/// Success (HTTP response code 200) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperOk : ResponseWrapper
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperOk(string message = "Success.") :
			base(200, message)
		{
		}
	}
}
