namespace Elephant.Common
{
    /// <summary>
    /// A wrapper for returning data along with a message and success status.
    /// </summary>
    /// <typeparam name="T">Your data type.</typeparam>
    public interface IResultSuccess<T>
	{
		/// <summary>
		/// The object to wrap.
		/// </summary>
		T? Value { get; set; }

		/// <summary>
		/// Determines whether the action was successfull or not.
		/// </summary>
		bool IsSuccess { get; set; }
	}
}
