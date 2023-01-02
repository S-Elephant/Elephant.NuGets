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
	/// Success (HTTP response code 200) <see cref="ResponseWrapper{TData}"/> without data.
	/// </summary>
	public class ResponseWrapperOk : ResponseWrapper<bool>
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperOk(string message = "Success.") :
			base(false, 200, message)
		{
		}
	}
}
