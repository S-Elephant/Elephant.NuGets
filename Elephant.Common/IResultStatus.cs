namespace Elephant.Common
{
    /// <summary>
    /// A wrapper for returning data along with a message and success status.
    /// </summary>
    /// <typeparam name="T">Your data type.</typeparam>
    public interface IResultStatus<T> : IResultSuccess<T>
    {
        /// <summary>
        /// The (error) message. It's empty if there's no error.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Status code. Defaults to 200.
        /// </summary>
        int StatusCode { get; set; }

        /// <summary>
        /// Convert to a BadRequest result.
        /// </summary>
        void BadRequest(string message = "");

        /// <summary>
        /// Convert to an Unauthorized result.
        /// </summary>
        void Unauthorized(string message = "");

        /// <summary>
        /// Converts this instance to a new instance of a new type with a new value, all other properties are copied.
        /// </summary>
        /// <typeparam name="TNewType">The new type.</typeparam>
        /// <param name="newValue">The new value.</param>
        ResultStatus<TNewType> ConvertToNewTypeWithNewValue<TNewType>(TNewType newValue);
    }
}
